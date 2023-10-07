namespace Xo.AzDO.Engine.Models.External.Request;

public class BurndownChartWidgetSettings
{
	[JsonProperty("totalScopeTrendlineEnabled")]
	public bool TotalScopeTrendlineEnabled { get; init; } = true;

	[JsonProperty("completedWorkEnabled")]
	public bool CompletedWorkEnabled { get; init; } = false;

	[JsonProperty("stackByWorkItemTypeEnabled")]
	public bool StackByWorkItemTypeEnabled { get; init; } = false;

	[JsonProperty("showNonWorkingDays")]
	public bool ShowNonWorkingDays { get; init; } = false;

	[JsonProperty("aggregation")]
	public Aggregation Aggregation { get; init; } = new Aggregation();

	[JsonProperty("workItemTypeFilter")]
	public WorkItemTypeFilter WorkItemTypeFilter { get; init; } = new WorkItemTypeFilter();

	[JsonProperty("timePeriodConfiguration")]
	public TimePeriodConfiguration TimePeriodConfiguration { get; init; } = new TimePeriodConfiguration();

	[JsonProperty("team")]
	public Team Team { get; init; }

	[JsonProperty("iterationId")]
	public string IterationId { get; init; } = "@CurrentIteration";

	[JsonProperty("iterationPath")]
	public string IterationPath { get; init; }

	[JsonProperty("isMinimalChart")]
	public bool IsMinimalChart { get; init; } = false;

	[JsonProperty("isLightboxChart")]
	public bool IsLightboxChart { get; init; } = false;

	[JsonProperty("isLegacy")]
	public bool IsLegacy { get; init; } = false;
}

public class Aggregation
{
	[JsonProperty("identifier")]
	public int Identifier { get; init; } = 1;

	[JsonProperty("settings")]
	public string Settings { get; init; } = "Microsoft.VSTS.Scheduling.RemainingWork";
}

public class WorkItemTypeFilter
{
	[JsonProperty("identifier")]
	public string Identifier { get; init; } = "BacklogCategory";

	[JsonProperty("settings")]
	public string Settings { get; init; } = "Microsoft.TaskCategory";
}

public class TimePeriodConfiguration
{
	[JsonProperty("startDate")]
	public string StartDate { get; init; } = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

	[JsonProperty("endDate")]
	public string EndDate { get; init; } = DateTime.Today.ToString("yyyy-MM-dd");
}

public class Team
{
	[JsonProperty("teamId")]
	public string TeamId { get; init; }

	[JsonProperty("projectId")]
	public string ProjectId { get; init; }
}
