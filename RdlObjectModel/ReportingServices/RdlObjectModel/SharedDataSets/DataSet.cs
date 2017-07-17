using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
  public class DataSet : DataSetBase
  {
    public Query Query
    {
      get
      {
        return (Query) PropertyStore.GetObject(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Field>))]
    [XmlArrayItem("Field", typeof (Field), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
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

    [XmlArrayItem("Filter", typeof (Filter), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
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

    public DataSet()
    {
    }

    internal DataSet(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public bool Equals(DataSet dataSet)
    {
      return dataSet != null && (Query != null || dataSet.Query == null) && ((Query == null || dataSet.Query != null) && (Query.Equals(dataSet.Query) && FieldsAreEqual(Fields, dataSet.Fields))) && (FiltersAreEqual(Filters, dataSet.Filters) && Equals((DataSetBase) dataSet));
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as DataSet);
    }

    public override int GetHashCode()
    {
      if (Query != null)
        return Query.GetHashCode();
      return base.GetHashCode();
    }

    private bool FieldsAreEqual(IList<Field> FirstList, IList<Field> SecondList)
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

    private bool FiltersAreEqual(IList<Filter> FirstList, IList<Filter> SecondList)
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

    public override QueryBase GetQuery()
    {
      return Query;
    }

    public override void Initialize()
    {
      base.Initialize();
      Query = new Query();
      Fields = new RdlCollection<Field>();
      Filters = new RdlCollection<Filter>();
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

    internal class Definition : DefinitionStore<DataSet, Definition.Properties>
    {
      internal enum Properties
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
      }
    }
  }
}
