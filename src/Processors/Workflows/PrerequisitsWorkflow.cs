internal class PrerequisitsWorkflow : BaseWorkflow, IWorkflow<CreateDashboardWorkflowCmd>
{
	private readonly IAsyncFunctory _teamDetailsFunctory;
	private readonly IAsyncFunctory _iterationsFunctory;

	public PrerequisitsWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFunctitect functitect
	) : base(nodeBuilderFactory, functitect)
	{
		this._teamDetailsFunctory = this._Functitect
			.Build(typeof(IProcessor<GetTeamDetailsCmd, TeamRes>))
			.AsAsync();

		this._iterationsFunctory = this._Functitect
			.Build(typeof(IProcessor<GetIterationsCmd, IterationsRes>))
			.AsAsync();
	}

	public INode Init(
		IWorkflowContext context,
		CreateDashboardWorkflowCmd cmd
	)
	{
		var getTeamDetails = this._NodeBuilderFactory.Create("__team_details__")
			.AddContext(context)
			.AddFunctory(this._teamDetailsFunctory)
			.AddArg(new GetTeamDetailsCmd { TeamId = cmd.TeamName, ProjectId = "Software", })
			.Build();

		var getIterations = this._NodeBuilderFactory.Create("__team_iterations__")
			.AddContext(context)
			.AddFunctory(this._iterationsFunctory)
			.AddArg(new GetIterationsCmd { TeamName = cmd.TeamName, ProjectId = "Software" })
			.AddArg(getTeamDetails)
			.Build();

		return getIterations;
	}
}
