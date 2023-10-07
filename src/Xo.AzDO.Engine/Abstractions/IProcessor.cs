namespace Xo.AzDO.Engine.Abstractions;

public interface IProcessor<TIn, TOut>
	where TIn : IProcessorCmd
	where TOut : IProcessorRes
{
	Task<TOut> ProcessAsync(TIn cmd);
}