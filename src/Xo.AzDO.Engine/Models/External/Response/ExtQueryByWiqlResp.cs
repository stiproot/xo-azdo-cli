namespace Xo.AzDO.Engine.Models.External.Response;

public class ExtQueryByWiqlResp
{
    [JsonProperty("queryType")]
    public string QueryType { get; init; }

    [JsonProperty("queryResultType")]
    public string QueryResultType { get; init; }

    [JsonProperty("asOf")]
    public DateTime AsOf { get; init; }

    [JsonProperty("columns")]
    public List<ExtQueryByWiqlColumn> Columns { get; init; }

    [JsonProperty("sortColumns")]
    public List<ExtQueryByWiqlSortColumn> SortColumns { get; init; }

    [JsonProperty("workItems")]
    public List<ExtQueryByWiqlWorkItem> WorkItems { get; init; }
}

public class ExtQueryByWiqlColumn
{
    [JsonProperty("referenceName")]
    public string ReferenceName { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }
}

public class ExtQueryByWiqlField
{
    [JsonProperty("referenceName")]
    public string ReferenceName { get; init; }

    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }
}

public class ExtQueryByWiqlSortColumn
{
    [JsonProperty("field")]
    public ExtQueryByWiqlField Field { get; init; }

    [JsonProperty("descending")]
    public bool Descending { get; init; }
}

public class ExtQueryByWiqlWorkItem
{
    [JsonProperty("id")]
    public int Id { get; init; }

    [JsonProperty("url")]
    public string Url { get; init; }
}

