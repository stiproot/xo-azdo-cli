namespace Xo.AzDO.Engine.Models;

public class QueryCmd : IProcessorCmd
{
	public string QueryFolderPath { get; init; }
	public string QueryName { get; init; }
	public BuildWiqlCmd BuildWiqlCmd { get; init; }
}