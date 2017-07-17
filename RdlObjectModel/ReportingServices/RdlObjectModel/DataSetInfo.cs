namespace Microsoft.ReportingServices.RdlObjectModel
{
  public sealed class DataSetInfo : ReportObject
  {
    public string DataSetName
    {
      get
      {
        return PropertyStore.GetObject<string>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public string SchemaPath
    {
      get
      {
        return PropertyStore.GetObject<string>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public string TableName
    {
      get
      {
        return PropertyStore.GetObject<string>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public string TableAdapterFillMethod
    {
      get
      {
        return PropertyStore.GetObject<string>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public string TableAdapterGetDataMethod
    {
      get
      {
        return PropertyStore.GetObject<string>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public string TableAdapterName
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

    public string ObjectDataSourceSelectMethod
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

    public string ObjectDataSourceSelectMethodSignature
    {
      get
      {
        return PropertyStore.GetObject<string>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    public string ObjectDataSourceType
    {
      get
      {
        return PropertyStore.GetObject<string>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    public DataSetInfo()
    {
    }

    internal DataSetInfo(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public bool Equals(DataSetInfo obj)
    {
      if (obj == null || !(obj.DataSetName == DataSetName) || (!(obj.SchemaPath == SchemaPath) || !(obj.TableName == TableName)) || (!(obj.TableAdapterFillMethod == TableAdapterFillMethod) || !(obj.TableAdapterGetDataMethod == TableAdapterGetDataMethod) || (!(obj.TableAdapterName == TableAdapterName) || !(obj.ObjectDataSourceSelectMethod == ObjectDataSourceSelectMethod))) || !(obj.ObjectDataSourceSelectMethodSignature == ObjectDataSourceSelectMethodSignature))
        return false;
      return obj.ObjectDataSourceType == ObjectDataSourceType;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as DataSetInfo);
    }

    public override int GetHashCode()
    {
      return (DataSetName ?? string.Empty).GetHashCode() ^ (SchemaPath ?? string.Empty).GetHashCode() ^ (TableName ?? string.Empty).GetHashCode() ^ (TableAdapterFillMethod ?? string.Empty).GetHashCode() ^ (TableAdapterGetDataMethod ?? string.Empty).GetHashCode() ^ (TableAdapterName ?? string.Empty).GetHashCode() ^ (ObjectDataSourceSelectMethod ?? string.Empty).GetHashCode() ^ (ObjectDataSourceSelectMethodSignature ?? string.Empty).GetHashCode() ^ (ObjectDataSourceType ?? string.Empty).GetHashCode();
    }

    internal class Definition : DefinitionStore<DataSetInfo, Definition.Properties>
    {
      public enum Properties
      {
        DataSetName,
        SchemaPath,
        TableName,
        TableAdapterFillMethod,
        TableAdapterGetDataMethod,
        TableAdapterName,
        ObjectDataSourceSelectMethod,
        ObjectDataSourceSelectMethodSignature,
        ObjectDataSourceType,
      }
    }
  }
}
