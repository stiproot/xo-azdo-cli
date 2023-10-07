namespace Xo.AzDO.Engine.Models.External.Response;

public class ExtIterationsResp
{
	public int Count { get; init; }
	public IEnumerable<Iteration> Value { get; init; }
}

public class Iteration
{
	public string Id { get; init; }
	public string Name { get; init; }
	public string Path { get; init; }
	public Attributes Attributes { get; init; }
	public string Url { get; init; }
}

public class Attributes
{
	public string StartDate { get; init; }
	public string FinishDate { get; init; }
	public string TimeFrame { get; init; }
}