using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class EnumSystem : BaseInternalExpression
  {
    private readonly string _Enum;
    private readonly Type _EnumType;
    private object _EvaluateValue;

    public EnumSystem(Type type, string enumName)
    {
      _EnumType = type;
      _Enum = enumName;
    }

    public override TypeCode TypeCode()
    {
      throw new NotImplementedException();
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      return _EnumType.Name + "." + _Enum;
    }

    public override object Evaluate()
    {
      return _EvaluateValue;
    }
  }
}
