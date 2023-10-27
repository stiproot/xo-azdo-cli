namespace Xo.AzDO.Engine.Providers;

public class DashboardWorkflowCmdProvider : IProvider<CreateDashboardWorkflowCmd>
{
    public CreateDashboardWorkflowCmd Provide() => new CreateDashboardWorkflowCmd
    {
        IterationPath = "Software\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023\\Sprint 14 2023",
        TeamName = "CEM - N2 Chapmans Peak Project Team",
        DashboardName = "Simon Tmp XXX 4",
        QueryFolderBasePath = "Shared Queries/Customers and Emerging Markets/Rapid Response/N2 Chapmans Peak Project Team/Project Metrics/Dashboard Queries",
        Initiatives = new List<Initiative>
        {
            new()
            {
                Title = "N2CP-Reboot",
                Desc = "Rebootish",
                Tag = "N2CP-Reboot",
            }
        }
    };
}
