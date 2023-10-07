namespace Xo.AzDO.Engine.Models;

public class QueryScalarWidgetSettings
{
	[JsonProperty("defaultBackgroundColor")]
	public string DefaultBackgroundColor { get; init; } = "#00643a";
	[JsonProperty("queryId")]
	public string QueryId { get; init; }
	[JsonProperty("queryName")]
	public string QueryName { get; init; }
	[JsonProperty("colorRules")]
	public List<object> ColorRules { get; init; } = new List<object>();
}
