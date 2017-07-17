using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Body : ReportElement
  {
    [XmlElement(typeof (RdlCollection<ReportItem>))]
    public IList<ReportItem> ReportItems
    {
      get
      {
        return (IList<ReportItem>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ReportSize Height
    {
      get
      {
        return PropertyStore.GetSize(2);
      }
      set
      {
        PropertyStore.SetSize(2, value);
      }
    }

    public Body()
    {
    }

    internal Body(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ReportItems = new RdlCollection<ReportItem>();
      Height = Constants.DefaultZeroSize;
    }

    internal class Definition : DefinitionStore<Report, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        ReportItems,
        Height,
        PropertyCount,
      }
    }
  }
}
