namespace Xo.AzDO.Cli.Models;

internal class CreateDashboardCmd : IProcessorCmd
{
	public ExtDashboardReq Req { get; init; }
	public string TeamName { get; init; }
}