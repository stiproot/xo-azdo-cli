namespace Xo.AzDO.Engine.Abstractions;

public interface IDashboardsTemplatesProvider
{
	string GetTemplateContent(DashboardTemplates template);
}
