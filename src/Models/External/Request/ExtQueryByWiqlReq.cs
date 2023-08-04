namespace Xo.AzDO.Cli.Models.External.Request;

internal class ExtQueryByWiqlReq
{
	[JsonProperty("query")]
	public string Query { get; init; }
}
