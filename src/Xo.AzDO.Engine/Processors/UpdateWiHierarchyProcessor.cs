namespace Xo.AzDO.Engine.Processors;

public class UpdateWiHierarchyProcessor : BaseHttpProcessor, IProcessor<UpdateWiHierarchyCmd, UpdateWiHierarchyRes>
{
    private const string API_VERSION = "7.0";

    private readonly IProcessor<GetWiCmd, GetWiRes> _getWiProcessor;
    private readonly IProcessor<UpdateWiCmd, UpdateWiRes> _updateWiProcessor;
    private UpdateWiHierarchyCmd? RootCmd;

    public UpdateWiHierarchyProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer,
        IProcessor<GetWiCmd, GetWiRes> getWiProcessor,
        IProcessor<UpdateWiCmd, UpdateWiRes> updateWiProcessor
    ) : base(httpClientFactory, API_VERSION, typeSerializer)
    {
        this._getWiProcessor = getWiProcessor ?? throw new ArgumentNullException(nameof(getWiProcessor));
        this._updateWiProcessor = updateWiProcessor ?? throw new ArgumentNullException(nameof(updateWiProcessor));
    }

    public async Task<UpdateWiHierarchyRes> ProcessAsync(UpdateWiHierarchyCmd cmd)
    {
        this.RootCmd = cmd;

        GetWiRes wiRes = await this._getWiProcessor.ProcessAsync(new GetWiCmd { Id = cmd.Id });
        
        await this.ProcessRes(wiRes);

        return new UpdateWiHierarchyRes { };
    }

    private async Task ProcessRes(GetWiRes res)
    {
        UpdateWiCmd updateWiCmd = this.Map(res);

        await this._updateWiProcessor.ProcessAsync(updateWiCmd);

        await ProcessChildren(res);
    }

    private async Task ProcessChildren(GetWiRes res)
    {
        if (res.ExtResp.relations is null || !res.ExtResp.relations.Any()) return;

        var ids = res.ExtResp.relations.Where(x => x.rel == "System.LinkTypes.Hierarchy-Forward").Select(c => int.Parse(c.url.Split('/').Last()));

        var cmds = ids.Select(i => new GetWiCmd { Id = i });

        var wis = await Task.WhenAll(cmds.Select(c => this._getWiProcessor.ProcessAsync(c)));

        await Task.WhenAll(wis.Select(ProcessRes));
    }

    private UpdateWiCmd Map(
        GetWiRes res
    )
    {
        ExtGetWiResp extResp = res.ExtResp;

        UpdateWiCmd cmd = new UpdateWiCmd
        {
            id = res.ExtResp.id,
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
        if (dict.TryGetValue(key, out object? value)) return value;
        return null;
    }
}
