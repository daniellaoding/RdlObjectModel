using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
  public class Query : QueryBase
  {
    public string DataSourceReference
    {
      get
      {
        return (string) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlElement(typeof (RdlCollection<DataSetParameter>))]
    [XmlArrayItem("DataSetParameter", typeof (DataSetParameter), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
    public IList<DataSetParameter> DataSetParameters
    {
      get
      {
        return (IList<DataSetParameter>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public Query()
    {
    }

    internal Query(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      DataSourceReference = "";
      DataSetParameters = new RdlCollection<DataSetParameter>();
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      string.IsNullOrEmpty(DataSourceReference);
    }

    public bool Equals(Query query)
    {
      if (query == null || !DataSetParametersAreEqual(DataSetParameters, query.DataSetParameters) || !(DataSourceReference == query.DataSourceReference))
        return false;
      return Equals((QueryBase) query);
    }

    private bool DataSetParametersAreEqual(IList<DataSetParameter> FirstList, IList<DataSetParameter> SecondList)
    {
      if (FirstList.Count != SecondList.Count)
        return false;
      for (int index = 0; index < FirstList.Count; ++index)
      {
        if (!FirstList[index].Equals(SecondList[index]))
          return false;
      }
      return true;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Query);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public DataSetParameter GetDataSetParameterByName(string name)
    {
      foreach (DataSetParameter dataSetParameter in DataSetParameters)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(dataSetParameter.Name, name) == 0)
          return dataSetParameter;
      }
      return null;
    }

    internal class Definition : DefinitionStore<Query, Definition.Properties>
    {
      internal enum Properties
      {
        CommandType,
        CommandText,
        Timeout,
        DataSourceReference,
        DataSetParameters,
      }
    }
    
  }
}
