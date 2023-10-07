namespace Xo.AzDO.Cli.Models.External.Request;

internal class ExtFolderReq
{
	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("isFolder")]
	public bool IsFolder { get; init; } = true;
}