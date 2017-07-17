using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class AltReportItem : ReportObject
  {
    public ReportItem ReportItem
    {
      get
      {
        return (ReportItem) PropertyStore.GetObject(0);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(0, value);
      }
    }

    public AltReportItem()
    {
    }

    internal AltReportItem(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<AltReportItem, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ReportItem,
      }
    }
  }
}
