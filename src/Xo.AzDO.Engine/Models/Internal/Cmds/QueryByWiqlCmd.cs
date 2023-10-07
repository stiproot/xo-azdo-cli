namespace Xo.AzDO.Engine.Models;

public class QueryByWiqlCmd : IProcessorCmd
{
	public string? Query { get; init; } 
	public BuildWiqlCmd? BuildWiqlCmd { get; init; }
}
