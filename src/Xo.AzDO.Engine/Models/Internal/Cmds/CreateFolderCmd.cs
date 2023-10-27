namespace Xo.AzDO.Engine.Models;

public class CreateFolderCmd : IProcessorCmd
{
	public string FolderName { get; init; } = string.Empty;
	public string QueryFolderPath { get; init; } = string.Empty;
}