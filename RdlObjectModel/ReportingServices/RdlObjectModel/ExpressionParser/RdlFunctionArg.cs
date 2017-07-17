namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class RdlFunctionArg
  {
	  internal bool IsRequired { get; }

	  internal RdlArgTypes ArgType { get; }

	  internal bool IsVarArg { get; }

	  internal RdlFunctionArg(bool isRequired, RdlArgTypes argType)
      : this(isRequired, argType, false)
    {
    }

    internal RdlFunctionArg(bool isRequired, RdlArgTypes argType, bool isVarArg)
    {
      IsRequired = isRequired;
      ArgType = argType;
      IsVarArg = isVarArg;
    }
  }
}
