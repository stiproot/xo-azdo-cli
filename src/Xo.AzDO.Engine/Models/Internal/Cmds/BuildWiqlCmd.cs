namespace Xo.AzDO.Cli.Models;

internal class BuildWiqlCmd : IProcessorCmd
{
	public IEnumerable<string> Columns { get; init; }
	public string Table { get; init; } = "WorkItems";
	public IEnumerable<QryCondition> Conditions { get; init; }
	public string Mode { get; init; } = null;
}