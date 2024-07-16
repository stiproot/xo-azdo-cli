namespace Xo.AzDO.Engine.Processors;

public class TeamProcessor : BaseHttpProcessor, IProcessor<GetTeamDetailsCmd, TeamRes>
{
    private const string API_VERSION = "7.0";

    public TeamProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer
    ) : base(httpClientFactory, API_VERSION, typeSerializer) { }

    public async Task<TeamRes> ProcessAsync(GetTeamDetailsCmd cmd)
    {
        string url = $"{BASE_URL}/_apis/projects/{cmd.ProjectId}/teams/{cmd.TeamId}?api-version={this._ApiVersion}";
        var uri = new Uri(url);
        var req = HttpRequestMessageFactory.Create(uri);

        using var httpClient = this.CreateHttpClient();

        var resp = await httpClient.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        var respContent = await resp.Content.ReadAsStringAsync();

        return new TeamRes { ExtResp = this._TypeSerializer.Deserialize<ExtTeamResp>(respContent) };
    }
}
