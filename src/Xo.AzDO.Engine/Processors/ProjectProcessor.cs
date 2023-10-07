namespace Xo.AzDO.Engine.Processors;

public class ProjectProcessor : BaseHttpProcessor, IProcessor<GetProjectDetailsCmd, ProjectRes>
{
	private const string API_VERSION = "7.0";

	public ProjectProcessor(
		IHttpClientFactory httpClientFactory,
		ITypeSerializer typeSerializer
	) : base(httpClientFactory, API_VERSION, typeSerializer) { }

	public async Task<ProjectRes> ProcessAsync(GetProjectDetailsCmd cmd)
	{
		var url = new Uri($"{BASE_URL}/_apis/projects/{cmd.ProjectId}?api-version={this._ApiVersion}");
		var req = HttpRequestMessageFactory.Create(url);

		using var httpClient = this.CreateHttpClient();
		var resp = await httpClient.SendAsync(req);
		resp.EnsureSuccessStatusCode();
		var respContent = await resp.Content.ReadAsStringAsync();

		return new ProjectRes { ExtResp = this._TypeSerializer.Deserialize<ExtProjectResp>(respContent) };
	}
}
