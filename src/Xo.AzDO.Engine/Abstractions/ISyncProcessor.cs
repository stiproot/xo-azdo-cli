namespace Xo.AzDO.Cli.Abstractions;

internal interface ISyncProcessor<TIn, TOut>
	where TIn : IProcessorCmd
	where TOut : IProcessorRes
{
	TOut Process(TIn cmd);
}

// internal class DimensionProcessor : ISyncProcessor<CreateDashboardWorkflowCmd, (IGrid, IGrid)>
// {
	// public (IGrid, IGrid) Process(CreateDashboardWorkflowCmd cmd)
	// {
		// throw new NotImplementedException();
	// }
// }