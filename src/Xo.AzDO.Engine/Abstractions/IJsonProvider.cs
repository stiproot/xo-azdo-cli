namespace Xo.AzDO.Engine.Abstractions;

public interface IJsonProvider
{
    T GetContent<T>(string fileName);
    string GetContent(string fileName);
}
