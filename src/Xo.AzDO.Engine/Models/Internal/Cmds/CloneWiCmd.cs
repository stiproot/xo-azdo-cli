namespace Xo.AzDO.Cli.Models;

internal class CloneWiCmd : IProcessorCmd
{
	public int Id { get; init; } = 0;
	public int ParentId { get; init; } = 0;
	public string IterationName { get; init; } = string.Empty;
	public string IterationBasePath { get; init; } = string.Empty;
	public string TeamName { get; init; } = string.Empty;
	public string AreaPath { get; init; } = string.Empty;
	public string Tags { get; init; } = string.Empty;
}
