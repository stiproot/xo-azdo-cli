namespace Xo.AzDO.Engine.Processors;

public class GetFolderProcessor : BaseHttpProcessor, IProcessor<GetFolderCmd, FolderRes>
{
	private const string API_VERSION = "7.0";

	public GetFolderProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer
	) : base(httpClientFactory, API_VERSION, typeSerializer) { }

	public async Task<FolderRes> ProcessAsync(GetFolderCmd cmd)
	{
		try
		{
			string url = $"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}/{cmd.FolderName}?api-version={this._ApiVersion}";
			var uri= new Uri(url);
			var req = HttpRequestMessageFactory.Create(uri);

			using var httpClient = this.CreateHttpClient();
			var resp = await httpClient.SendAsync(req);
			var respContent = await resp.Content.ReadAsStringAsync();

			if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return new FolderRes{ IsSuccessful = false, ExceptionInfo = respContent };

			resp.EnsureSuccessStatusCode();

			return new FolderRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent), IsSuccessful = true };
		}
		catch(Exception ex)
		{
			return new FolderRes{ IsSuccessful = false, ExceptionInfo = ex.StackTrace };
		}
	}
}
