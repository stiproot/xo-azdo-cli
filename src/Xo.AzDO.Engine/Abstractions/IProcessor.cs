namespace Xo.AzDO.Cli.Abstractions;

internal interface IProcessor<TIn, TOut>
	where TIn : IProcessorCmd
	where TOut : IProcessorRes
{
	Task<TOut> ProcessAsync(TIn cmd);
}