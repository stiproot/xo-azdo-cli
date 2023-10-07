namespace Xo.AzDO.Engine.Models;

public class GetFolderCmd : IProcessorCmd
{
	public string FolderName { get; init; }
	public string QueryFolderPath { get; init; }
}