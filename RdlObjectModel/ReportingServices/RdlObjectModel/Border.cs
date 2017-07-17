using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Border : ReportObject, IShouldSerialize
  {
    [ReportExpressionDefaultValueConstant(typeof (ReportColor), "DefaultBorderColor")]
    public ReportExpression<ReportColor> Color
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (BorderStyles), BorderStyles.Default)]
    public ReportExpression<BorderStyles> Style
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BorderStyles>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultBorderWidth")]
    public ReportExpression<ReportSize> Width
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Border()
    {
    }

    internal Border(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string property)
    {
      if (property == "Style" && !Style.IsExpression && Style.Value == BorderStyles.Default)
        return SerializationMethod.Never;
      return Parent is Style && ((Style) Parent).Border != this ? SerializationMethod.Always : SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<Border, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Color,
        Style,
        Width,
      }
    }
  }
}
