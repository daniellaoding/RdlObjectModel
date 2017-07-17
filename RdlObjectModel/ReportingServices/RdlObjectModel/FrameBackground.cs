namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class FrameBackground : ReportObject
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

    public FrameBackground()
    {
    }

    internal FrameBackground(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<FrameBackground, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        PropertyCount,
      }
    }
  }
}
