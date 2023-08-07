namespace Xo.AzDO.Cli.Providers;

internal class QueryByWiqlCmdProvider : IProvider<QueryByWiqlCmd>
{
	public QueryByWiqlCmd Provide() => new QueryByWiqlCmd
	{
		BuildWiqlCmd = new BuildWiqlCmd
		{
			Columns = new List<string>
			{
				"[System.Id]",
				"[System.WorkItemType]",
				"[System.Title]",
				"[System.AssignedTo]",
				"[System.State]",
				"[System.Tags]"
			},
			Table = "WorkItems",
			Conditions = new List<QryCondition>
			{
				new QryCondition { Column = "[System.TeamProject]", Condition = "@project" },
				new QryCondition { Column = "[System.WorkItemType]", Condition = "'Feature'" },
				new QryCondition { Column = "[System.Tags]", Operator = "CONTAINS", Condition = "'N2CP-RACI-Template'" },
				new QryCondition { Column = "[System.Tags]", Operator = "CONTAINS", Condition = "'N2CP-RACI-Template-Development'" },
				new QryCondition { Column = "[System.Tags]", Operator = "CONTAINS", Condition = "'N2CP-RACI-Template-Feature'" },
			}
		}
	};
}
