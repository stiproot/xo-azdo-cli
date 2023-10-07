namespace Xo.AzDO.Cli.Models;

internal class GetFolderCmd : IProcessorCmd
{
	public string FolderName { get; init; }
	public string QueryFolderPath { get; init; }
}