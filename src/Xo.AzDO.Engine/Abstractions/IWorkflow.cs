namespace Xo.AzDO.Engine.Abstractions;

public interface IWorkflow<TIn> where TIn : IProcessorCmd
{
	INode Init(
		IWorkflowContext context,
		TIn cmd
	);
}