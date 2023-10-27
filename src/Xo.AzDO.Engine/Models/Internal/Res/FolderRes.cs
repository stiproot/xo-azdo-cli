namespace Xo.AzDO.Engine.Models;

public class FolderRes : IProcessorRes
{
	public ExtQueryResp? ExtResp { get; init; }
	public bool IsSuccessful { get; init; }
	public string? ExceptionInfo { get; init; }
}