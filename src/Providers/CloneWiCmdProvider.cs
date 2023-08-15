namespace Xo.AzDO.Cli.Providers;

internal class CloneWiCmdProvider : IProvider<CloneWiCmd>
{
    public CloneWiCmd Provide() => new CloneWiCmd { };
}
