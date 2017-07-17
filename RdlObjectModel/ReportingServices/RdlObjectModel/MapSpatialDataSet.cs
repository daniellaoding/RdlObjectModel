using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapSpatialDataSet : MapSpatialData
  {
    public ReportExpression DataSetName
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

    public ReportExpression SpatialField
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

    [XmlElement(typeof (RdlCollection<ReportExpression>))]
    [XmlArrayItem("MapFieldName", typeof (ReportExpression))]
    public IList<ReportExpression> MapFieldNames
    {
      get
      {
        return (IList<ReportExpression>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapSpatialDataSet()
    {
    }

    internal MapSpatialDataSet(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      MapFieldNames = new RdlCollection<ReportExpression>();
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, DataSetName.Expression);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      DataSet dataSetByName = ancestor.GetDataSetByName(DataSetName.Expression);
      if (dataSetByName == null || dependencies.Contains(dataSetByName))
        return;
      dependencies.Add(dataSetByName);
    }

    internal class Definition : DefinitionStore<MapSpatialDataSet, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataSetName,
        SpatialField,
        MapFieldNames,
        PropertyCount,
      }
    }
  }
}
