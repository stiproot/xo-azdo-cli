namespace Xo.AzDO.Engine.Processors;

public abstract class BaseHttpProcessor
{
    protected readonly IHttpClientFactory _HttpClientFactory;
    protected readonly ITypeSerializer _TypeSerializer;
    protected readonly string _ApiVersion;
    protected readonly string BASE_URL;
    protected readonly string PROJECT_NAME;

    public BaseHttpProcessor(
        IHttpClientFactory httpClientFactory,
        string apiVersion,
        ITypeSerializer typeSerializer,
        Config config
    )
    {
        this._HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this._ApiVersion = apiVersion ?? throw new ArgumentNullException(nameof(apiVersion));
        this._TypeSerializer = typeSerializer ?? throw new ArgumentNullException(nameof(typeSerializer));

        var orgName = config?.OrgName ?? throw new ArgumentNullException(nameof(config.OrgName));
        var projectName = config?.ProjectName ?? throw new ArgumentNullException(nameof(config.ProjectName));

        this.BASE_URL = $"https://dev.azure.com/{orgName}";
        this.PROJECT_NAME = projectName;
    }

    protected HttpClient CreateHttpClient() => this._HttpClientFactory.CreateClient("AzureHttpClient");
}
