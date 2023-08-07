namespace Xo.AzDO.Cli.Abstractions;

internal interface IMapper<in TSource, out TTarget> where TTarget : new()
{
	TTarget Map(TSource source);
}

