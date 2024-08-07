namespace Xo.AzDO.Engine.Processors;

public class QueryProcessor : BaseHttpProcessor, IProcessor<QueryCmd, QueryRes>
{
    private readonly IProcessor<BuildWiqlCmd, WiqlRes> _wiqlProcessor;
    private readonly IProcessor<GetQueryCmd, GetQueryRes> _getQueryProcessor;
    private const string API_VERSION = "7.0";

    public QueryProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer,
        IProcessor<BuildWiqlCmd, WiqlRes> wiqlProcessor,
        IProcessor<GetQueryCmd, GetQueryRes> getQueryProcessor
    ) : base(
        httpClientFactory,
        API_VERSION,
        typeSerializer
    )
    {
        this._wiqlProcessor = wiqlProcessor ?? throw new ArgumentNullException(nameof(wiqlProcessor));
        this._getQueryProcessor = getQueryProcessor ?? throw new ArgumentNullException(nameof(getQueryProcessor));
    }

    public async Task<QueryRes> ProcessAsync(QueryCmd cmd)
    {
        string url = $"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryFolderPath}?api-version={this._ApiVersion}";
        var uri = new Uri(url);

        string queryPath = $"{cmd.QueryFolderPath}/{cmd.QueryName}";
        var getQueryCmd = new GetQueryCmd { QueryPath = queryPath };
        var getQueryRes = await this._getQueryProcessor.ProcessAsync(getQueryCmd);

        if (getQueryRes.IsSuccessful)
        {
            return new QueryRes { ExtResp = getQueryRes.ExtResp! };
        }

        var wiql = cmd.Wiql is not null ? cmd.Wiql : this._wiqlProcessor.ProcessAsync(cmd.BuildWiqlCmd!).Result.WiQuery;

        var reqContent = this._TypeSerializer.Serialize(new ExtQueryReq
        {
            Wiql = wiql,
            Name = cmd.QueryName
        });

        var req = HttpRequestMessageFactory.Create(uri, reqContent, HttpMethod.Post);

        using var httpClient = this.CreateHttpClient();
        var resp = await httpClient.SendAsync(req);

        resp.EnsureSuccessStatusCode();

        var respContent = await resp.Content.ReadAsStringAsync();

        return new QueryRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent) };
    }
}
