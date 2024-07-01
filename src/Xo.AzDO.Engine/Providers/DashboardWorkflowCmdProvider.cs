namespace Xo.AzDO.Engine.Providers;

public class DashboardWorkflowCmdProvider : IProvider<CreateDashboardWorkflowCmd>
{
    public CreateDashboardWorkflowCmd Provide() => new()
    {
        IterationPath = "",
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
