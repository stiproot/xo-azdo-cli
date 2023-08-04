namespace Xo.AzDO.Cli.Models;

internal class GetWiCmd : IProcessorCmd
{
	public int Id { get; init; }
	public ExtWiResp ExtResp { get; set; }
}
