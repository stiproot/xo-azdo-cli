namespace Xo.AzDO.Cli.Processors;

internal abstract class BaseHttpProcessor
{
	protected readonly IHttpClientFactory _HttpClientFactory;
	protected readonly ITypeSerializer _TypeSerializer;
	protected readonly string _ApiVersion;
	protected const string BASE_URL = "https://dev.azure.com/Derivco";
	protected const string PROJECT_NAME = "Software";

	public BaseHttpProcessor(
		IHttpClientFactory httpClientFactory,
		string apiVersion,
		ITypeSerializer typeSerializer
	)
	{
		this._HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		this._ApiVersion = apiVersion ?? throw new ArgumentNullException(nameof(apiVersion));
		this._TypeSerializer = typeSerializer ?? throw new ArgumentNullException(nameof(typeSerializer));
	}

	protected HttpClient CreateHttpClient() => this._HttpClientFactory.CreateClient("AzureHttpClient");
}
