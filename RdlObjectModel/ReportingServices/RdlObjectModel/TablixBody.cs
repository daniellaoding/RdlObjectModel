using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixBody : DataRegionBody
  {
    [XmlElement(typeof (RdlCollection<TablixColumn>))]
    public IList<TablixColumn> TablixColumns
    {
      get
      {
        return (IList<TablixColumn>) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<TablixRow>))]
    public IList<TablixRow> TablixRows
    {
      get
      {
        return (IList<TablixRow>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public TablixBody()
    {
    }

    internal TablixBody(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TablixColumns = new RdlCollection<TablixColumn>();
      TablixRows = new RdlCollection<TablixRow>();
    }

    internal class Definition : DefinitionStore<TablixBody, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        TablixColumns,
        TablixRows,
      }
    }
  }
}
