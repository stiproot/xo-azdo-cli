namespace Xo.AzDO.Engine.Providers;

public class UpdateWiHierarchyCmdProvider : IProvider<UpdateWiHierarchyCmd>
{
    public UpdateWiHierarchyCmd Provide() => new()
    {
        Id = 1220322,
        Tags = "update-hierarchy"
    };
}
