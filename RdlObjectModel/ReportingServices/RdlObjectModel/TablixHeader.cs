namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixHeader : ReportObject
  {
    public ReportSize Size
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

    public CellContents CellContents
    {
      get
      {
        return (CellContents) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public TablixHeader()
    {
    }

    internal TablixHeader(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Size = Constants.DefaultZeroSize;
      CellContents = new CellContents();
    }

    internal class Definition : DefinitionStore<TablixHeader, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Size,
        CellContents,
      }
    }
  }
}
