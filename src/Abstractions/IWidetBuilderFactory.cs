namespace Xo.AzDO.Cli.Abstractions;

internal interface IWidgetBuilderFactory
{
	IWidgetBuilder Create(
		WidgetTypes type,
		string name
	);
}
