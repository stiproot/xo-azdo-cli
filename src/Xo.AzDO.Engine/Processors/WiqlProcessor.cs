namespace Xo.AzDO.Engine.Processors;

public class WiqlProcessor : IProcessor<BuildWiqlCmd, WiqlRes>
{
	public async Task<WiqlRes> ProcessAsync(BuildWiqlCmd cmd) => await Task.FromResult(new WiqlRes { WiQuery = this.BuildQry(cmd) });

	private string BuildQry(BuildWiqlCmd cmd)
	{
		string qry =
			$"SELECT {string.Join(",", cmd.Columns)} " +
			$"FROM {cmd.Table} " +
			"WHERE " +
				cmd.Mode switch
				{
					"Recursive" => string.Join(" AND ", cmd.Conditions.GroupBy(c => c.GroupingKey).Select(g => $"({string.Join(" AND ", g.Select(c => $"{c.Column} {c.Operator} {c.Condition}"))}) ")) + "MODE (Recursive)",
					_ => string.Join(" AND ", cmd.Conditions.Select(c => $"{c.Column} {c.Operator} {c.Condition}"))
				};

		return qry;
	}
}

public class QryCondition
{
	public string Column { get; init; }
	public string Operator { get; init; } = "=";
	public string Condition { get; set; }
	public uint GroupingKey { get; init; } = 0;
}
