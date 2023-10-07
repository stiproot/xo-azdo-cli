public abstract class BaseWorkflow
{
	protected readonly INodeBuilderFactory _NodeBuilderFactory;
	protected readonly IFnFactory _FnFactory;
	protected readonly IStateManager _StateManager;

	public BaseWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFnFactory fnFactory,
		IStateManager stateManager
	)
	{
		this._NodeBuilderFactory = nodeBuilderFactory ?? throw new ArgumentNullException(nameof(nodeBuilderFactory));
		this._FnFactory = fnFactory ?? throw new ArgumentNullException(nameof(fnFactory));
		this._StateManager = stateManager ?? throw new ArgumentNullException(nameof(stateManager));
	}
}
