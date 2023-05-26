namespace Xo.AzDO.Cli.Services;

internal class TypeSerializer : ITypeSerializer
{
	public string Serialize<T>(T type) => JsonConvert.SerializeObject(type);
	public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
}
