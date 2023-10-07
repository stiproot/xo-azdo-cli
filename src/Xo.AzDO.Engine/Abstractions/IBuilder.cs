namespace Xo.AzDO.Engine.Abstractions;

public interface IBuilder<out TOut, TIn>
{
	TOut Build(ref TIn input);
}
