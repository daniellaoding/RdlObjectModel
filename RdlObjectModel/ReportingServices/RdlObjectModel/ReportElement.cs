namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class ReportElement : ReportObject
  {
    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportElement()
    {
    }

    internal ReportElement(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      Style = new Style();
    }

    internal class Definition : DefinitionStore<ReportElement, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
      }
    }
  }
}
