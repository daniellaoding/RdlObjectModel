namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class User
  {
    internal abstract object this[string key] { get; }

    internal abstract string UserID { get; }

    internal abstract string Language { get; }
  }
}
