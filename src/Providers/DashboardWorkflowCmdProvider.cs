namespace Xo.AzDO.Cli.Providers;

internal class DashboardWorkflowCmdProvider : IProvider<CreateDashboardWorkflowCmd>
{
    public CreateDashboardWorkflowCmd Provide() => new CreateDashboardWorkflowCmd
    {
        IterationName = "Sprint 14 2023",
        IterationBasePath = "Software\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023",
        TeamName = "CEM - N2 Chapmans Peak Project Team",
        DashboardName = "Chapmans Peak Sprint 14 2023",
        QueryFolderBasePath = "Shared Queries/Customers and Emerging Markets/Rapid Response/N2 Chapmans Peak Project Team",
        Initiatives = new List<Initiative>
        {
            new()
            {
                Title = "HDT - Product Enhancements",
                Tag = "N2CP-IS-HelpDesk",
                Desc= "",
                QueryFolderName = "Sprint 14 2023"
            },
            new()
            {
                Title = "Reboot",
                Tag = "N2CP-Reboot",
                Desc = "This goal is to spend 30% of our time towards process improvement, fixing technical debt and approved learning.",
                QueryFolderName = "Sprint 14 2023"
            },
            new()
            {
                Title = "Dreadnought Training",
                Tag = "N2CP-DreadnoughtTraining",
                Desc = "",
                QueryFolderName = "Sprint 14 2023"
            }
        },
    };
}
