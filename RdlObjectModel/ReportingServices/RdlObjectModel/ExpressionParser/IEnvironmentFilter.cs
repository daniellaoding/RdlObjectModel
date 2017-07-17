using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface IEnvironmentFilter
  {
    bool IsAllowedType(Type type, out bool allowNew, out bool allowNewArray);

    bool IsAllowedMember(string memberName);
  }
}
