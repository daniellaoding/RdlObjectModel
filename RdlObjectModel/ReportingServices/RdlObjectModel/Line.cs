namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Line : ReportItem
  {
    public Line()
    {
    }

    internal Line(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal class Definition : DefinitionStore<Line, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Name,
        ActionInfo,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Visibility,
        ToolTip,
        ToolTipLocID,
        DocumentMapLabel,
        DocumentMapLabelLocID,
        Bookmark,
        RepeatWith,
        CustomProperties,
        DataElementName,
        DataElementOutput,
        PropertyCount,
      }
    }
  }
}
