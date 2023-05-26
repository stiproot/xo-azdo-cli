namespace Xo.AzDO.Cli.Abstractions;

internal interface IBuilder<out TOut, TIn>
{
	TOut Build(ref TIn input);
}