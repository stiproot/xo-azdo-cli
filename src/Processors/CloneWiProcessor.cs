namespace Xo.AzDO.Cli.Processors;

internal class CloneWiProcessor : BaseHttpProcessor, IProcessor<CloneWiCmd, CloneWiRes>
{
	private const string API_VERSION = "7.0";

	private readonly IProcessor<GetWiCmd, GetWiRes> _getWiProcessor;
	private readonly IProcessor<CreateWiCmd, WiRes> _createWiProcessor;
	private CloneWiCmd? RootCmd;
	private string IterationPath() => $"{RootCmd!.IterationBasePath}\\{RootCmd!.IterationName}";

	public CloneWiProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer,
		IProcessor<GetWiCmd, GetWiRes> getWiProcessor,
		IProcessor<CreateWiCmd, WiRes> createWiProcessor
	) : base(httpClientFactory, API_VERSION, typeSerializer) 
	{
		this._getWiProcessor = getWiProcessor ?? throw new ArgumentNullException(nameof(getWiProcessor));
		this._createWiProcessor = createWiProcessor ?? throw new ArgumentNullException(nameof(createWiProcessor));
	}

	public async Task<CloneWiRes> ProcessAsync(CloneWiCmd cmd)
	{
		this.RootCmd = cmd;

		GetWiRes wiRes = await this._getWiProcessor.ProcessAsync(new GetWiCmd { Id = cmd.Id });

		CreateWiCmd createWiCmd = await ProcessRes(wiRes);
		createWiCmd.relation = new Models.WorkItemRelation
		{
			relation_type = "Child",
			url = $"https://dev.azure.com/Derivco/Software/_workitems/edit/{cmd.ParentId}"
		};

		WiRes wi = await this._createWiProcessor.ProcessAsync(createWiCmd);

		return new CloneWiRes { };
	}

	private async Task<CreateWiCmd> ProcessRes(GetWiRes res)
	{
		var children = await ProcessChildren(res);

		CreateWiCmd cmd = Map(res, children);

		return cmd;
	}

	private async Task<List<CreateWiCmd>> ProcessChildren(GetWiRes res)
	{
		if(res.ExtResp.relations is null || !res.ExtResp.relations.Any()) return new List<CreateWiCmd>();

		var ids = res.ExtResp.relations.Where(x => x.rel == "System.LinkTypes.Hierarchy-Forward").Select(c => int.Parse(c.url.Split('/').Last())); 

		var cmds = ids.Select(i => new GetWiCmd { Id = i });

		var wis = await Task.WhenAll(cmds.Select(c => this._getWiProcessor.ProcessAsync(c)));

		var ress = await Task.WhenAll(wis.Select(w => ProcessRes(w)));

		return ress.ToList();
	}

	private CreateWiCmd Map(
			GetWiRes res, 
			IList<CreateWiCmd> children
	)
	{
		ExtGetWiResp extResp = res.ExtResp;

		CreateWiCmd cmd = new CreateWiCmd 
		{ 
			type = extResp.fields["System.WorkItemType"].ToString()!,
			title = extResp.fields["System.Title"].ToString()!,
			description = SafeGet(extResp.fields, "System.Description")?.ToString() ?? string.Empty,
			area_path = this.RootCmd!.AreaPath,
			iteration_path = IterationPath(),
			children = children,
			tags = BuildTags(extResp.fields),
		};

		return cmd;
	}

	private string BuildTags(IDictionary<string, object> fields)
	{
		string tags = SafeGet(fields, "System.Tags")?.ToString() ?? string.Empty;
		return $"{this.RootCmd!.Tags};{tags}";
	}

	private object? SafeGet(
			IDictionary<string, object> dict, 
			string key
	)
	{
		if(dict.TryGetValue(key, out object? value)) return value;
		return null;
	}
}
