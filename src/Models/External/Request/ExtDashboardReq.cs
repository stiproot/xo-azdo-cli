namespace Xo.AzDO.Cli.Models.External.Request;

internal class ExtDashboardReq
{
	public string Name { get; init; }
	public IEnumerable<Widget> Widgets { get; init; }
}