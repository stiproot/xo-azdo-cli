namespace Xo.AzDO.Engine.Processors;

public class DashboardWorkflowProcessorV3 : IProcessor<CreateDashboardWorkflowCmd, DashboardWorkflowRes>
{
    private readonly INodeBuilderFactory _nodeBuilderFactory;
    private readonly IFnFactory _fnFactory;
    private readonly IWorkflowContextFactory _workflowContextFactory;
    private readonly IMsgFactory _msgFactory;
    private readonly ITypeSerializer _typeSerializer;
    private readonly IWidgetBuilderFactory _widgetBuilderFactory;
    private readonly IProcessor<CreateFolderCmd, FolderRes> _createFolderProcessor;
    private readonly IWorkflow<CreateDashboardWorkflowCmd> _prerequisitsWorkflow;
    private readonly IFn _queryFn;
    private readonly IFn _dashboardFn;
    private IDictionary<string, IRectangle> _rootHash;
    private IDictionary<string, IRectangle> _childHash;

    private string Prefix(int i) => $"r-goal-{i}";

    public DashboardWorkflowProcessorV3(
        INodeBuilderFactory nodeBuilderFactory,
        IFnFactory fnFactory,
        IWorkflowContextFactory workflowContextFactory,
        IMsgFactory msgFactory,
        ITypeSerializer typeSerializer,
        IWidgetBuilderFactory widgetBuilderFactory,
        IProcessor<CreateFolderCmd, FolderRes> createFolderProcessor,
        IWorkflow<CreateDashboardWorkflowCmd> prerequisitsWorkflow
    )
    {
        this._nodeBuilderFactory = nodeBuilderFactory ?? throw new ArgumentNullException(nameof(nodeBuilderFactory));
        this._fnFactory = fnFactory ?? throw new ArgumentNullException(nameof(fnFactory));
        this._workflowContextFactory = workflowContextFactory ?? throw new ArgumentNullException(nameof(workflowContextFactory));
        this._msgFactory = msgFactory ?? throw new ArgumentNullException(nameof(msgFactory));
        this._typeSerializer = typeSerializer ?? throw new ArgumentNullException(nameof(typeSerializer));
        this._widgetBuilderFactory = widgetBuilderFactory ?? throw new ArgumentNullException(nameof(widgetBuilderFactory));
        this._createFolderProcessor = createFolderProcessor ?? throw new ArgumentNullException(nameof(createFolderProcessor));
        this._prerequisitsWorkflow = prerequisitsWorkflow ?? throw new ArgumentNullException(nameof(prerequisitsWorkflow));

        this._queryFn = this._fnFactory
            .Build(typeof(IProcessor<QueryCmd, QueryRes>));
        this._dashboardFn = this._fnFactory
            .Build(typeof(IProcessor<CreateDashboardCmd, DashboardRes>));
    }

    public async Task<DashboardWorkflowRes> ProcessAsync(CreateDashboardWorkflowCmd cmd)
    {
        var cancellationToken = new CancellationToken();
        var context = this._workflowContextFactory.Create();

        this.BuildDimensions(cmd);

        // todo: fix this...
        await this._createFolderProcessor.ProcessAsync(new CreateFolderCmd { FolderName = cmd.DashboardName, QueryFolderPath = cmd.QueryFolderBasePath });

        // todo: fix this...
        this._prerequisitsWorkflow
            .Init(context, cmd);
        // .Resolve(cancellationToken);

        var dashboard = this.BuildWorkflow(cmd, context);
        await dashboard.Resolve(cancellationToken);

        return new DashboardWorkflowRes { };
    }

