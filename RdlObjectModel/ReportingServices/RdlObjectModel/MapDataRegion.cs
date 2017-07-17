using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MapDataRegion : ReportObject, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue("")]
    public string DataSetName
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Filter>))]
    public IList<Filter> Filters
    {
      get
      {
        return (IList<Filter>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public MapMember MapMember
    {
      get
      {
        return (MapMember) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public MapDataRegion()
    {
    }

    internal MapDataRegion(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, DataSetName);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      DataSet dataSetByName = ancestor.GetDataSetByName(DataSetName);
      if (dataSetByName == null || dependencies.Contains(dataSetByName))
        return;
      dependencies.Add(dataSetByName);
    }

    internal class Definition : DefinitionStore<MapDataRegion, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        DataSetName,
        Filters,
        MapMember,
        PropertyCount,
      }
    }
  }
}
