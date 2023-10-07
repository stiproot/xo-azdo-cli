namespace Xo.AzDO.Cli.Models;

internal class GetIterationDetailsCmd : IProcessorCmd
{
	public string IterationId { get; init; }
}