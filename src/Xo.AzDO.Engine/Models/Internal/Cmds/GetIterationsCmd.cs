namespace Xo.AzDO.Cli.Models;

internal class GetIterationsCmd : IProcessorCmd
{
    public string TeamName { get; init; }
    public string ProjectId { get; init; }
}
