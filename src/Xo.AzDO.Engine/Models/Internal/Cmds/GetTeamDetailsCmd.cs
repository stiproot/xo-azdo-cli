namespace Xo.AzDO.Engine.Models;

public class GetTeamDetailsCmd : IProcessorCmd
{
	public string ProjectId { get; init; }
	public string TeamId { get; init; }
}