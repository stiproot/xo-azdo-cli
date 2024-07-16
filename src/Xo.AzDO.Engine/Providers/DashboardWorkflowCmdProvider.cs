namespace Xo.AzDO.Engine.Providers;

public class DashboardWorkflowCmdProvider : IProvider<CreateDashboardWorkflowCmd>
{
    public CreateDashboardWorkflowCmd Provide() => new()
    {
        IterationPath = "Sprint 14 2024",
        TeamName = "",
        DashboardName = "",
        QueryFolderBasePath = "",
        Initiatives = new List<Initiative>
        {
            new()
            {
                Title = "",
                Desc = "",
                Tag = "",
            }
        }
    };
}