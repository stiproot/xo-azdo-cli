namespace Xo.AzDO.Engine.Models;

public class BuildWidgetCmd : IProcessorCmd
{
	public string Name { get; init; }
	public int RowSpan { get; init; }
	public int ColSpan { get; init; }
	public int Row { get; init; }
	public int Column { get; init; }
	public string Settings { get; init; }
	public string ContributionId { get; init; }
	public string ContributionConfigurationId { get; init; }
	public string ETag { get; init; }
}