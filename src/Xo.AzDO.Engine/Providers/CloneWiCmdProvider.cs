namespace Xo.AzDO.Engine.Providers;

public class CloneWiCmdProvider : IProvider<CloneWiCmd>
{
    public CloneWiCmd Provide() => new()
    {
        Id = 1,
        ParentId = 1,
        AreaPath = "",
        IterationPath = "",
        Tags = ""
    };
}
