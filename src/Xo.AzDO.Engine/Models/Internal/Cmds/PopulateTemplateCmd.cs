namespace Xo.AzDO.Engine.Models;

public class PopulateTemplateCmd : IProcessorCmd
{
	public string Template { get; init; }
	public IDictionary<string, string> Values { get; init; }
}