using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  [XmlElementClass("Border")]
  public class EmptyBorder : Border, IShouldSerialize
  {
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

    [ReportExpressionDefaultValue(typeof (BorderStyles), BorderStyles.Solid)]
    public new ReportExpression<BorderStyles> Style
    {
      get
      {
        return base.Style;
      }
      set
      {
        base.Style = value;
      }
    }

    public EmptyBorder()
    {
    }

    internal EmptyBorder(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Color = Constants.DefaultEmptyColor;
      Style = BorderStyles.Solid;
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
    {
      if (property == "Style" && !Style.IsExpression && Style.Value == BorderStyles.Solid)
        return SerializationMethod.Never;
      return Parent is EmptyColorStyle && ((EmptyColorStyle) Parent).Border != this ? SerializationMethod.Always : SerializationMethod.Auto;
    }
  }
}
