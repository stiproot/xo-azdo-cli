namespace Xo.AzDO.Cli.Processors;

internal class DashboardProcessor : BaseHttpProcessor, IProcessor<CreateDashboardCmd, DashboardRes>
{
    public DashboardProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer
    ) : base(
        httpClientFactory,
        "7.0-preview.3",
        typeSerializer
    )
    { }

    public async Task<DashboardRes> ProcessAsync(CreateDashboardCmd cmd)
    {
        var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/{cmd.TeamName}/_apis/dashboard/dashboards?api-version={this._ApiVersion}");
        var reqContent = this._TypeSerializer.Serialize<ExtDashboardReq>(cmd.Req);

        Console.WriteLine(reqContent);

        var req = HttpRequestMessageFactory.Create(url, reqContent, HttpMethod.Post);

        using var httpClient = this.CreateHttpClient();
        var resp = await httpClient.SendAsync(req);

        var respContent = await resp.Content.ReadAsStringAsync();
        Console.WriteLine(respContent);

        resp.EnsureSuccessStatusCode();

        return new DashboardRes { ExtResp = JsonConvert.DeserializeObject<ExtDashboardResp>(respContent) };
    }
}
