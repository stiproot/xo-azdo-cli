using Microsoft.Extensions.Configuration;

namespace Xo.AzDO.Engine.Mappers;

public class ConfigMapper
{
	public static void Map(IConfigurationRoot configurationRoot, out Config config)
	{
		config = new Config();
		configurationRoot.GetSection("secrets").Bind(config);
	}
}