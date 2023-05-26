namespace Xo.AzDO.Cli.Abstractions;

internal class WidgetBuilderFactory : IWidgetBuilderFactory
{
	public IWidgetBuilder Create(
		WidgetTypes type,
		string name
	) => type switch
	{
		WidgetTypes.MarkdownWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.MarkdownWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.MarkdownWidget.Configuration",
			eTag: "21"
		),
		WidgetTypes.SprintCapacityWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.SprintCapacityWidget",
			eTag: "1"
		),
		WidgetTypes.SprintOverviewWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.SprintOverviewWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.SprintOverviewWidget.Configuration",
			eTag: ""
		),
		WidgetTypes.TeamMembersWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.TeamMembersWidget",
			eTag: ""
		),
		WidgetTypes.VelocityWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.VelocityWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.VelocityWidget.Configuration",
			eTag: ""
		),
		WidgetTypes.NewWorkItemWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.NewWorkItemWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.NewWorkItemWidget.Configuration",
			eTag: ""
		),
		WidgetTypes.AnalyticsSprintBurndownWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.AnalyticsSprintBurndownWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.AnalyticsSprintBurndownWidget.Configuration",
			eTag: "1"
		),
		WidgetTypes.QueryScalarWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.QueryScalarWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.QueryScalarWidget.Configuration",
			eTag: "17"
		),
		WidgetTypes.WitChartWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.WitChartWidget",
			configurationContributionId: "ms.vss-dashboards-web.Microsoft.VisualStudioOnline.Dashboards.WitChartWidget.Configuration",
			eTag: "26"
		),
		WidgetTypes.WitViewWidget => new BaseWidgetBuilder(
			name: name,
			contributionId: "ms.vss-mywork-web.Microsoft.VisualStudioOnline.MyWork.WitViewWidget",
			configurationContributionId: "ms.vss-mywork-web.Microsoft.VisualStudioOnline.MyWork.WitViewWidget.Configuration",
			eTag: "17"
		),
		_ => throw new NotImplementedException(),
	};
}