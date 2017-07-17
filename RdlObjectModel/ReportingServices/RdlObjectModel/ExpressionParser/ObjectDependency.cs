namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal struct ObjectDependency
  {
	  public ReportElementType Type { get; }

	  public ReportObject Dependency { get; }

	  public ObjectDependency(ReportElementType inType, ReportObject inDependency)
    {
      Type = inType;
      Dependency = inDependency;
    }

    public static bool operator ==(ObjectDependency leftOp, ObjectDependency rightOp)
    {
      return leftOp.Equals(rightOp);
    }

    public static bool operator !=(ObjectDependency leftOp, ObjectDependency rightOp)
    {
      return !leftOp.Equals(rightOp);
    }

    public override bool Equals(object obj)
    {
      if (obj is ObjectDependency)
      {
        ObjectDependency objectDependency = (ObjectDependency) obj;
        if (Type == objectDependency.Type && ReferenceEquals(Dependency, objectDependency.Dependency))
          return true;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
