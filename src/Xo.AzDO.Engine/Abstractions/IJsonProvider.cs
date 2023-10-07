namespace Xo.AzDO.Cli.Abstractions;

public interface IJsonProvider
{
    T GetContent<T>(string fileName);
    string GetContent(string fileName);
}
