namespace Xo.AzDO.Engine.Providers;

public class PatProvider : IProvider<Pat>
{
    private readonly Config _config;

    public PatProvider(Config config) => this._config = config ?? throw new ArgumentNullException(nameof(config));

    public Pat Provide()
    {
        string base64 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", this._config.Pat)));
        Console.WriteLine(base64);
        return new Pat { Token = this._config.Pat, Base64EncodedToken = base64 };
    }
}
