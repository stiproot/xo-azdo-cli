using Microsoft.Extensions.DependencyInjection;

namespace Xo.AzDO.Engine.Factories;

public static class ServiceProviderFactory
{
	public static IServiceProvider Create()
		=> ServiceCollectionFactory
				.Create()
				.AddServices()
				.BuildServiceProvider();
}

public static class ServiceCollectionFactory
{
	public static IServiceCollection Create() => new ServiceCollection();
}