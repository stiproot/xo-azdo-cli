namespace Xo.AzDO.Cli.Models;

internal class QueryByWiqlCmd : IProcessorCmd
{
	public string? Query { get; init; } 
	public BuildWiqlCmd? BuildWiqlCmd { get; init; }
}
