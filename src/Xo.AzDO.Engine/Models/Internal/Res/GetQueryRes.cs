namespace Xo.AzDO.Engine.Models;

public class GetQueryRes : IProcessorRes
{
	public bool IsSuccessful { get; init; }
	public string? ExceptionInfo { get; init; }
	public ExtQueryResp? ExtResp { get; init; }
}