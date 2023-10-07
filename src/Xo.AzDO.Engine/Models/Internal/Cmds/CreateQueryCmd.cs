namespace Xo.AzDO.Cli.Models;

internal class QueryCmd : IProcessorCmd
{
	public string QueryFolderName { get; init; }
	public string QueryFolderPath { get; init; }
	public string QueryName { get; init; }
	// public string Tag { get; init; }
	public BuildWiqlCmd BuildWiqlCmd { get; init; }
}