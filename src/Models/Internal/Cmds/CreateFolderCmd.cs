namespace Xo.AzDO.Cli.Models;

internal class CreateFolderCmd : IProcessorCmd
{
	public string FolderName { get; init; }
	public string QueryFolderPath { get; init; }
}