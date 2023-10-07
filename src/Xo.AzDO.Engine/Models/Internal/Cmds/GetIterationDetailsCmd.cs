namespace Xo.AzDO.Engine.Models;

public class GetIterationDetailsCmd : IProcessorCmd
{
	public string IterationId { get; init; }
}