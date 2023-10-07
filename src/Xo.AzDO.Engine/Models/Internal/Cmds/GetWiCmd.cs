namespace Xo.AzDO.Engine.Models;

public class GetWiCmd : IProcessorCmd
{
	public int Id { get; init; }
	public ExtWiResp ExtResp { get; set; }
}
