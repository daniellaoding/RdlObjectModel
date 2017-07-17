using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("Style")]
  public class EmptyColorStyle : Style
  {
    public EmptyBorder Border
    {
      get
      {
        return (EmptyBorder) base.Border;
      }
      set
      {
        if (value != null && value.Color == ReportColor.Empty)
          value.Color = Constants.DefaultEmptyColor;
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public new ReportExpression<ReportColor> Color
    {
      get
      {
        return base.Color;
      }
      set
      {
        base.Color = value;
      }
    }

    public EmptyColorStyle()
    {
    }

    internal EmptyColorStyle(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Color = Constants.DefaultEmptyColor;
    }
  }
}
