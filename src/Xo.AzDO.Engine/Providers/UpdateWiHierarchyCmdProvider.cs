namespace Xo.AzDO.Engine.Providers;

public class UpdateWiHierarchyCmdProvider : IProvider<UpdateWiHierarchyCmd>
{
    public UpdateWiHierarchyCmd Provide() => new()
    {
        Id = 777,
        Tags = "some-tag"
    };
}
