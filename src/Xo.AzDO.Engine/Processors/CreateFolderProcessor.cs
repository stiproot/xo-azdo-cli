namespace Xo.AzDO.Engine.Processors;

public class CreateFolderProcessor : BaseHttpProcessor, IProcessor<CreateFolderCmd, FolderRes>
{
	private const string API_VERSION = "7.0";

	public CreateFolderProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer
	) : base(httpClientFactory, API_VERSION, typeSerializer) { }

	public async Task<FolderRes> ProcessAsync(CreateFolderCmd cmd)
	{
		var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}?api-version={this._ApiVersion}");
		var reqContent = this._TypeSerializer.Serialize(new ExtFolderReq { Name = cmd.FolderName });
		var req = HttpRequestMessageFactory.Create(url, reqContent, HttpMethod.Post);

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(req);
		var respContent = await resp.Content.ReadAsStringAsync();
		resp.EnsureSuccessStatusCode();

		return new FolderRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent) };
	}
}
