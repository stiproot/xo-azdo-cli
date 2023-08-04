namespace AzureDevOpsClient.Models;

internal class GetWiCmd : IProcessorCmd
{
	public int Id { get; init; }
	public ExtWiResp ExtResp { get; set; }
}
