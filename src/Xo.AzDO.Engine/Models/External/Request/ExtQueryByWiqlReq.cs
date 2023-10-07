namespace Xo.AzDO.Engine.Models.External.Request;

public class ExtQueryByWiqlReq
{
	[JsonProperty("query")]
	public string Query { get; init; }
}
