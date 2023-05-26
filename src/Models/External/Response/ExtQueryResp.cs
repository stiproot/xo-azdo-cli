namespace Xo.AzDO.Cli.Models.External.Response;

internal sealed class ExtQueryResp
{
	[JsonProperty("id")]
	public string Id { get; init; }

	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("path")]
	public string Path { get; init; }

	[JsonProperty("createdBy")]
	public User CreatedBy { get; init; }

	[JsonProperty("createdDate")]
	public DateTime CreatedDate { get; init; }

	[JsonProperty("lastModifiedBy")]
	public User LastModifiedBy { get; init; }

	[JsonProperty("lastModifiedDate")]
	public DateTime LastModifiedDate { get; init; }

	[JsonProperty("isFolder")]
	public bool IsFolder { get; init; }

	[JsonProperty("hasChildren")]
	public bool HasChildren { get; init; }

	[JsonProperty("isPublic")]
	public bool IsPublic { get; init; }

	[JsonProperty("_links")]
	public Links Links { get; init; }

	[JsonProperty("url")]
	public string Url { get; init; }
}

public class User
{
	[JsonProperty("id")]
	public string Id { get; init; }

	[JsonProperty("name")]
	public string Name { get; init; }

	[JsonProperty("displayName")]
	public string DisplayName { get; init; }

	[JsonProperty("url")]
	public string Url { get; init; }

	[JsonProperty("_links")]
	public UserLinks Links { get; init; }

	[JsonProperty("uniqueName")]
	public string UniqueName { get; init; }

	[JsonProperty("imageUrl")]
	public string ImageUrl { get; init; }

	[JsonProperty("descriptor")]
	public string Descriptor { get; init; }
}

internal sealed class Links
{
	[JsonProperty("self")]
	public Link Self { get; init; }

	[JsonProperty("html")]
	public Link Html { get; init; }

	[JsonProperty("parent")]
	public Link Parent { get; init; }
}

public class UserLinks
{
	[JsonProperty("avatar")]
	public Link Avatar { get; init; }
}

public class Link
{
	[JsonProperty("href")]
	public string Href { get; init; }
}