    private void BuildDimensions(CreateDashboardWorkflowCmd cmd)
    {
        var blueprints = new List<IRectangleBlueprint>();

        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 1, Uuid = "r-team-members", GroupId = 0, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 1, Uuid = "r-sprint-overview", GroupId = 0, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 1, Uuid = "r-sprint-capacity", GroupId = 0, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 1, Uuid = "r-new-work-item", GroupId = 0, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 1, Uuid = "r-team-velocity", GroupId = 0, IsElasticW = true });

        blueprints.Add(new RectangleBlueprint { MinW = 5, MinH = 4, Uuid = "r-burndown", GroupId = 1, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 4, Uuid = "r-tasks-not-updated-7d", GroupId = 1, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 4, Uuid = "r-tasks-not-updated-1d", GroupId = 1, IsElasticW = true });
        blueprints.Add(new RectangleBlueprint { MinW = 3, MinH = 4, Uuid = "r-tasks-closed-1d", GroupId = 1, IsElasticW = true });

        blueprints.AddRange(
            cmd.Initiatives
                .Select((v, i) => (IRectangleBlueprint)new RectangleBlueprint { MinW = 10, MinH = 4, Uuid = Prefix(i), GroupId = 2, IsElasticW = true })
                .ToList()
        );

        var rootGrid = new Grid(15)
            .Init(blueprints)
            .InflateRowWidthsToMeet()
            .InitCoords()
            .IniHash()
            .Validate()
            .Print();

        this._rootHash = rootGrid.Hash();
        this._childHash = rootGrid
            .Result()
            .SelectMany(row => row)
            .Where(r => r.GroupId == 2)
            .SelectMany((r, i) =>
            {
                List<IRectangleBlueprint> blueprints = new()
                {
                    new RectangleBlueprint { MinW = 2, MinH = 1, Uuid = $"{Prefix(i)}/r-md", IsElasticW = false, GroupId = 0 }
                };

                blueprints.AddRange(WidgetConsts.Statuses.Select(s => new RectangleBlueprint { MinW = 1, MinH = 1, Uuid = $"{Prefix(i)}/r-status-{s}", IsElasticW = false, GroupId = 0 }));

                blueprints.Add(new RectangleBlueprint { MinW = 10, MinH = 3, Uuid = $"{Prefix(i)}/r-query-tree", IsElasticW = false, GroupId = 1 });

                return new Grid(r.W)
                    .Init(blueprints)
                    .InflateRowWidthsToMeet()
                    .InitCoords()
                    .IniHash()
                    .Validate()
                    .Print()
                    .Result()
                    .SelectMany(row => row);
            })
            .ToDictionary(r => r.Uuid);
    }

    private INode BuildWorkflow(
        CreateDashboardWorkflowCmd cmd,
        IWorkflowContext context
    )
    {
        var teamDetails = (context.GetMsg("__team_details__") as Msg<TeamRes>)!.GetData().ExtResp;
        var iterationDetails = (context.GetMsg("__team_iterations__") as Msg<IterationsRes>)!.GetData().ExtResp;
        var iteration = iterationDetails.Value.Last(x => x.Name == cmd.IterationName);

        var widgets = new List<Widget>();
        var promisedWidgets = new List<INode>();

        var sprintCapacityWidget =
            this._widgetBuilderFactory.Create(WidgetTypes.SprintCapacityWidget, "Sprint Capacity")
            .AddSettings(this._typeSerializer.Serialize(new SprintCapacityWidgetSettings { TeamId = teamDetails.Id }))
            .AddDimensions(Pos("r-sprint-capacity"))
            .Build();

        widgets.Add(sprintCapacityWidget);

        var sprintOverviewWidget =
            this._widgetBuilderFactory.Create(WidgetTypes.SprintOverviewWidget, "Sprint Overview")
            .AddDimensions(Pos("r-sprint-overview"))
            .Build();

        widgets.Add(sprintOverviewWidget);

        var teamMembersWidget =
            this._widgetBuilderFactory.Create(WidgetTypes.TeamMembersWidget, "Team Members")
            .AddSettings(this._typeSerializer.Serialize(new SprintCapacityWidgetSettings { TeamId = teamDetails.Id }))
            .AddDimensions(Pos("r-team-members"))
            .Build();

        widgets.Add(teamMembersWidget);

        var teamVelocityWidget =
            this._widgetBuilderFactory.Create(WidgetTypes.VelocityWidget, "Team Velocity")
            .AddDimensions(Pos("r-team-velocity"))
            .Build();

        widgets.Add(teamVelocityWidget);

        var newWorkItemWidget =
            this._widgetBuilderFactory.Create(WidgetTypes.NewWorkItemWidget, "New Work Item")
            .AddDimensions(Pos("r-new-work-item"))
            .Build();

        widgets.Add(newWorkItemWidget);

        var burndownWidget
            = this._widgetBuilderFactory.Create(WidgetTypes.AnalyticsSprintBurndownWidget, "Burndown")
            .AddSettings(
                this._typeSerializer.Serialize(new BurndownChartWidgetSettings()
                {
                    Team = new Team { TeamId = teamDetails.Id, ProjectId = teamDetails.ProjectId },
                    IterationPath = cmd.IterationPath,
                    TimePeriodConfiguration = new TimePeriodConfiguration { StartDate = FormatDate(iteration.Attributes.StartDate), EndDate = FormatDate(iteration.Attributes.FinishDate) }
                })
            )
            .AddDimensions(Pos("r-burndown"))
            .Build();

        widgets.Add(burndownWidget);

        promisedWidgets.Add(
            this.BuildQueryWidgetWorkflow(
                context,
                new QueryCmd
                {
                    QueryFolderPath = cmd.QueryFolderPath,
                    QueryName = $"{cmd.IterationName}-TasksNotUpdated-7d-Qry",
                    BuildWiqlCmd = new BuildWiqlCmd
                    {
                        Columns = new List<string>
                        {
                            "[System.Id]",
                            "[System.WorkItemType]",
                            "[System.Title]",
                            "[System.AssignedTo]",
                            "[System.State]",
                            "[System.Tags]"
                        },
                        Table = "WorkItems",
                        Conditions = new List<QryCondition>
                        {
                            new() {Column = "[System.TeamProject]", Condition = "@project"},
                            new() {Column = "[System.WorkItemType]", Condition = "'Task'"},
                            new() {Column = "[System.ChangedDate]", Operator = "<", Condition = "@startOfDay('-7d')"},
                            new() {Column = "[System.State]", Condition = "'Active'"},
                            new() {Column = "[Microsoft.VSTS.Scheduling.RemainingWork]", Operator = ">", Condition = "0"},
                            new() {Column = "[System.IterationPath]", Condition = $"'{cmd.IterationPath}'"},
                        }
                    }
                },
                id => c =>
                {
                    var res = c.GetMsgData<QueryRes>(id)!.ExtResp;

                    var widget = this._widgetBuilderFactory
                        .Create(WidgetTypes.WitViewWidget, "Tasks Not Updated -7d")
                        .AddSettings(this._typeSerializer.Serialize(new WitViewWidgetSettings { Query = new Query { QueryId = res.Id, QueryName = res.Name } }))
                        .AddDimensions(Pos("r-tasks-not-updated-7d"))
                        .Build();

                    return this._msgFactory.Create(widget);
                }
            ));

        promisedWidgets.Add(
            this.BuildQueryWidgetWorkflow(
                context,
                new QueryCmd
                {
                    QueryFolderPath = cmd.QueryFolderPath,
                    QueryName = $"{cmd.IterationName}-TasksNotUpdated-1d-Qry",
                    BuildWiqlCmd = new BuildWiqlCmd
                    {
                        Columns = new List<string>
                        {
                            "[System.Id]",
                            "[System.WorkItemType]",
                            "[System.Title]",
                            "[System.AssignedTo]",
                            "[System.State]",
                            "[System.Tags]"
                        },
                        Table = "WorkItems",
                        Conditions = new List<QryCondition>
                        {
                            new() {Column = "[System.TeamProject]", Condition = "@project"},
                            new() {Column = "[System.WorkItemType]", Condition = "'Task'"},
                            new() {Column = "[System.ChangedDate]", Operator = "<", Condition = "@startOfDay('-1d')"},
                            new() {Column = "[System.State]", Condition = "'Active'"},
                            new() {Column = "[Microsoft.VSTS.Scheduling.RemainingWork]", Operator = ">", Condition = "0"},
                            new() {Column = "[System.IterationPath]", Condition = $"'{cmd.IterationPath}'"},
                        }
                    }
                },
                id => c =>
                {
                    var res = c.GetMsgData<QueryRes>(id)!.ExtResp;

                    var widget = this._widgetBuilderFactory
                        .Create(WidgetTypes.WitViewWidget, "Tasks Not Updated -1d")
                        .AddSettings(this._typeSerializer.Serialize(new WitViewWidgetSettings { Query = new Query { QueryId = res.Id, QueryName = res.Name } }))
                        .AddDimensions(Pos("r-tasks-not-updated-1d"))
                        .Build();

                    return this._msgFactory.Create(widget);
                }
            ));

        promisedWidgets.Add(
            this.BuildQueryWidgetWorkflow(
                context,
                new QueryCmd
                {
                    QueryFolderPath = cmd.QueryFolderPath,
                    QueryName = $"{cmd.IterationName}-TaskClosed-1d-Qry",
                    BuildWiqlCmd = new BuildWiqlCmd
                    {
                        Columns = new List<string>
                        {
                            "[System.Id]",
                            "[System.WorkItemType]",
                            "[System.Title]",
                            "[System.AssignedTo]",
                            "[System.State]",
                            "[System.Tags]"
                        },
                        Table = "WorkItems",
                        Conditions = new List<QryCondition>
                        {
                            new() {Column = "[System.TeamProject]", Condition = "@project"},
                            new() {Column = "[System.WorkItemType]", Condition = "'Task'"},
                            new() {Column = "[System.ChangedDate]", Operator = "<", Condition = "@startOfDay('-1d')"},
                            new() {Column = "[System.State]", Condition = "'Closed'"},
                            new() {Column = "[System.IterationPath]", Condition = $"'{cmd.IterationPath}'"},
                        }
                    }
                },
                id => c =>
                {
                    var res = c.GetMsgData<QueryRes>(id)!.ExtResp;

                    var widget = this._widgetBuilderFactory
                        .Create(WidgetTypes.WitViewWidget, "Tasks Closed -1d")
                        .AddSettings(this._typeSerializer.Serialize(new WitViewWidgetSettings { Query = new Query { QueryId = res.Id, QueryName = res.Name } }))
                        .AddDimensions(Pos("r-tasks-closed-1d"))
                        .Build();

                    return this._msgFactory.Create(widget);
                }
            ));

        foreach (var (initiative, initiativeIndx) in cmd.Initiatives.Select((v, i) => (v, i)))
        {
            string uuid = Prefix(initiativeIndx);

            var markdownWidget =
                this._widgetBuilderFactory.Create(WidgetTypes.MarkdownWidget, $"md-widget{initiative.Title}")
                .AddSettings(initiative.Markdown)
                .AddDimensions(Pos(uuid, $"{uuid}/r-md"))
                .Build();

            widgets.Add(markdownWidget);

            promisedWidgets.AddRange(WidgetConsts.Statuses.Select((s, i) => this.BuildQueryWidgetWorkflow
            (
                context,
                new QueryCmd
                {
                    QueryFolderPath = cmd.QueryFolderPath,
                    QueryName = $"{cmd.IterationName}-Status ({s})-{initiative.Tag}-Qry",
                    BuildWiqlCmd = new BuildWiqlCmd
                    {
                        Columns = new List<string>
                        {
                            "[System.Id]",
                            "[System.WorkItemType]",
                            "[System.Title]",
                            "[System.AssignedTo]",
                            "[System.State]",
                            "[System.Tags]"
                        },
                        Table = "WorkItems",
                        Conditions = new List<QryCondition>
                        {
                            new() {Column = "[System.TeamProject]", Condition = "@project"},
                            new() {Column = "[System.WorkItemType]", Operator = "<>", Condition = "''"},
                            new() {Column = "[System.State]", Operator = "<>", Condition = "''"},
                            new() {Column = "[System.State]", Condition = $"'{s}'"},
                            new() {Column = "[System.IterationPath]", Condition = $"'{cmd.IterationPath}'"},
                            new() {Column = "[System.Tags]", Operator = "CONTAINS", Condition = $"'{initiative.Tag}'"},
                        }
                    }
                },
                id => c =>
                {
                    var res = c.GetMsgData<QueryRes>(id)!.ExtResp;

                    var widget = this._widgetBuilderFactory
                        .Create(WidgetTypes.QueryScalarWidget, $"Status ({s})")
                        .AddSettings(this._typeSerializer.Serialize<QueryScalarWidgetSettings>(new QueryScalarWidgetSettings { QueryId = res.Id, QueryName = res.Name, DefaultBackgroundColor = WidgetConsts.StatusColors[s] }))
                        .AddDimensions(Pos(uuid, $"{uuid}/r-status-{s}"))
                        .Build();

                    return this._msgFactory.Create(widget);
                }
            )));

            promisedWidgets.Add(
                this.BuildQueryWidgetWorkflow(
                    context,
                    new QueryCmd
                    {
                        QueryFolderPath = cmd.QueryFolderPath,
                        QueryName = $"{cmd.IterationName}-View-{initiative.Tag}-Qry",
                        BuildWiqlCmd = new BuildWiqlCmd
                        {
                            Mode = "Recursive",
                            Columns = new List<string>
                            {
                                "[System.Id]",
                                "[Microsoft.VSTS.Common.Priority]",
                                "[System.State]",
                                "[System.Title]",
                                "[System.AssignedTo]",
                                "[System.Tags]",
                                "[Microsoft.VSTS.Scheduling.StoryPoints]"
                            },
                            Table = "WorkItemLinks",
                            Conditions = new List<QryCondition>
                            {
                                new() {Column = "[Source].[System.TeamProject]", Condition = "'Software'", GroupingKey = 1},
                                new() {Column = "[Source].[System.WorkItemType]", Operator = "<>", Condition = "''", GroupingKey = 1},
                                new() {Column = "[Source].[System.IterationPath]", Condition = $"'{cmd.IterationPath}'", GroupingKey = 1},
                                new() {Column = "[Source].[System.Tags]", Operator = "CONTAINS", Condition = $"'{initiative.Tag}'", GroupingKey = 1},
                                new() {Column = "[Source].[Microsoft.VSTS.Scheduling.StoryPoints]", Operator = ">", Condition = "0", GroupingKey = 1},
                                new() {Column = "[System.Links.LinkType]", Condition = "'System.LinkTypes.Hierarchy-Forward'", GroupingKey = 2},
                                new() {Column = "[Target].[System.TeamProject]", Condition = "'Software'", GroupingKey = 3},
                                new() {Column = "[Target].[System.WorkItemType]", Operator = "<>", Condition = "''", GroupingKey = 3},
                                new() {Column = "[Target].[System.Tags]", Operator = "CONTAINS", Condition = $"'{initiative.Tag}'", GroupingKey = 3},
                                new() {Column = "[Target].[Microsoft.VSTS.Scheduling.RemainingWork]", Operator = ">", Condition = "0", GroupingKey = 3},
                                new() {Column = "[Target].[Microsoft.VSTS.Scheduling.OriginalEstimate]", Operator = ">", Condition = "0", GroupingKey = 3},
                            }
                        }
                    },
                    id => c =>
                    {
                        var res = c.GetMsgData<QueryRes>(id)!.ExtResp;
                        string settings = this._typeSerializer.Serialize<WitViewWidgetSettings>(new WitViewWidgetSettings()
                        {
                            Query = new Query
                            {
                                QueryId = res.Id,
                                QueryName = res.Name
                            }
                        });

                        var widget = this._widgetBuilderFactory
                            .Create(WidgetTypes.WitViewWidget, "Work Items")
                            .AddSettings(settings)
                            .AddDimensions(Pos(uuid, $"{uuid}/r-query-tree"))
                            .Build();

                        return this._msgFactory.Create(widget);
                    }
                )
            );
        }

        Func<IWorkflowContext, IMsg?> fn = c =>
        {
            string[] ids = promisedWidgets.Select(w => w.NodeConfiguration.Id).ToArray();
            var ws = c.GetMsgs(ids)
                .Select(m => (m as Msg<Widget>)!.GetData())
                .ToList();

            ws.AddRange(widgets);

            var dCmd = new CreateDashboardCmd
            {
                Req = new ExtDashboardReq { Widgets = ws, Name = cmd.DashboardName }, // todo: where should this dashboard name come from?...
                TeamName = cmd.TeamName
            };

            return this._msgFactory.Create(dCmd, "cmd");
        };

        var dashboardCmd =
            this._nodeBuilderFactory.Create()
                .Configure(c => c.AddContext(context).AddArg(promisedWidgets.ToArray()))
                .AddFn(fn)
                .Build();

        // todo: this is a hack... should not have to specify the type here...
        Type dashboardProcessorType = typeof(DashboardProcessor);
        var dashboard =
            this._nodeBuilderFactory.Create()
                .Configure(c => c.AddServiceType(dashboardProcessorType).AddContext(context).AddArg(dashboardCmd))
                .AddFn(this._dashboardFn)
                .Build();

        return dashboard;
    }

    private INode BuildQueryWidgetWorkflow(
        IWorkflowContext context,
        QueryCmd cmd,
        Func<string, Func<IWorkflowContext, IMsg?>> fn
    )
    {
        // todo: this is a hack... should not have to specify the type here...
        Type queryProcessorType = typeof(QueryProcessor);

        var qry =
            this._nodeBuilderFactory.Create()
                .Configure(c => c.AddServiceType(queryProcessorType).MatchArg(cmd).AddContext(context))
                .AddFn(this._queryFn)
                .Build();

        var widget =
            this._nodeBuilderFactory.Create()
                .Configure(c => c.AddArg(qry).AddContext(context))
                .AddFn(fn(qry.NodeConfiguration.Id))
                .Build();

        return widget;
    }

    public (int, int, int, int) Pos(
        string uuid,
        string? innerUuid = null
    )
    {
        var outer = this._rootHash[uuid];

        if (innerUuid == null) return (outer.Y + 1, outer.X + 1, outer.H, outer.W);

        var inner = this._childHash[innerUuid];

        return (outer.Y + inner.Y + 1, outer.X + inner.X + 1, inner.H, inner.W);
    }

    private static string FormatDate(string input) => DateTime.Parse(input).ToString("yyyy-MM-dd");
}
