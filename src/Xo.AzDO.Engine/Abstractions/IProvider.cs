namespace Xo.AzDO.Engine.Abstractions;

public interface IProvider<T>
{
	T Provide();
}