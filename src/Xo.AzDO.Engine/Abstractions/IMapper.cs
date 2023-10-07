namespace Xo.AzDO.Engine.Abstractions;

public interface IMapper<in TSource, out TTarget> where TTarget : new()
{
	TTarget Map(TSource source);
}

