namespace Xo.AzDO.Cli.Models.External.Request;

internal class BaseWidgetBuilder : IWidgetBuilder
{
	protected readonly Widget _Widget;

	public BaseWidgetBuilder(
		string name,
		// string settings,
		string contributionId,
		string eTag,
		string? configurationContributionId = null
	)
	{
		this._Widget = new Widget(
			name,
			// settings,
			new SettingsVersion(),
			contributionId,
			eTag,
			configurationContributionId: configurationContributionId
		);
	}

	public IWidgetBuilder AddName(string name)
	{
		this._Widget.name = name;
		return this;
	}

	public IWidgetBuilder AddDimensions((int y, int x, int h, int w) dimensions) => this
		.AddPosition(dimensions.y, dimensions.x)
		.AddSize(dimensions.h, dimensions.w);

	public IWidgetBuilder AddPosition(int row, int col)
	{
		this._Widget.position = new Position { row = row, column = col };
		return this;
	}

	public IWidgetBuilder AddSize(int rowSpan, int colSpan)
	{
		this._Widget.size = new Size { rowSpan = rowSpan, columnSpan = colSpan };
		return this;
	}

	public IWidgetBuilder AddSettings(string settings)
	{
		this._Widget.settings = settings;
		return this;
	}

	public virtual Widget Build() => this._Widget;
}