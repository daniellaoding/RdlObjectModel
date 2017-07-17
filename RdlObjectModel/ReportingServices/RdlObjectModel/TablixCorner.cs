using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixCorner : ReportObject
  {
    [XmlArrayItem("TablixCornerRow", typeof (TablixCornerRow))]
    [XmlElement(typeof (RdlCollection<IList<TablixCornerCell>>))]
    public IList<IList<TablixCornerCell>> TablixCornerRows
    {
      get
      {
        return (IList<IList<TablixCornerCell>>) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public TablixCorner()
    {
    }

    internal TablixCorner(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TablixCornerRows = new RdlCollection<IList<TablixCornerCell>>();
    }

    internal class Definition : DefinitionStore<TablixCorner, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        TablixCornerRows,
      }
    }
  }
}
