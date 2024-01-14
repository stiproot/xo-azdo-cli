namespace Xo.AzDO.Engine.Models;

public class WorkItemRelation
{
    public object attributes { get; init; } = new { };
    public string relation_type { get; init; } = string.Empty;
    public string url { get; init; } = string.Empty;
}
