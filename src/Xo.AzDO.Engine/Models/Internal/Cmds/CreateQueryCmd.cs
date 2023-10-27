namespace Xo.AzDO.Engine.Models;

public class QueryCmd : IProcessorCmd
{
	public string QueryFolderPath { get; init; } = string.Empty;
	public string QueryName { get; init; } = string.Empty;
	public BuildWiqlCmd? BuildWiqlCmd { get; init; }
	public string? Wiql { get; init; }
}