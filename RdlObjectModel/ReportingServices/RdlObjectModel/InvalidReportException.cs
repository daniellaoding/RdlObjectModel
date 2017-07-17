using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class InvalidReportException : Exception
  {
    public InvalidReportException(string message)
      : base(message)
    {
    }
  }
}
