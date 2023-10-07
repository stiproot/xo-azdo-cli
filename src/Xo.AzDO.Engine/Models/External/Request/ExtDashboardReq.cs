namespace Xo.AzDO.Engine.Models.External.Request;

public class ExtDashboardReq
{
	public string Name { get; init; }
	public IEnumerable<Widget> Widgets { get; init; }
}