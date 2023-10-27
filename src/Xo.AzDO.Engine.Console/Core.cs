namespace Xo.AzDO.Engine.Console;

public static class Core
{
    public static void CreateQuery()
    {
        var provider = ServiceProviderFactory.Create();
        var cmd = provider.GetServiceType<IProvider<QueryCmd>>().Provide();
        var processor = provider.GetServiceType<IProcessor<QueryCmd, QueryRes>>();

        processor.ProcessAsync(cmd).Wait();
    }

    public static void WorkItems()
    {
        var provider = ServiceProviderFactory.Create();
        var cmds = provider.GetServiceType<IProvider<IEnumerable<CreateWiCmd>>>().Provide();
        var processor = provider.GetServiceType<IProcessor<CreateWiCmd, WiRes>>();

        Task.WhenAll(cmds.Select(c => processor.ProcessAsync(c))).Wait();
    }

    public static void CloneWorkItem()
    {
        var provider = ServiceProviderFactory.Create();
        var cmd = provider.GetServiceType<IProvider<CloneWiCmd>>().Provide();
        var processor = provider.GetServiceType<IProcessor<CloneWiCmd, CloneWiRes>>();

        processor.ProcessAsync(cmd).Wait();
    }

    public static void QueryByWiql()
    {
        var provider = ServiceProviderFactory.Create();
        var cmd = provider.GetServiceType<IProvider<QueryByWiqlCmd>>().Provide();
        var processor = provider.GetServiceType<IProcessor<QueryByWiqlCmd, QueryByWiqlRes>>();

        processor.ProcessAsync(cmd).Wait();
    }

    public static void Dashboard()
    {
        var provider = ServiceProviderFactory.Create();
        var cmd = provider.GetServiceType<IProvider<CreateDashboardWorkflowCmd>>().Provide();
        var processor = provider.GetServiceType<IProcessor<CreateDashboardWorkflowCmd, DashboardWorkflowRes>>();
        processor.ProcessAsync(cmd).Wait();
    }

    public static void Folder()
    {
        var provider = ServiceProviderFactory.Create();
        var cmd = new CreateFolderCmd 
        { 
            FolderName = "XYZ", 
            QueryFolderPath = "Shared Queries/Customers and Emerging Markets/Rapid Response/N2 Chapmans Peak Project Team/Project Metrics/Dashboard Queries" 
        };
        var processor = provider.GetServiceType<IProcessor<CreateFolderCmd, FolderRes>>();
        processor.ProcessAsync(cmd).Wait();
    }
}
