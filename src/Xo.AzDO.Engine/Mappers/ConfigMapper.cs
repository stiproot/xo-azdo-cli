using Microsoft.Extensions.Configuration;

namespace Xo.AzDO.Engine.Mappers;

public class ConfigMapper
{
	public static void Map(IConfigurationRoot configurationRoot, out Config config)
	{
		var pat = configurationRoot.GetSection("secrets")["pat"];
		var orgName = configurationRoot.GetSection("azdo")["orgName"];
		var projectName = configurationRoot.GetSection("azdo")["projectName"];

		config = new Config
		{
			Pat = pat,
			OrgName = orgName,
			ProjectName = projectName
		};
	}
}