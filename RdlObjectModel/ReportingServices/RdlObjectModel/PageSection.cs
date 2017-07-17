using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class PageSection : ReportElement
  {
    public ReportSize Height
    {
      get
      {
        return PropertyStore.GetSize(1);
      }
      set
      {
        PropertyStore.SetSize(1, value);
      }
    }

    [DefaultValue(false)]
    public bool PrintOnFirstPage
    {
      get
      {
        return PropertyStore.GetBoolean(2);
      }
      set
      {
        PropertyStore.SetBoolean(2, value);
      }
    }

    [DefaultValue(false)]
    public bool PrintOnLastPage
    {
      get
      {
        return PropertyStore.GetBoolean(3);
      }
      set
      {
        PropertyStore.SetBoolean(3, value);
      }
    }

    [DefaultValue(false)]
    public bool PrintBetweenSections
    {
      get
      {
        return PropertyStore.GetBoolean(5);
      }
      set
      {
        PropertyStore.SetBoolean(5, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ReportItem>))]
    public IList<ReportItem> ReportItems
    {
      get
      {
        return (IList<ReportItem>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public PageSection()
    {
    }

    internal PageSection(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Height = Constants.DefaultZeroSize;
      ReportItems = new RdlCollection<ReportItem>();
    }

    internal class Definition : DefinitionStore<PageSection, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Height,
        PrintOnFirstPage,
        PrintOnLastPage,
        ReportItems,
        PrintBetweenSections,
      }
    }
  }
}
