using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IExpression : IFormattable
  {
    object Value { get; set; }

    string Expression { get; set; }

    bool IsExpression { get; }

    bool IsEmpty { get; }

    void GetDependencies(IList<ReportObject> dependencies, ReportObject parent);
  }
}
