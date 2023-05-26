namespace Xo.AzDO.Cli.Processors;

internal class GetFolderProcessor : BaseHttpProcessor, IProcessor<GetFolderCmd, FolderRes>
{
	private const string API_VERSION = "7.0";

	public GetFolderProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer
	) : base(httpClientFactory, API_VERSION, typeSerializer) { }

	public async Task<FolderRes> ProcessAsync(GetFolderCmd cmd)
	{
		var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}/{cmd.FolderName}?api-version={this._ApiVersion}");
		var req = HttpRequestMessageFactory.Create(url);

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(req);
		var respContent = await resp.Content.ReadAsStringAsync();

		if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

		resp.EnsureSuccessStatusCode();

		return new FolderRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent) };
	}
}
