namespace Xo.AzDO.Cli.Abstractions;

internal interface ITypeMapper<TSource, out TTarget> where TTarget : new()
{
	// TTarget Map(ref TSource source);
	TTarget Map(TSource source);
}

