using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class MapVectorLayer : MapLayer
  {
	  public string MapDataRegionName
    {
      get
      {
        return PropertyStore.GetObject<string>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapBindingFieldPair>))]
    public IList<MapBindingFieldPair> MapBindingFieldPairs
    {
      get
      {
        return (IList<MapBindingFieldPair>) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [XmlElement(typeof (RdlCollection<MapFieldDefinition>))]
    public IList<MapFieldDefinition> MapFieldDefinitions
    {
      get
      {
        return (IList<MapFieldDefinition>) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public MapSpatialData MapSpatialData
    {
      get
      {
        return (MapSpatialData) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [ValidEnumValues("MapDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Output)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(10);
      }
      set
      {
        ((EnumProperty) DefinitionStore<MapVectorLayer, Definition.Properties>.GetProperty(10)).Validate(this, (int) value);
        PropertyStore.SetInteger(10, (int) value);
      }
    }

    [DefaultValue(1000)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public int DesignerMaxRowCount { get; set; } = 1000;

	  public MapVectorLayer()
    {
    }

    internal MapVectorLayer(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapBindingFieldPairs = new RdlCollection<MapBindingFieldPair>();
      MapFieldDefinitions = new RdlCollection<MapFieldDefinition>();
      DataElementOutput = DataElementOutputTypes.Output;
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      MapDataRegionName = nameChanges.GetNewName(NameChanges.EntryType.ReportItem, MapDataRegionName);
    }

    internal class Definition : DefinitionStore<MapVectorLayer, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        VisibilityMode,
        MinimumZoom,
        MaximumZoom,
        Transparency,
        MapDataRegionName,
        MapBindingFieldPairs,
        MapFieldDefinitions,
        MapSpatialData,
        DataElementName,
        DataElementOutput,
      }
    }

    public static class Defaults
    {
      public const int DesignerMaxRowCount = 1000;
    }
  }
}
