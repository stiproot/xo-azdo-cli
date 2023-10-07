namespace Xo.AzDO.Engine.Abstractions;

public interface ITypeSerializer
{
	string Serialize<T>(T type);
	T Deserialize<T>(string json);
}
