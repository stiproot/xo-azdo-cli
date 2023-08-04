namespace Xo.AzDO.Cli.Processors;

internal class QueryByWiqlProcessor : BaseHttpProcessor, IProcessor<QueryByWiqlCmd, QueryByWiqlRes>
{
	private readonly IProcessor<BuildWiqlCmd, WiqlRes> _wiqlProcessor;
	private const string API_VERSION = "7.0";

	public QueryByWiqlProcessor(
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

	public async Task<QueryByWiqlRes> ProcessAsync(QueryByWiqlCmd cmd)
	{
		try
		{
			if(cmd is null) throw new InvalidOperationException();
			if(cmd.BuildWiqlCmd is null && cmd.Query is null) throw new InvalidOperationException();

			var url = new Uri($"{BASE_URL}/{PROJECT_NAME}/_apis/wit/wiql?api-version={this._ApiVersion}");

			string wiq = cmd.Query ?? (this._wiqlProcessor.ProcessAsync(cmd.BuildWiqlCmd!).Result.WiQuery);

			var reqContent = this._TypeSerializer.Serialize(new ExtQueryByWiqlReq
			{
				Query = wiq
			});

			var req = HttpRequestMessageFactory.Create(url, reqContent, HttpMethod.Post);

			using var httpClient = this.CreateHttpClient();
			var resp = await httpClient.SendAsync(req);
			resp.EnsureSuccessStatusCode();
			var respContent = await resp.Content.ReadAsStringAsync();

			Console.WriteLine(respContent);

			return new QueryByWiqlRes { ExtResp = this._TypeSerializer.Deserialize<ExtQueryByWiqlResp>(respContent) };
		}
		catch (Exception ex)
		{
			Console.WriteLine(this._TypeSerializer.Serialize(ex));
			throw;
		}
	}
}


