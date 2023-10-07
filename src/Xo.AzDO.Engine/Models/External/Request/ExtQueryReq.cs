namespace Xo.AzDO.Engine.Models.External.Request;

public class ExtQueryReq
{
	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("wiql")]
	public string Wiql { get; init; }
}