namespace Xo.AzDO.Cli.Abstractions;

internal interface ITypeSerializer
{
	string Serialize<T>(T type);
	T Deserialize<T>(string json);
}
