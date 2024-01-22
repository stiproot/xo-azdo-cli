namespace Xo.AzDO.Engine.Models;

public class UpdateWiHierarchyCmd : IProcessorCmd
{
	public int Id { get; init; } = 0;
	public string Tags { get; init; } = string.Empty;
}