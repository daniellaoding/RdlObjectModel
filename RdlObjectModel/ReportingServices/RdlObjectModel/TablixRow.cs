using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixRow : ReportObject
  {
    public ReportSize Height
    {
      get
      {
        return PropertyStore.GetSize(0);
      }
      set
      {
        PropertyStore.SetSize(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<TablixCell>))]
    public IList<TablixCell> TablixCells
    {
      get
      {
        return (IList<TablixCell>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public TablixRow()
    {
    }

    internal TablixRow(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Height = Constants.DefaultZeroSize;
      TablixCells = new RdlCollection<TablixCell>();
    }

    internal class Definition : DefinitionStore<TablixRow, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Height,
        TablixCells,
      }
    }
  }
}
