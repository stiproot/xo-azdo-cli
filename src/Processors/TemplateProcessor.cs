namespace Xo.AzDO.Cli.Processors;

// todo: remove?...
internal class TemplateProcessor : IProcessor<PopulateTemplateCmd, TemplateRes>
{
	public async Task<TemplateRes> ProcessAsync(PopulateTemplateCmd cmd)
	{
		// todo: optimize...
		string output = cmd.Template;
		foreach (var p in cmd.Values)
		{
			output = output.Replace(p.Key, p.Value);
		}
		return new TemplateRes { Template = output };
	}
}