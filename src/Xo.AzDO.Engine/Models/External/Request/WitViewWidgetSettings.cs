namespace Xo.AzDO.Engine.Models;

public class WitViewWidgetSettings
{
	[JsonProperty("query")]
	public Query Query { get; init; }

	[JsonProperty("selectedColumns")]
	public IEnumerable<Column> SelectedColumns { get; init; } = new List<Column>
	{
        new() { Name = "Priority", ReferenceName = "Microsoft.VSTS.Common.Priority" },
        new() { Name = "ID", ReferenceName = "System.Id" },
        new() { Name = "State", ReferenceName = "System.State" },
        new() { Name = "Title", ReferenceName = "System.Title" },
        new() { Name = "Assigned To", ReferenceName = "System.AssignedTo" },
        new() { Name = "Tags", ReferenceName = "System.Tags" },
        new() { Name = "Story Points", ReferenceName = "Microsoft.VSTS.Scheduling.StoryPoints" }
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
