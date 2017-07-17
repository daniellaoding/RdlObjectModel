using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class Globals
  {
    internal abstract object this[string key] { get; }

    internal abstract string ReportName { get; }

    internal abstract int PageNumber { get; }

    internal abstract int TotalPages { get; }

    internal abstract DateTime ExecutionTime { get; }

    internal abstract string ReportServerUrl { get; }

    internal abstract string ReportFolder { get; }
  }
}
