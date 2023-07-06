internal class QueryFolderWorkflow : BaseWorkflow, IWorkflow<CreateFolderCmd>
{
	public QueryFolderWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFnFactory fnFactory,
		IStateManager stateManager
	) : base(nodeBuilderFactory, fnFactory, stateManager) { }

	public INode Init(
		IWorkflowContext context,
		CreateFolderCmd cmd
	)
	{
		return this._StateManager
			// todo: update this...
			// .IsNotNull<IProcessor<GetFolderCmd, FolderRes>>()
			.RootIf<IProcessor<GetFolderCmd,FolderRes>>(c => 
				c
					.MatchArg(new GetFolderCmd { FolderName = cmd.FolderName, QueryFolderPath = cmd.QueryFolderPath })
			)
			.Else<IProcessor<CreateFolderCmd, FolderRes>>(configure:c => 
				c
					.MatchArg(cmd)
			)
			.Build();
	}
}
