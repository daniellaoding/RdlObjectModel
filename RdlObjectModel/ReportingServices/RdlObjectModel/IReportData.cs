using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public interface IReportData
  {
    IList<DataSource> DataSources { get; }

    IList<DataSet> DataSets { get; }

    IList<ReportParameter> ReportParameters { get; }

    ReportParametersLayout ReportParametersLayout { get; }
  }
}
