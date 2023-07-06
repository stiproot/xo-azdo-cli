internal class PrerequisitsWorkflow : BaseWorkflow, IWorkflow<CreateDashboardWorkflowCmd>
{
	private readonly IAsyncFn _teamDetailsFn;
	private readonly IAsyncFn _iterationsFn;

	public PrerequisitsWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFnFactory fnFactory,
		IStateManager stateManager
	) : base(
			nodeBuilderFactory, 
			fnFactory, 
			stateManager
	)
	{
		this._teamDetailsFn = this._FnFactory
			.Build(typeof(IProcessor<GetTeamDetailsCmd, TeamRes>))
			.AsAsync();

		this._iterationsFn = this._FnFactory
			.Build(typeof(IProcessor<GetIterationsCmd, IterationsRes>))
			.AsAsync();
	}

	public INode Init(
		IWorkflowContext context,
		CreateDashboardWorkflowCmd cmd
	)
	{
		var getTeamDetails = this._NodeBuilderFactory.Create()
			.Configure(c => 
				c
					.SetId("__team_details__")
					.AddContext(context)
					.AddArg(new GetTeamDetailsCmd { TeamId = cmd.TeamName, ProjectId = "Software" })
				)
			.AddFn(this._teamDetailsFn)
			.Build();

		var getIterations = this._NodeBuilderFactory.Create()
			.Configure(c => 
				c
					.SetId("__team_iterations__")
					.AddContext(context)
					.AddArg(getTeamDetails)
			)
			.AddFn(this._iterationsFn)
			.Build();

		return getIterations;
	}
}
