namespace Xo.AzDO.Engine.Models;

public class CreateFolderCmd : IProcessorCmd
{
	public string FolderName { get; init; }
	public string QueryFolderPath { get; init; }
}