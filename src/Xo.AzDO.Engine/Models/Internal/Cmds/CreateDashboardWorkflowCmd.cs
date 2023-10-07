namespace Xo.AzDO.Engine.Models;

public class CreateDashboardWorkflowCmd : IProcessorCmd
{
	public string IterationName { get; init; }
	public string IterationBasePath { get; init; }
	public IEnumerable<Initiative> Initiatives { get; init; }
	public string TeamName { get; init; }
	public string DashboardName { get; init; }
	public string QueryFolderBasePath { get; init; }
}

public class Initiative
{
	public string Title { get; init; }
	public string Desc { get; init; }
	public string Tag { get; init; }
	public IDictionary<string, string> Links { get; init; } = new Dictionary<string, string>();
	public string QueryFolderName { get; init; }
	public string Markdown =>
		$"#{this.Title}\n" +
		$"{this.Desc}\n" +
		$"Tag: {this.Tag}\n" +
		string.Join("", this.Links.Select(l => $"- [{l.Key}]({l.Value})\n"));
}
