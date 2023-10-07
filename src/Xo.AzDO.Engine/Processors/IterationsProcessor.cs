namespace Xo.AzDO.Engine.Processors;

public class IterationsProcessor : BaseHttpProcessor, IProcessor<GetIterationsCmd, IterationsRes>
{
	private const string API_VERSION = "7.0";

	public IterationsProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer
	) : base(httpClientFactory, API_VERSION, typeSerializer) { }

	public async Task<IterationsRes> ProcessAsync(GetIterationsCmd cmd)
	{
		var url = new Uri($"{BASE_URL}/{cmd.ProjectId}/{cmd.TeamName}/_apis/work/teamsettings/iterations?api-version={this._ApiVersion}");
		var req = HttpRequestMessageFactory.Create(url);

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(req);
		resp.EnsureSuccessStatusCode();
		var respContent = await resp.Content.ReadAsStringAsync();

		return new IterationsRes { ExtResp = this._TypeSerializer.Deserialize<ExtIterationsResp>(respContent) };
	}
}
