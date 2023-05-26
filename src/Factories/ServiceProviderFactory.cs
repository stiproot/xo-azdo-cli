using Microsoft.Extensions.DependencyInjection;

namespace Xo.AzDO.Cli.Factories;

internal static class ServiceProviderFactory
{
	public static IServiceProvider Create()
		=> ServiceCollectionFactory
				.Create()
				.AddServices()
				.BuildServiceProvider();
}

internal static class ServiceCollectionFactory
{
	public static IServiceCollection Create() => new ServiceCollection();
}