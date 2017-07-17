using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapAppearanceRule : ReportObject
  {
    [ReportExpressionDefaultValue("")]
    public ReportExpression DataValue
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MapRuleDistributionTypes), MapRuleDistributionTypes.Optimal)]
    public ReportExpression<MapRuleDistributionTypes> DistributionType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MapRuleDistributionTypes>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (int), "5")]
    public ReportExpression<int> BucketCount
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<int>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ReportExpression StartValue
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public ReportExpression EndValue
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapBucket>))]
    public IList<MapBucket> MapBuckets
    {
      get
      {
        return (IList<MapBucket>) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public string LegendName
    {
      get
      {
        return PropertyStore.GetObject<string>(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ReportExpression LegendText
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [ValidEnumValues("MapDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Output)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(9);
      }
      set
      {
        ((EnumProperty) DefinitionStore<MapAppearanceRule, Definition.Properties>.GetProperty(9)).Validate(this, (int) value);
        PropertyStore.SetInteger(9, (int) value);
      }
    }

    public MapAppearanceRule()
    {
    }

    internal MapAppearanceRule(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DistributionType = MapRuleDistributionTypes.Optimal;
      BucketCount = 5;
      MapBuckets = new RdlCollection<MapBucket>();
      DataElementOutput = DataElementOutputTypes.Output;
    }

    internal class Definition : DefinitionStore<MapAppearanceRule, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataValue,
        DistributionType,
        BucketCount,
        StartValue,
        EndValue,
        MapBuckets,
        LegendName,
        LegendText,
        DataElementName,
        DataElementOutput,
      }
    }
  }
}
