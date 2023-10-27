namespace Xo.AzDO.Engine.Processors;

public class CreateFolderProcessor : BaseHttpProcessor, IProcessor<CreateFolderCmd, FolderRes>
{
	private const string API_VERSION = "7.0";
	private readonly IProcessor<GetFolderCmd, FolderRes> _getFolderProcessor;

	public CreateFolderProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer,
		IProcessor<GetFolderCmd, FolderRes> getFolderProcessor
	) : base(httpClientFactory, API_VERSION, typeSerializer) 
	{
		this._getFolderProcessor = getFolderProcessor ?? throw new ArgumentNullException(nameof(getFolderProcessor));
	}

	public async Task<FolderRes> ProcessAsync(CreateFolderCmd cmd)
	{
		// Determine if folder exists...
		var getFolderCmd = new GetFolderCmd{ FolderName = cmd.FolderName, QueryFolderPath = cmd.QueryFolderPath };
		var getFolderRes = await this._getFolderProcessor.ProcessAsync(getFolderCmd);

		if(getFolderRes.IsSuccessful) return getFolderRes;

		string url = $"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}?api-version={this._ApiVersion}";
		var uri = new Uri(url);

		var reqContent = this._TypeSerializer.Serialize(new ExtFolderReq { Name = cmd.FolderName });

		var req = HttpRequestMessageFactory.Create(uri, reqContent, HttpMethod.Post);

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(req);
		var respContent = await resp.Content.ReadAsStringAsync();
		resp.EnsureSuccessStatusCode();

		return new FolderRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent) };
	}
}
