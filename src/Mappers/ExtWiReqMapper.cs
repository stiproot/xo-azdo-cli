namespace Xo.AzDO.Cli.Mappers;

internal sealed class ExtWiReqMapper : ITypeMapper<CreateWiCmd, ExtWiReq>
{
	private static IDictionary<string, string> PropertyNameMappings = new Dictionary<string, string>
	{
		{ nameof(CreateWiCmd.title), "/fields/System.Title" },
		{ nameof(CreateWiCmd.area_path), "/fields/System.AreaPath" },
		{ nameof(CreateWiCmd.iteration_path), "/fields/System.IterationPath" },
		{ nameof(CreateWiCmd.tags), "/fields/System.Tags" },
		{ nameof(CreateWiCmd.description), "/fields/System.Description" },
		{ nameof(CreateWiCmd.acceptance_criteria), "/fields/Microsoft.VSTS.Common.AcceptanceCriteria" },
		{ nameof(CreateWiCmd.state), "/fields/System.State" },
		{ nameof(CreateWiCmd.assigned_to), "/fields/System.AssignedTo" },
		{ nameof(CreateWiCmd.remaining), "/fields/Microsoft.VSTS.Scheduling.RemainingWork" },
		{ nameof(CreateWiCmd.original_estimate), "/fields/Microsoft.VSTS.Scheduling.OriginalEstimate" },
		{ nameof(CreateWiCmd.story_points), "/fields/Microsoft.VSTS.Scheduling.StoryPoints" },
	};

	private static IDictionary<string, string> PropertyValueMappings = new Dictionary<string, string>
	{
		{ "Child", "System.LinkTypes.Hierarchy-Reverse" }
	};

	private static IEnumerable<string> PropsToExclude = new List<string>
	{
		nameof(CreateWiCmd.ExtResp),
		nameof(CreateWiCmd.type),
		nameof(CreateWiCmd.children)
	};

	private object PropNameSwitch(PropertyInfo propInfo, CreateWiCmd cmd)
	{
		Console.WriteLine(propInfo.Name);
		return propInfo.Name switch
		{
			nameof(CreateWiCmd.relation) => new { op = "add", path = "/relations/-", value = new { rel = PropertyValueMappings[cmd.relation.relation_type], url = cmd.relation.url } },
			_ => new
			{
				op = "add",
				path = PropertyNameMappings[propInfo.Name],
				value = (string)propInfo.GetValue(cmd)
			}
		};
	}

	public ExtWiReq Map(CreateWiCmd cmd)
	{
		var payload =
			cmd
				.GetType()
				.GetProperties()
				.Where(p => !PropsToExclude.Contains(p.Name))
				.Where(p => p.GetValue(cmd) != null)
				.Select(p => PropNameSwitch(p, cmd));

		return new ExtWiReq { workitem_payload = payload };

		//return new ExtWiReq
		//{
		//workitem_payload = new List<Object>
		//{
		//new
		//{
		//op = "add",
		//path = "/fields/System.Title",
		//value = cmd.title
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/System.AreaPath",
		//value = cmd.area_path
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/System.IterationPath",
		//value = cmd.iteration_path
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/System.Tags",
		//value = cmd.tags
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/System.Description",
		//value = cmd.description
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/Microsoft.VSTS.Common.AcceptanceCriteria",
		//value = cmd.acceptance_criteria
		//},
		//new
		//{
		//op = "add",
		//path = "/fields/System.State",
		//value = cmd.state
		//},
		//new
		//{
		//op = "add",
		//path = "/relations/-",
		//value = new
		//{
		//rel = cmd.relation.relation_type switch
		//{
		//"Child"     => "System.LinkTypes.Hierarchy-Reverse",
		//_           => throw new InvalidOperationException()
		//},
		//url = cmd.relation.url
		//}
		//}
		//},
		//workitem_type = cmd.type switch
		//{
		//"User Story" => "User Story",
		//"Feature" => "Feature",
		//"Task" => "Task",
		//_ => throw new InvalidOperationException()
		//}
		//};
	}
}
