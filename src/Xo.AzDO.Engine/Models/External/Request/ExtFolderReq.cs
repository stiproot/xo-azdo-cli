namespace Xo.AzDO.Engine.Models.External.Request;

public class ExtFolderReq
{
	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("isFolder")]
	public bool IsFolder { get; init; } = true;
}