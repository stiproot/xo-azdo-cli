namespace Xo.AzDO.Engine.Models;

public class CloneWiCmd : IProcessorCmd
{
	public int Id { get; init; } = 0;
	public int ParentId { get; init; } = 0;
	public string IterationPath { get; init; } = string.Empty;
	public string AreaPath { get; init; } = string.Empty;
	public string Tags { get; init; } = string.Empty;
}
