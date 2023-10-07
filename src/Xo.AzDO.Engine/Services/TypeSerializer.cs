namespace Xo.AzDO.Engine.Services;

public class TypeSerializer : ITypeSerializer
{
	public string Serialize<T>(T type) => JsonConvert.SerializeObject(type);
	public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
}
