internal class QueryFolderWorkflow : BaseWorkflow, IWorkflow<CreateFolderCmd>
{
	public QueryFolderWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFunctitect functitect
	) : base(nodeBuilderFactory, functitect) { }

	// internal class _NotNullBinaryBranchNodePathResolver : IBinaryBranchNodePathResolver
	// {
	// public bool Resolve(IMsg? msg) => msg is not null;
	// }

	public INode Init(
		IWorkflowContext context,
		CreateFolderCmd cmd
	)
	{
		return this._NodeBuilderFactory.Binary(context)
			.IsNotNull<IProcessor<GetFolderCmd, FolderRes>>()
			.AddFalse<IProcessor<CreateFolderCmd, FolderRes>, CreateFolderCmd>(args: cmd, requiresResult: false)
			.AddArg(new GetFolderCmd { FolderName = cmd.FolderName, QueryFolderPath = cmd.QueryFolderPath })
			.Build();
	}
}
