namespace Xo.AzDO.Cli.Models.External.Request;

internal class Widget
{
	// public string id { get; set; }
	public string name { get; set; }
	public Size? size { get; set; }
	public Position? position { get; set; }
	public string? settings { get; set; } = null;
	public SettingsVersion settingsVersion { get; init; } = new SettingsVersion();
	public string contributionId { get; init; }
	public string? configurationContributionId { get; init; } = null;
	public bool isNameConfigurable { get; init; } = true;
	public bool isEnabled { get; init; }
	public string eTag { get; init; }

	public Widget(
		// string id,
		string name,
		SettingsVersion settingsVersion,
		string contributionId,
		string eTag,
		string? settings = null,
		bool isEnabled = true,
		string? configurationContributionId = null,
		Size? size = null,
		Position? position = null
	)
	{
		// this.id = id;
		this.name = name;
		this.settings = settings;
		this.settingsVersion = settingsVersion;
		this.contributionId = contributionId;
		this.eTag = eTag;
		this.isEnabled = isEnabled;
		this.configurationContributionId = configurationContributionId;
		if (size is not null) this.size = size;
		if (position is not null) this.position = position;
	}
}

internal class Size
{
	public int rowSpan { get; init; }
	public int columnSpan { get; init; }
}

internal class Position
{
	public int row { get; init; }
	public int column { get; init; }
}

internal class SettingsVersion
{
	public int major { get; init; } = 1;
	public int minor { get; init; } = 0;
	public int patch { get; init; } = 0;
}