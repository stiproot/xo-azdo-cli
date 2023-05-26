namespace Xo.AzDO.Cli.Models;

internal class GetTeamDetailsCmd : IProcessorCmd
{
	public string ProjectId { get; init; }
	public string TeamId { get; init; }
}