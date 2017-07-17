namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class Parameter
  {
    internal abstract object Value { get; }

    internal abstract object Label { get; }

    internal abstract int Count { get; }

    internal abstract bool IsMultiValue { get; }
  }
}
