namespace Xo.AzDO.Engine.Models;

public class WitViewWidgetSettings
{
	[JsonProperty("query")]
	public Query Query { get; init; }

	[JsonProperty("selectedColumns")]
	public IEnumerable<Column> SelectedColumns { get; init; } = new List<Column>
	{
		new() { Name = "Title", ReferenceName = "System.Title" },
		new() { Name = "Assigned To", ReferenceName = "System.AssignedTo" },
		new() { Name = "State", ReferenceName = "System.State" }
	};
}

public class Query
{
	[JsonProperty("queryId")]
	public string QueryId { get; init; }

	[JsonProperty("queryName")]
	public string QueryName { get; init; }
}

public class Column
{
	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("referenceName")]
	public string ReferenceName { get; init; }
}
