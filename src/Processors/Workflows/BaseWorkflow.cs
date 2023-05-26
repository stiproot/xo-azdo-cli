internal abstract class BaseWorkflow
{
	protected readonly INodeBuilderFactory _NodeBuilderFactory;
	protected readonly IFunctitect _Functitect;

	public BaseWorkflow(
		INodeBuilderFactory nodeBuilderFactory,
		IFunctitect functitect
	)
	{
		this._NodeBuilderFactory = nodeBuilderFactory ?? throw new ArgumentNullException(nameof(nodeBuilderFactory));
		this._Functitect = functitect ?? throw new ArgumentNullException(nameof(functitect));
	}
}
