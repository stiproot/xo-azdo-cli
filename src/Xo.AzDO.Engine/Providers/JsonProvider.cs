namespace Xo.AzDO.Engine.Providers;

public class JsonProvider : IJsonProvider
{
    private string PathToFile(string fileName)
    {
        var executingDir = Assembly.GetExecutingAssembly().Location;
        var filePath = Path.Combine(Path.GetDirectoryName(executingDir), fileName);
        if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);
        return filePath;
    }

    private string ReadContentOfFile(string filePath)
            => File.ReadAllText(filePath);

    public T GetContent<T>(string fileName)
            => JsonConvert.DeserializeObject<T>(this.ReadContentOfFile(this.PathToFile(fileName)));

    public string GetContent(string fileName)
            => this.ReadContentOfFile(this.PathToFile(fileName));
}

