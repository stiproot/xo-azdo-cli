namespace Xo.AzDO.Engine.Models;

public class CreateDashboardCmd : IProcessorCmd
{
	public ExtDashboardReq Req { get; init; }
	public string TeamName { get; init; }
}