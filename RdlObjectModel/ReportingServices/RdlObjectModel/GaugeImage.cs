using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class GaugeImage : GaugePanelItem
  {
    public ReportExpression<SourceType> Source
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<SourceType>>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression MIMEType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(13);
      }
      set
      {
        PropertyStore.SetObject(13, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> TransparentColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(14);
      }
      set
      {
        PropertyStore.SetObject(14, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Angle
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(15);
      }
      set
      {
        PropertyStore.SetObject(15, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (double), 0.0)]
    public ReportExpression<double> Transparency
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<double>>(16);
      }
      set
      {
        PropertyStore.SetObject(16, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ResizeModes), ResizeModes.AutoFit)]
    public ReportExpression<ResizeModes> ResizeMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ResizeModes>>(17);
      }
      set
      {
        PropertyStore.SetObject(17, value);
      }
    }

    public GaugeImage()
    {
    }

    internal GaugeImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Image.GetEmbeddedImgDependencies(GetAncestor<Report>(), dependencies, Source.Value, Value);
    }

    internal class Definition : DefinitionStore<GaugeImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Style,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Hidden,
        ToolTip,
        ActionInfo,
        ParentItem,
        Source,
        Value,
        MIMEType,
        TransparentColor,
        Angle,
        Transparency,
        ResizeMode,
        PropertyCount,
      }
    }
  }
}
