namespace Xo.AzDO.Engine.Providers;

public class CreateQueryCmdProvider : IProvider<QueryCmd>
{
    private readonly Config _config;

    public CreateQueryCmdProvider(Config config)
    {
        this._config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public QueryCmd Provide() => new()
    {
        QueryName = "",
        QueryFolderPath = "",
        Wiql = $"SELECT [System.Id],[Microsoft.VSTS.Common.Priority],[System.State],[System.Title],[System.AssignedTo],[System.Tags],[Microsoft.VSTS.Scheduling.StoryPoints] FROM WorkItemLinks WHERE ([Source].[System.TeamProject] = '{this._config.ProjectName}' AND [Source].[System.WorkItemType] <> '' AND [Source].[System.IterationPath] = '{this._config.ProjectName}\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023\\Sprint 13 2023' AND [Source].[System.Tags] CONTAINS 'N2CP - Reboot' AND [Source].[Microsoft.VSTS.Scheduling.StoryPoints] > 0)  AND ([System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward')  AND ([Target].[System.TeamProject] = '{this._config.ProjectName}' AND [Target].[System.WorkItemType] <> '' AND [Target].[System.Tags] CONTAINS 'N2CP - Reboot' AND [Target].[Microsoft.VSTS.Scheduling.RemainingWork] > 0 AND [Target].[Microsoft.VSTS.Scheduling.OriginalEstimate] > 0) MODE (Recursive)"
    };
}
