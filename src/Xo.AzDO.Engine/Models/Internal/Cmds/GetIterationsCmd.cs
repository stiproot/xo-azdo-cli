namespace Xo.AzDO.Engine.Models;

public class GetIterationsCmd : IProcessorCmd
{
    public string TeamName { get; init; }
    public string ProjectId { get; init; }
}
