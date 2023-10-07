namespace Xo.AzDO.Engine.Models;

public class GetProjectDetailsCmd : IProcessorCmd
{
	public string ProjectId { get; init; }
}