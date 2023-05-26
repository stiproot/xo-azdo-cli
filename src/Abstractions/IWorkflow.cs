namespace Xo.AzDO.Cli.Abstractions;

internal interface IWorkflow<TIn> where TIn : IProcessorCmd
{
	INode Init(
		IWorkflowContext context,
		TIn cmd
	);
}