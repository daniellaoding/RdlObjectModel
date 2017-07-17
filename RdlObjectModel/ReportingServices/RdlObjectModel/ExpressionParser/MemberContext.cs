using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class MemberContext
  {
	  internal string Name { get; }

	  internal List<MemberInfo> MemberInfos { get; }

	  internal TypeContext ReturnTypeContext => null;

	  internal MemberContextTypes MemberContextType { get; }

	  internal TypeContext OwningType { get; }

	  internal MemberContext(string memberName, MemberContextTypes memberContextType)
    {
      Name = memberName;
      MemberContextType = memberContextType;
    }

    internal MemberContext(TypeContext owningType, MemberInfo memberInfo)
    {
      MemberInfos = new List<MemberInfo>();
      MemberInfos.Add(memberInfo);
      OwningType = owningType;
      MemberContextType = MapMemberContextType(memberInfo.MemberType);
    }

    private static MemberContextTypes MapMemberContextType(MemberTypes memberType)
    {
      switch (memberType)
      {
        case MemberTypes.Field:
          return MemberContextTypes.Field;
        case MemberTypes.Method:
          return MemberContextTypes.Method;
        case MemberTypes.Property:
          return MemberContextTypes.Property;
        default:
          return MemberContextTypes.Unknown;
      }
    }

    internal void AddOverload(MemberInfo memberInfo)
    {
      MemberInfos.Add(memberInfo);
    }

    internal enum MemberContextTypes
    {
      Method,
      Field,
      Property,
      Unknown,
    }
  }
}
