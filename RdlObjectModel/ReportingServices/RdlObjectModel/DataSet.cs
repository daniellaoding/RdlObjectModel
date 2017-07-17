using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataSet : DataSetBase
  {
    private bool m_isModifiedSincePreview = true;

    public Query Query
    {
      get
      {
        return (Query) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
        if (value == null)
          return;
        PropertyStore.SetObject(10, null);
      }
    }

    public SharedDataSet SharedDataSet
    {
      get
      {
        return (SharedDataSet) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
        if (value == null)
          return;
        PropertyStore.SetObject(7, null);
      }
    }

    [XmlElement(typeof (RdlCollection<Field>))]
    public IList<Field> Fields
    {
      get
      {
        return (IList<Field>) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Filter>))]
    public IList<Filter> Filters
    {
      get
      {
        return (IList<Filter>) PropertyStore.GetObject(9);
      }
      set
      {
        PropertyStore.SetObject(9, value);
      }
    }

    [XmlIgnore]
    public bool IsReference => SharedDataSet != null;

	  [XmlIgnore]
    public bool IsModifiedSincePreview
    {
      get
      {
        return m_isModifiedSincePreview;
      }
      set
      {
        if (value == m_isModifiedSincePreview)
          return;
        SavePropertyValue("IsModifiedForPreview", true, (bool newValue, out bool oldValue) =>
        {
	        oldValue = true;
	        m_isModifiedSincePreview = true;
        });
        m_isModifiedSincePreview = value;
      }
    }

    public DataSet()
    {
    }

    internal DataSet(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Fields = new RdlCollection<Field>();
      Filters = new RdlCollection<Filter>();
    }

    private bool FieldExistsByName(IList<Field> list, string name)
    {
      foreach (Field field in list)
      {
        if (field.Name == name)
          return true;
      }
      return false;
    }

    public bool IsSharedDataSourceReference()
    {
      if (SharedDataSet != null)
        return true;
      DataSource dataSource = Query.DataSource;
      if (dataSource != null)
        return !string.IsNullOrEmpty(dataSource.DataSourceReference);
      return false;
    }

    public override QueryBase GetQuery()
    {
      return Query;
    }

    public IList<QueryParameter> GetQueryParameters()
    {
      if (Query != null)
        return Query.QueryParameters;
      if (SharedDataSet != null)
        return SharedDataSet.QueryParameters;
      return null;
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (!base.RdlSemanticEqualsCore(rdlObj, visitedList))
        return false;
      DataSet dataSet = rdlObj as DataSet;
      return dataSet != null && SemanticCompare(Query, dataSet.Query, visitedList) && (SemanticCompare(SharedDataSet, dataSet.SharedDataSet, visitedList) && SemanticCompare(Fields, dataSet.Fields, visitedList)) && SemanticCompare(Filters, dataSet.Filters, visitedList);
    }

    public QueryParameter GetQueryParameterByName(string name)
    {
      foreach (QueryParameter queryParameter in GetQueryParameters())
      {
        if (StringUtil.CompareClsCompliantIdentifiers(queryParameter.Name, name) == 0)
          return queryParameter;
      }
      return null;
    }

    public Field GetFieldByName(string name)
    {
      foreach (Field field in Fields)
      {
        if (StringUtil.CompareClsCompliantIdentifiers(field.Name, name) == 0)
          return field;
      }
      return null;
    }

    public IEnumerable<string> GetFieldReferences()
    {
      foreach (Field field in Fields)
        yield return "=" + ReportExpression.BuildFieldReference(field.Name);
    }

    public IEnumerable<string> GetFieldDefaultAggregateExpressions()
    {
      foreach (Field field in Fields)
        yield return field.DefaultAggregateExpression;
    }

    public IEnumerable<string> GetFieldNames()
    {
      foreach (Field field in Fields)
        yield return field.Name;
    }

    public string GetDataFieldByFieldName(string fieldName)
    {
      Field fieldByName = GetFieldByName(fieldName);
      if (fieldByName == null)
        return string.Empty;
      return fieldByName.DataField;
    }

    public static DataSet CreateEmbeddedDataSet()
    {
      return new DataSet() { Query = new Query() };
    }

    internal class Definition : DefinitionStore<DataSet, Definition.Properties>
    {
      public enum Properties
      {
        Name,
        CaseSensitivity,
        Collation,
        AccentSensitivity,
        KanatypeSensitivity,
        WidthSensitivity,
        InterpretSubtotalsAsDetails,
        Query,
        Fields,
        Filters,
        SharedDataSet,
      }
    }
  }
}
