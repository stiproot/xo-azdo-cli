namespace Xo.AzDO.Engine.Models;

public class UpdateWiCmd : BaseWiCmd, IProcessorCmd
{
    public int id { get; init; }
    public float complete { get; init; }
}