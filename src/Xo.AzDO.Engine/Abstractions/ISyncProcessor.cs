namespace Xo.AzDO.Engine.Abstractions;

public interface ISyncProcessor<TIn, TOut>
	where TIn : IProcessorCmd
	where TOut : IProcessorRes
{
	TOut Process(TIn cmd);
}

// public class DimensionProcessor : ISyncProcessor<CreateDashboardWorkflowCmd, (IGrid, IGrid)>
// {
	// public (IGrid, IGrid) Process(CreateDashboardWorkflowCmd cmd)
	// {
		// throw new NotImplementedException();
	// }
// }