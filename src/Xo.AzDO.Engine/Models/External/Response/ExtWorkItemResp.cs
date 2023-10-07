namespace Xo.AzDO.Engine.Models.External.Response;

public sealed class ExtWiResp
{
  public ReferenceLinks _links { get; init; }
  public WorkItemCommentVersionRef commentVersionRef { get; init; }
  public object fields { get; init; }
  public int id { get; init; }
  public IEnumerable<WorkItemRelation> relations { get; init; }
  public int rev { get; init; }
  public string url { get; init; }
}

public sealed class WorkItemCommentVersionRef
{
  public int commentId { get; init; }
  public int createdInRevision { get; init; }
  public bool idDeleted { get; init; }
  public string text { get; init; }
  public string url { get; init; }
  public int version { get; init; }
}

public sealed class ReferenceLinks
{
  public object links { get; init; }
}

public sealed class WorkItemRelation
{
  public object attributes { get; init; }
  public string relation_type { get; init; }
  public string url { get; init; }
}

