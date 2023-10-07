namespace Xo.AzDO.Engine.Abstractions;

public interface IWidgetBuilderFactory
{
	IWidgetBuilder Create(
		WidgetTypes type,
		string name
	);
}
