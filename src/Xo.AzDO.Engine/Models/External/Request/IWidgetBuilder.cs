namespace Xo.AzDO.Engine.Models.External.Request;

public interface IWidgetBuilder
{
	IWidgetBuilder AddName(string name);
	IWidgetBuilder AddDimensions((int y, int x, int h, int w) dimensions);
	IWidgetBuilder AddPosition(int row, int col);
	IWidgetBuilder AddSize(int rowSpan, int colSpan);
	IWidgetBuilder AddSettings(string settings);
	Widget Build();
}