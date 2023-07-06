namespace Xo.AzDO.Cli.Abstractions;

internal interface IMapper<TSource, out TTarget> where TTarget : new()
{
	TTarget Map(TSource source);
}

