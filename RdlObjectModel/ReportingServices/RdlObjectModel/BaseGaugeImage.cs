using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class BaseGaugeImage : ReportObject
  {
    public ReportExpression<SourceType> Source
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<SourceType>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression MIMEType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> TransparentColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public BaseGaugeImage()
    {
    }

    internal BaseGaugeImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Image.GetEmbeddedImgDependencies(GetAncestor<Report>(), dependencies, Source.Value, Value);
    }

    internal class Definition : DefinitionStore<BaseGaugeImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Source,
        Value,
        MIMEType,
        TransparentColor,
        PropertyCount,
      }
    }
  }
}
