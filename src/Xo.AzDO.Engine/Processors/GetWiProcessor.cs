namespace Xo.AzDO.Engine.Processors;

public class GetWiProcessor : BaseHttpProcessor, IProcessor<GetWiCmd, GetWiRes>
{
    const string API_VERSION = "7.0";

    public GetWiProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer
    ) : base(httpClientFactory, API_VERSION, typeSerializer)
    {
    }

    public async Task<GetWiRes> ProcessAsync(GetWiCmd cmd)
    {
        var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/workitems/{cmd.Id}?$expand=all&api-version={this._ApiVersion}");

        var httpReq = HttpRequestMessageFactory.Create(url);

        using var httpClient = this.CreateHttpClient();
        var resp = await httpClient.SendAsync(httpReq);
        resp.EnsureSuccessStatusCode();
        var respContent = await resp.Content.ReadAsStringAsync();

        Console.WriteLine(respContent);

        var type = this._TypeSerializer.Deserialize<ExtGetWiResp>(respContent);

        return new GetWiRes { ExtResp = type };
    }
}
