namespace Xo.AzDO.Cli.Models;

internal class PopulateTemplateCmd : IProcessorCmd
{
	public string Template { get; init; }
	public IDictionary<string, string> Values { get; init; }
}