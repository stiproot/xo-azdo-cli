namespace Xo.AzDO.Engine.Mappers;

public sealed class ExtUpdateWiReqMapper : IMapper<UpdateWiCmd, ExtWiReq>
{
	private static IDictionary<string, string> PropertyNameMappings = new Dictionary<string, string>
	{
		{ nameof(UpdateWiCmd.title), "/fields/System.Title" },
		{ nameof(UpdateWiCmd.area_path), "/fields/System.AreaPath" },
		{ nameof(UpdateWiCmd.iteration_path), "/fields/System.IterationPath" },
		{ nameof(UpdateWiCmd.tags), "/fields/System.Tags" },
		{ nameof(UpdateWiCmd.description), "/fields/System.Description" },
		{ nameof(UpdateWiCmd.acceptance_criteria), "/fields/Microsoft.VSTS.Common.AcceptanceCriteria" },
		{ nameof(UpdateWiCmd.state), "/fields/System.State" },
		{ nameof(UpdateWiCmd.assigned_to), "/fields/System.AssignedTo" },
		{ nameof(UpdateWiCmd.remaining), "/fields/Microsoft.VSTS.Scheduling.RemainingWork" },
		{ nameof(UpdateWiCmd.original_estimate), "/fields/Microsoft.VSTS.Scheduling.OriginalEstimate" },
		{ nameof(UpdateWiCmd.story_points), "/fields/Microsoft.VSTS.Scheduling.StoryPoints" },
		{ nameof(UpdateWiCmd.history), "/fields/System.History" },
		{ nameof(UpdateWiCmd.complete), "/fields/Custom.PercentageComplete" },
	};

	private static IEnumerable<string> PropsToExclude = new List<string>
	{
		nameof(UpdateWiCmd.ExtResp),
		nameof(UpdateWiCmd.type),
		nameof(UpdateWiCmd.id)
	};

	private object PropNameSwitch(PropertyInfo propInfo, UpdateWiCmd cmd)
	{
		return propInfo.Name switch
		{
			_ => new
			{
				op = "add",
				path = PropertyNameMappings[propInfo.Name],
				value = propInfo.GetValue(cmd)!.ToString()
			}
		};
	}

	public ExtWiReq Map(UpdateWiCmd cmd)
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
