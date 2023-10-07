namespace Xo.AzDO.Cli.Abstractions;

internal interface IProvider<T>
{
	T Provide();
}