
namespace AzureDevOpsClient.Providers;

internal class CloneWiCmdProvider : IProvider<CloneWiCmd>
{
	public CloneWiCmd Provide() => new CloneWiCmd
	{
		Id = 1066390,
		ParentId = 1057034,
		IterationName = "Sprint 12 2023",
		IterationBasePath = "Software\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023",
		TeamName = "CEM - N2 Chapmans Peak Project Team",
		AreaPath = "Software\\Customers and Emerging Markets\\Rapid Response\\Project\\N2 Chapmans Peak Project Team",
		Tags = "WorkItemCreationTool-Clone",
	};
}
