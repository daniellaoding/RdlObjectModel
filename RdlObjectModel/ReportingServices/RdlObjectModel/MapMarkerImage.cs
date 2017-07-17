using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapMarkerImage : ReportObject
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

    [ReportExpressionDefaultValue(typeof (MapResizeModes), MapResizeModes.AutoFit)]
    public ReportExpression<MapResizeModes> ResizeMode
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapResizeModes>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public MapMarkerImage()
    {
    }

    internal MapMarkerImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ResizeMode = MapResizeModes.AutoFit;
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Image.GetEmbeddedImgDependencies(GetAncestor<Report>(), dependencies, Source.Value, Value);
    }

    internal class Definition : DefinitionStore<MapMarkerImage, Definition.Properties>
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
        ResizeMode,
        PropertyCount,
      }
    }
  }
}
