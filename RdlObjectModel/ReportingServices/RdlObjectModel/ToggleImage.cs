namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ToggleImage : ReportObject
  {
    public ReportExpression<bool> InitialState
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ToggleImage()
    {
    }

    internal ToggleImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<ToggleImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        InitialState,
      }
    }
  }
}
