using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixCell : DataRegionCell
  {
    public CellContents CellContents
    {
      get
      {
        return (CellContents) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ValidEnumValues("TablixCellDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.ContentsOnly)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(2);
      }
      set
      {
        PropertyStore.SetInteger(2, (int) value);
      }
    }

    public TablixCell()
    {
    }

    internal TablixCell(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataElementOutput = DataElementOutputTypes.ContentsOnly;
    }

    internal class Definition : DefinitionStore<TablixCell, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        CellContents,
        DataElementName,
        DataElementOutput,
      }
    }
  }
}
