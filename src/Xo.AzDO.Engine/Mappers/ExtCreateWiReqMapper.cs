namespace Xo.AzDO.Engine.Mappers;

public sealed class ExtCreateWiReqMapper : IMapper<CreateWiCmd, ExtWiReq>
{
	private static IDictionary<string, string> PropertyNameMappings = new Dictionary<string, string>
	{
		{ nameof(CreateWiCmd.title), "/fields/System.Title" },
		{ nameof(CreateWiCmd.tags), "/fields/System.Tags" },
		{ nameof(CreateWiCmd.description), "/fields/System.Description" },
		{ nameof(CreateWiCmd.assigned_to), "/fields/System.AssignedTo" },
		{ nameof(CreateWiCmd.acceptance_criteria), "/fields/Microsoft.VSTS.Common.AcceptanceCriteria" },
		{ nameof(CreateWiCmd.area_path), "/fields/System.AreaPath" },
		{ nameof(CreateWiCmd.iteration_path), "/fields/System.IterationPath" },
		{ nameof(CreateWiCmd.state), "/fields/System.State" },
		{ nameof(CreateWiCmd.story_points), "/fields/Microsoft.VSTS.Scheduling.StoryPoints" },
		{ nameof(CreateWiCmd.original_estimate), "/fields/Microsoft.VSTS.Scheduling.OriginalEstimate" },
		{ nameof(CreateWiCmd.remaining), "/fields/Microsoft.VSTS.Scheduling.RemainingWork" },
		{ nameof(CreateWiCmd.completed), "/fields/Microsoft.VSTS.Scheduling.CompletedWork" },
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
				value = (string)propInfo.GetValue(cmd)!
			}
		};
	}

	public ExtWiReq Map(CreateWiCmd cmd)
	{
		var payload =
			cmd
				.GetType()
				.GetProperties()
				.Where(p => PropsToExclude.Contains(p.Name) is false)
				.Where(p => p.GetValue(cmd) is not null)
				.Select(p => PropNameSwitch(p, cmd));

		return new ExtWiReq { workitem_payload = payload };
	}
}
