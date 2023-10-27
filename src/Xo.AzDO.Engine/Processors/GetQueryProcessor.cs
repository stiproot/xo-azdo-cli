namespace Xo.AzDO.Engine.Processors;

public class GetQueryProcessor : BaseHttpProcessor, IProcessor<GetQueryCmd, GetQueryRes>
{
    private const string API_VERSION = "7.0";

    public GetQueryProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer
    ) : base(
        httpClientFactory,
        API_VERSION,
        typeSerializer
    ) { }

    public async Task<GetQueryRes> ProcessAsync(GetQueryCmd cmd)
    {
        try
        {
            if (cmd is null || cmd.QueryPath is null) throw new InvalidOperationException();

            var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/queries/{cmd.QueryPath}?api-version={this._ApiVersion}&$expand=wiql");

            var req = HttpRequestMessageFactory.Create(url);

            using var httpClient = this.CreateHttpClient();
            var resp = await httpClient.SendAsync(req);
            var respContent = await resp.Content.ReadAsStringAsync();

            if(resp.IsSuccessStatusCode)
            {
                var extResp = this._TypeSerializer.Deserialize<ExtQueryResp>(respContent);
                return new GetQueryRes { ExtResp = extResp, IsSuccessful = true };
            }

            return new GetQueryRes { ExtResp = null, IsSuccessful = false, ExceptionInfo = respContent };
        }
        catch (Exception ex)
        {
            return new GetQueryRes { ExtResp = null, IsSuccessful = false, ExceptionInfo = ex.StackTrace };
        }
    }
}


