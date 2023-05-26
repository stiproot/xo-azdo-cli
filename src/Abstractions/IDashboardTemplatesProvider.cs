namespace Xo.AzDO.Cli.Abstractions;

internal interface IDashboardsTemplatesProvider
{
	string GetTemplateContent(DashboardTemplates template);
}
