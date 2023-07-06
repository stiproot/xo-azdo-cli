using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Xo.TaskTree.DependencyInjection.Extensions;

namespace Xo.AzDO.Cli.Extensions;

internal static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection @this)
	{
		// todo: clean this up...
		IConfigurationRoot configurationRoot = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

		ConfigMapper.Map(configurationRoot, out Config config);

		@this.AddHttpClientFactory(config);

		@this.AddLogging(builder =>
		{
			builder.AddConsole();
		});

		@this.TryAddSingleton<IProcessor<CreateDashboardWorkflowCmd, DashboardWorkflowRes>, DashboardWorkflowProcessor>();
		@this.TryAddSingleton<IProcessor<CreateDashboardCmd, DashboardRes>, DashboardProcessor>();
		@this.TryAddSingleton<IProcessor<QueryCmd, QueryRes>, QueryProcessor>();
		@this.TryAddSingleton<IProcessor<CreateFolderCmd, FolderRes>, CreateFolderProcessor>();
		@this.TryAddSingleton<IProcessor<GetFolderCmd, FolderRes>, GetFolderProcessor>();
		@this.TryAddSingleton<IProcessor<BuildWiqlCmd, WiqlRes>, WiqlProcessor>();
		@this.TryAddSingleton<IProcessor<GetProjectDetailsCmd, ProjectRes>, ProjectProcessor>();
		@this.TryAddSingleton<IProcessor<GetTeamDetailsCmd, TeamRes>, TeamProcessor>();
		@this.TryAddSingleton<IProcessor<GetIterationsCmd, IterationsRes>, IterationsProcessor>();
		@this.TryAddSingleton<IProcessor<CreateWiCmd, WiRes>, WiProcessor>();
		@this.TryAddSingleton<IJsonProvider, JsonProvider>();
		@this.TryAddSingleton<ITypeSerializer, TypeSerializer>();
		@this.TryAddSingleton<IProvider<CreateDashboardWorkflowCmd>, DashboardWorkflowCmdProvider>();
		@this.TryAddSingleton<IProvider<IEnumerable<CreateWiCmd>>, CreateWiCmdProvider>();
		@this.TryAddSingleton<IMapper<CreateWiCmd, ExtWiReq>, ExtWiReqMapper>();
		@this.TryAddSingleton<IWidgetBuilderFactory, WidgetBuilderFactory>();
		@this.TryAddSingleton<IWorkflow<CreateFolderCmd>, QueryFolderWorkflow>();
		@this.TryAddSingleton<IWorkflow<CreateDashboardWorkflowCmd>, PrerequisitsWorkflow>();

		@this.AddTaskTreeServices();

		return @this;
	}

	public static T GetServiceType<T>(this IServiceProvider @this) => (T)@this.GetService(typeof(T))!;

	private static IServiceCollection AddHttpClientFactory(this IServiceCollection @this, Config config)
	{
		var base64 = new PatProvider(config).Provide().Base64EncodedToken;

		@this.AddHttpClient("AzureHttpClient", client =>
		{
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64);
		});

		return @this;
	}
}
