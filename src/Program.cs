namespace Xo.AzDO.Cli;

class Program
{
	static void WorkItems()
	{
		var provider = ServiceProviderFactory.Create();
		var cmds = provider.GetServiceType<IProvider<IEnumerable<CreateWiCmd>>>().Provide();
		var processor = provider.GetServiceType<IProcessor<CreateWiCmd, WiRes>>();

		Task.WhenAll(cmds.Select(c => processor.ProcessAsync(c))).Wait();
	}

	static void Dashboard()
	{
		var provider = ServiceProviderFactory.Create();
		var cmd = provider.GetServiceType<IProvider<CreateDashboardWorkflowCmd>>().Provide();
		var processor = provider.GetServiceType<IProcessor<CreateDashboardWorkflowCmd, DashboardWorkflowRes>>();
		processor.ProcessAsync(cmd).Wait();
	}

	static void Folder()
	{
		var cancellationToken = new CancellationToken();
		var provider = ServiceProviderFactory.Create();
		var workflowContextFactory = provider.GetServiceType<IWorkflowContextFactory>();
		var context = workflowContextFactory.Create();
		var cmd = new CreateFolderCmd { FolderName = "Trx", QueryFolderPath = "Shared Queries/Customers and Emerging Markets/Rapid Response/N2 Chapmans Peak Project Team" };
		var workflow = provider.GetServiceType<IWorkflow<CreateFolderCmd>>();
		var msg = workflow.Init(context, cmd).Run(cancellationToken).Result;

		Console.WriteLine(msg);
	}

	static void Main(string[] args)
	{
		// WorkItems();
		// Folder();
		Dashboard();
	}
}
