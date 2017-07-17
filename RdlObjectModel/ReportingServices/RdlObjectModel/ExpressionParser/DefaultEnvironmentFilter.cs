using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class DefaultEnvironmentFilter : IEnvironmentFilter
  {
	  internal static IEnvironmentFilter Instance { get; } = new DefaultEnvironmentFilter();

	  private DefaultEnvironmentFilter()
    {
    }

    public bool IsAllowedType(Type type, out bool allowNew, out bool allowNewArray)
    {
      allowNew = true;
      allowNewArray = true;
      return true;
    }

    public bool IsAllowedMember(string memberName)
    {
      return true;
    }
  }
}
