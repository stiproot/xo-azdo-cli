namespace Xo.AzDO.Engine.Models;

public class UpdateWiCmd : BaseWiCmd, IProcessorCmd
{
    public int id { get; init; }
    public int complete { get; init; }
}