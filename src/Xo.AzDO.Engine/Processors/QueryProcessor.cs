namespace Xo.AzDO.Engine.Processors;

public class QueryProcessor : BaseHttpProcessor, IProcessor<QueryCmd, QueryRes>
{
    private readonly IProcessor<BuildWiqlCmd, WiqlRes> _wiqlProcessor;
    private const string API_VERSION = "7.0";

    public QueryProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer,
        IProcessor<BuildWiqlCmd, WiqlRes> wiqlProcessor
    ) : base(
        httpClientFactory,
        API_VERSION,
        typeSerializer
    )
    {
        this._wiqlProcessor = wiqlProcessor ?? throw new ArgumentNullException(nameof(wiqlProcessor));
    }

    public async Task<QueryRes> ProcessAsync(QueryCmd cmd)
    {
        try
        {
            string url = $"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}?api-version={this._ApiVersion}";
            var uri = new Uri(url);
            var wiq = this._wiqlProcessor.ProcessAsync(cmd.BuildWiqlCmd).Result;

            var reqContent = this._TypeSerializer.Serialize(new ExtQueryReq
            {
                Wiql = wiq.WiQuery,
                Name = cmd.QueryName
            });
            var req = HttpRequestMessageFactory.Create(uri, reqContent, HttpMethod.Post);

            using var httpClient = this.CreateHttpClient();
            var resp = await httpClient.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var respContent = await resp.Content.ReadAsStringAsync();

            return new QueryRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent) };
        }
        catch (Exception ex)
        {
            Console.WriteLine(this._TypeSerializer.Serialize(ex));
            throw;
        }
    }
}
