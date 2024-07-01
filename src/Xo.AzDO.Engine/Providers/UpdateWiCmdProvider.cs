namespace Xo.AzDO.Engine.Providers;

public class UpdateWiCmdProvider : IProvider<UpdateWiCmd>
{
    public UpdateWiCmd Provide()
    {
        return new UpdateWiCmd
        {
            id = 1,
            complete = 21,
            history = "(Automated) update of % complete"
        };
    }
}

