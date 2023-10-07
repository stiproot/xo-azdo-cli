public class WitChartWidgetSettings
{
	[JsonProperty("chartType")]
	public string ChartType { get; init; } = "stackBarChart";

	[JsonProperty("groupKey")]
	public string GroupKey { get; init; }

	[JsonProperty("scope")]
	public string Scope { get; init; } = "WorkitemTracking.Queries";

	[JsonProperty("title")]
	public string Title { get; init; }

	[JsonProperty("transformOptions")]
	public TransformOptions TransformOptions { get; init; }

	[JsonProperty("userColors")]
	public List<UserColor> UserColors { get; init; } = new List<UserColor>
	{
		new UserColor { Value = "New", BackgroundColor = "#cccccc" },
		new UserColor { Value = "Active", BackgroundColor = "#007acc" },
		new UserColor { Value = "Closed", BackgroundColor = "#00643a" },
		new UserColor { Value = "Removed", BackgroundColor = "#666666" }
	};

	[JsonProperty("lastArtifactName")]
	public string LastArtifactName { get; init; }
}

public class TransformOptions
{
	[JsonProperty("filter")]
	public string Filter { get; init; }

	[JsonProperty("groupBy")]
	public string GroupBy { get; init; } = "System.State";

	[JsonProperty("orderBy")]
	public OrderBy OrderBy { get; init; }

	[JsonProperty("measure")]
	public Measure Measure { get; init; }

	[JsonProperty("historyRange")]
	public object HistoryRange { get; init; }

	[JsonProperty("series")]
	public string Series { get; init; } = "System.AssignedTo";

	[JsonProperty("groupByTags")]
	public bool GroupByTags { get; init; } = true;

	[JsonProperty("filteredGroups")]
	public List<object> FilteredGroups { get; init; } = new List<object>();
}

public class OrderBy
{
	[JsonProperty("propertyName")]
	public string PropertyName { get; init; } = "label";

	[JsonProperty("direction")]
	public string Direction { get; init; } = "descending";
}

public class Measure
{
	[JsonProperty("aggregation")]
	public string Aggregation { get; init; } = "count";

	[JsonProperty("propertyName")]
	public string PropertyName { get; init; } = string.Empty;
}

public class UserColor
{
	[JsonProperty("value")]
	public string Value { get; init; }

	[JsonProperty("backgroundColor")]
	public string BackgroundColor { get; init; }
}
