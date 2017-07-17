using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface IReportLink : ICloneable
  {
    Report OwnerReport { get; }

    IReportLink ParentElement { get; }

    string FullyQualifiedName { get; }
  }
}
