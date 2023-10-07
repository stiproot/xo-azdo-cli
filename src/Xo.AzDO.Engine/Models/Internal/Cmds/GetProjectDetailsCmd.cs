namespace Xo.AzDO.Cli.Models;

internal class GetProjectDetailsCmd : IProcessorCmd
{
	public string ProjectId { get; init; }
}