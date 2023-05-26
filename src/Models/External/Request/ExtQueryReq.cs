namespace Xo.AzDO.Cli.Models.External.Request;

internal class ExtQueryReq
{
	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("wiql")]
	public string Wiql { get; init; }
}