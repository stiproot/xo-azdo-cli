namespace Xo.AzDO.Engine.Providers;

public class UpdateWiCmdProvider : IProvider<UpdateWiCmd>
{
    public UpdateWiCmd Provide()
    {
        return new UpdateWiCmd
        {
            id = 1160264,
            complete = 20,
            history = "(Automated) update of % complete"
        };
    }
}

