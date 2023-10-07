namespace Xo.AzDO.Engine.Models.External.Response;

public sealed class ExtGetWiResp
{
  public ExtGetWorkItemReferenceLinks _links { get; init; }
  public WorkItemCommentVersionRef commentVersionRef { get; init; }
  public Dictionary<string, object> fields { get; init; }
  public int id { get; init; }
  public IEnumerable<ExtGetWorkItemRelation> relations { get; init; }
  public int rev { get; init; }
  public string url { get; init; }
}

public sealed class ExtGetWorkItemCommentVersionRef
{
  public int commentId { get; init; }
  public int createdInRevision { get; init; }
  public bool idDeleted { get; init; }
  public string text { get; init; }
  public string url { get; init; }
  public int version { get; init; }
}

public sealed class ExtGetWorkItemReferenceLinks
{
  public object links { get; init; }
}

public sealed class ExtGetWorkItemRelation
{
  public ExtGetWorkItemAttribute attributes { get; init; }
  public string rel { get; init; }
  public string url { get; init; }
}

public sealed class ExtGetWorkItemAttribute
{
	public bool isLocked { get; init; } = false;
	public string name { get; init; } = string.Empty;
}

