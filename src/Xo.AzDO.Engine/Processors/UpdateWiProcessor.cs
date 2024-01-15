namespace Xo.AzDO.Engine.Processors;

public class UpdateWiProcessor : BaseHttpProcessor, IProcessor<UpdateWiCmd, UpdateWiRes>
{
    private readonly IMapper<UpdateWiCmd, ExtWiReq> _extReqMapper;
    const string API_VERSION = "7.2-preview.3";

    public UpdateWiProcessor(
        IHttpClientFactory httpClientFactory,
        ITypeSerializer typeSerializer,
        IMapper<UpdateWiCmd, ExtWiReq> typeMapper
    ) : base(
                httpClientFactory,
                API_VERSION,
                typeSerializer
    )
        => this._extReqMapper = typeMapper ?? throw new ArgumentNullException(nameof(typeMapper));

    public async Task<UpdateWiRes> ProcessAsync(UpdateWiCmd cmd)
    {
        await this.CoreProcessAsync(cmd);
        return new UpdateWiRes { Cmd = cmd };
    }

    private async Task CoreProcessAsync(UpdateWiCmd cmd)
    {
		// https://dev.azure.com/Derivco/Software/_apis/wit/workitems/1160264?api-version=7.2-preview.3

        var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/workitems/{cmd.id}?api-version={this._ApiVersion}");
        var reqContent = this._TypeSerializer.Serialize(this._extReqMapper.Map(cmd).workitem_payload);
        Console.WriteLine(reqContent);
        var httpReq = HttpRequestMessageFactory.Create(url, reqContent, HttpMethod.Patch, mediaType: "application/json-patch+json");

        using var httpClient = this.CreateHttpClient();
        var resp = await httpClient.SendAsync(httpReq);
        var respContent = await resp.Content.ReadAsStringAsync();
        Console.WriteLine(respContent);
        resp.EnsureSuccessStatusCode();

        var type = this._TypeSerializer.Deserialize<ExtWiResp>(respContent);
        cmd.ExtResp = type;
    }
}