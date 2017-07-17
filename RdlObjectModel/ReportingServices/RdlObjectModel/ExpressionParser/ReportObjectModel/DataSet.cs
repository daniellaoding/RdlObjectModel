namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class DataSet
  {
    internal abstract string CommandText { get; }

    internal abstract string RewrittenCommandText { get; }
  }
}
