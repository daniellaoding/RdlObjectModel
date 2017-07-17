namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixCornerCell : ReportObject
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

    public TablixCornerCell()
    {
    }

    internal TablixCornerCell(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<TablixCornerCell, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        CellContents,
      }
    }
  }
}
