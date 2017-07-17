using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal interface IIdentifiable : IReportLink, ICloneable
  {
    string Name { get; set; }

    string DisplayName { get; set; }
  }
}
