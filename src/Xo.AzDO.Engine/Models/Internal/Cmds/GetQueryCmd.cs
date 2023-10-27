namespace Xo.AzDO.Engine.Models;

public class GetQueryCmd : IProcessorCmd
{
    public string QueryPath { get; init; } = string.Empty;
}
