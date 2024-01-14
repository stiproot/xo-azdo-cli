namespace Xo.AzDO.Engine.Models;

public abstract class BaseWiCmd
{
    public string? type { get; init; }
    public string? title { get; init; }
    public string? description { get; init; }
    public string? acceptance_criteria { get; init; }
    public string? area_path { get; init; }
    public string? iteration_path { get; init; }
    public string? tags { get; init; }
    public string? assigned_to { get; init; }
    public string? state { get; init; }
    public string? remaining { get; init; }
    public string? original_estimate { get; init; }
    public string? story_points { get; init; }
    public string? history { get; init; }
    public ExtWiResp? ExtResp { get; set; }
}