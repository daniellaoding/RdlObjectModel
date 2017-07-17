namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixColumn : ReportObject
  {
    public ReportSize Width
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

    public TablixColumn()
    {
    }

    internal TablixColumn(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Width = Constants.DefaultZeroSize;
    }

    internal class Definition : DefinitionStore<TablixColumn, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Width,
      }
    }
  }
}
