internal class QueryFolderWorkflow : BaseWorkflow, IWorkflow<CreateFolderCmd>
{
    private readonly IProcessor<CreateFolderCmd, FolderRes> _processor;

    public QueryFolderWorkflow(
        INodeBuilderFactory nodeBuilderFactory,
        IFnFactory fnFactory,
        IStateManager stateManager,
        IProcessor<CreateFolderCmd, FolderRes> processor
    ) : base(nodeBuilderFactory, fnFactory, stateManager)
    {
        this._processor = processor ?? throw new ArgumentNullException(nameof(processor));
    }

    public INode Init(
        IWorkflowContext context,
        CreateFolderCmd cmd
    )
    {

        return this._StateManager
            // todo: update this...
            // .IsNotNull<IProcessor<GetFolderCmd, FolderRes>>()
            .IsNotNull<IProcessor<GetFolderCmd, FolderRes>>(c =>
                c
                    .MatchArg(new GetFolderCmd { FolderName = cmd.FolderName, QueryFolderPath = cmd.QueryFolderPath })
            )
            .Else<IProcessor<CreateFolderCmd, FolderRes>>(configure: c =>
                c
                    .MatchArg(cmd)
            )
            .Build();
    }
}
