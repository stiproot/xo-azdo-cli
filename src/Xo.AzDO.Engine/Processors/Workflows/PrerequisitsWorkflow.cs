public class PrerequisitsWorkflow : BaseWorkflow, IWorkflow<CreateDashboardWorkflowCmd>
{
    private readonly IFn _teamDetailsFn;
    private readonly IFn _iterationsFn;

    private readonly IProcessor<GetTeamDetailsCmd, TeamRes> _teamDetailsProcessor;
    private readonly IProcessor<GetIterationsCmd, IterationsRes> _iterationsProcessor;
    private readonly Config _config;

    public PrerequisitsWorkflow(
        INodeBuilderFactory nodeBuilderFactory,
        IFnFactory fnFactory,
        IStateManager stateManager,
        IProcessor<GetTeamDetailsCmd, TeamRes> _teamDetailsProcessor,
        IProcessor<GetIterationsCmd, IterationsRes> _iterationsProcessor,
        Config config
    ) : base(
            nodeBuilderFactory,
            fnFactory,
            stateManager
    )
    {
        this._teamDetailsProcessor = _teamDetailsProcessor ?? throw new ArgumentNullException(nameof(_teamDetailsProcessor));
        this._iterationsProcessor = _iterationsProcessor ?? throw new ArgumentNullException(nameof(_iterationsProcessor));
        this._config = config ?? throw new ArgumentNullException(nameof(config));

        // this._teamDetailsFn = this._FnFactory
        //     .Build(typeof(IProcessor<GetTeamDetailsCmd, TeamRes>));
        // this._iterationsFn = this._FnFactory
        //     .Build(typeof(IProcessor<GetIterationsCmd, IterationsRes>));
    }

    public INode Init(
        IWorkflowContext context,
        CreateDashboardWorkflowCmd cmd
    )
    {
        var teamDetails = this._teamDetailsProcessor.ProcessAsync(new GetTeamDetailsCmd { TeamId = cmd.TeamName, ProjectId = this._config.ProjectName }).Result;
        context.AddData("__team_details__", teamDetails);

        var iterations = this._iterationsProcessor.ProcessAsync(new GetIterationsCmd { TeamName = cmd.TeamName, ProjectId = this._config.ProjectName }).Result;

        context.AddData("__team_iterations__", iterations);


        // var getTeamDetails = this._NodeBuilderFactory.Create()
        //     .Configure(c =>
        //         c
        //             .SetId("__team_details__")
        //             .AddContext(context)
        //             .MatchArg(new GetTeamDetailsCmd { TeamId = cmd.TeamName, ProjectId = "Software" })
        //         )
        //     .AddFn(this._teamDetailsFn)
        //     .Build();

        // var getIterations = this._NodeBuilderFactory.Create()
        //     .Configure(c =>
        //         c
        //             .SetId("__team_iterations__")
        //             .AddContext(context)
        //             .AddArg(getTeamDetails)
        //     )
        //     .AddFn(this._iterationsFn)
        //     .Build();

        return null;
    }
}
