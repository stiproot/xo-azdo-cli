namespace Xo.AzDO.Cli.Providers;

internal class CreateWiCmdProvider : IProvider<IEnumerable<CreateWiCmd>>
{
    private readonly IJsonProvider _jsonProvider;

    public CreateWiCmdProvider(IJsonProvider jsonProvider) => this._jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));

    // Example...
    public IEnumerable<CreateWiCmd> Provide()
        => this._jsonProvider.GetContent<IEnumerable<CreateWiCmd>>(@"Import\import.json");
}

