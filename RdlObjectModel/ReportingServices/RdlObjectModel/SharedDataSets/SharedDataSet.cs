using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
  [XmlElementClass("SharedDataSet", typeof (SharedDataSet), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
  public class SharedDataSet : ReportObject, IGlobalNamedObject, INamedObject, IDataScopeService, IDataScope, IContainedObject
  {
	  [XmlIgnore]
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

    public string Description
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

    public DataSet DataSet
    {
      get
      {
        return (DataSet) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string ReportServerUrl { get; set; }

	  [DefaultValue(null)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public DataSetInfo SharedDataSetInfo
    {
      get
      {
        return PropertyStore.GetObject<DataSetInfo>(134217729);
      }
      set
      {
        PropertyStore.SetObject(134217729, value);
      }
    }

    Group IDataScope.Group => null;

	  public SharedDataSet()
    {
    }

    internal SharedDataSet(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public SharedDataSet(RdlObjectModel.DataSet dataSet, string description, string dataSourceReference, string reportServerUrl)
    {
      DataSet = ConvertDataSet(dataSet);
      DataSet.Query.DataSourceReference = dataSourceReference;
      Description = description;
      ReportServerUrl = reportServerUrl;
    }

    private static bool ContainsMappingName(List<MemberMapping> list, string name)
    {
      foreach (TypeMapping typeMapping in list)
      {
        if (typeMapping.Name.Equals(name))
          return true;
      }
      return false;
    }

    public override void Initialize()
    {
      base.Initialize();
    }

    public static void Serialize(Stream stream, SharedDataSet sharedDataSet)
    {
      if (sharedDataSet == null)
        return;
      CreateSerializer().Serialize(stream, sharedDataSet);
    }

    public static SharedDataSet Deserialize(Stream stream)
    {
      return (SharedDataSet) CreateSerializer().Deserialize(stream, typeof (SharedDataSet));
    }

    internal static RdlSerializer CreateSerializer()
    {
      SharedDataSetSerializerHost setSerializerHost = new SharedDataSetSerializerHost();
      return new RdlSerializer(new RdlSerializerSettings()
      {
        Host = setSerializerHost
      });
    }

    public IEnumerable<IDataScope> GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      return GetContainingDataScopes();
    }

    public bool Equals(SharedDataSet sharedDataSet)
    {
      if (sharedDataSet == null || !(Description == sharedDataSet.Description) || (!(ReportServerUrl == sharedDataSet.ReportServerUrl) || !(Name == sharedDataSet.Name)) || (!(Description == sharedDataSet.Description) || SharedDataSetInfo != sharedDataSet.SharedDataSetInfo))
        return false;
      return DataSet.Equals(sharedDataSet.DataSet);
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as SharedDataSet);
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    public static DataSet ConvertDataSet(RdlObjectModel.DataSet dataSet)
    {
      if (dataSet == null)
        throw new ArgumentNullException("dataSet");
      DataSet dataSet1 = new DataSet();
      StructMapping typeMapping1 = (StructMapping) TypeMapper.GetTypeMapping(typeof (RdlObjectModel.DataSet));
      StructMapping typeMapping2 = (StructMapping) TypeMapper.GetTypeMapping(typeof (DataSet));
      foreach (MemberMapping member in typeMapping1.Members)
      {
        if (member.HasValue(dataSet) && member.Name == "Fields")
        {
          RdlCollection<Field> rdlCollection1 = (RdlCollection<Field>) member.GetValue(dataSet);
          RdlCollection<Field> rdlCollection2 = new RdlCollection<Field>();
          foreach (Field field in rdlCollection1)
            rdlCollection2.Add(field);
          dataSet1.Fields = rdlCollection2;
        }
        if (member.HasValue(dataSet) && member.Name != "Query")
        {
          object obj = member.GetValue(dataSet);
          member.SetValue(dataSet1, CloneObject(obj));
        }
      }
      StructMapping typeMapping3 = (StructMapping) TypeMapper.GetTypeMapping(typeof (RdlObjectModel.Query));
      StructMapping typeMapping4 = (StructMapping) TypeMapper.GetTypeMapping(typeof (Query));
      foreach (MemberMapping member in typeMapping3.Members)
      {
        if (member.HasValue(dataSet.Query))
        {
          if (member.Name != "DataSourceName" && member.Name != "QueryParameters")
          {
            object obj = member.GetValue(dataSet.Query);
            if (ContainsMappingName(typeMapping4.Members, member.Name))
              member.SetValue(dataSet1.Query, CloneObject(obj));
          }
          else if (member.Name == "QueryParameters")
          {
            foreach (QueryParameter parameter in (Collection<QueryParameter>) member.GetValue(dataSet.Query))
              dataSet1.Query.DataSetParameters.Add(ConvertQueryParameter(parameter));
          }
        }
      }
      return dataSet1;
    }

    public static RdlObjectModel.DataSet ConvertDataSet(DataSet dataSet)
    {
      if (dataSet == null)
        return null;
      RdlObjectModel.DataSet dataSet1 = new RdlObjectModel.DataSet();
      StructMapping typeMapping1 = (StructMapping) TypeMapper.GetTypeMapping(typeof (DataSet));
      StructMapping typeMapping2 = (StructMapping) TypeMapper.GetTypeMapping(typeof (RdlObjectModel.DataSet));
      foreach (MemberMapping member in typeMapping1.Members)
      {
        if (member.HasValue(dataSet) && member.Name == "Fields")
        {
          RdlCollection<Field> rdlCollection1 = (RdlCollection<Field>) member.GetValue(dataSet);
          RdlCollection<Field> rdlCollection2 = new RdlCollection<Field>();
          foreach (Field field in rdlCollection1)
            rdlCollection2.Add(field);
          dataSet1.Fields = rdlCollection2;
        }
        if (member.HasValue(dataSet) && member.Name != "Query")
        {
          object obj = member.GetValue(dataSet);
          member.SetValue(dataSet1, CloneObject(obj));
        }
      }
      StructMapping typeMapping3 = (StructMapping) TypeMapper.GetTypeMapping(typeof (Query));
      StructMapping typeMapping4 = (StructMapping) TypeMapper.GetTypeMapping(typeof (RdlObjectModel.Query));
      foreach (MemberMapping member in typeMapping3.Members)
      {
        if (member.HasValue(dataSet.Query))
        {
          if (member.Name != "DataSourceReference" && member.Name != "DataSetParameters")
          {
            object obj = member.GetValue(dataSet.Query);
            if (ContainsMappingName(typeMapping4.Members, member.Name))
              member.SetValue(dataSet1.Query, CloneObject(obj));
          }
          else if (member.Name == "DataSetParameters")
          {
            foreach (DataSetParameter parameter in (Collection<DataSetParameter>) member.GetValue(dataSet.Query))
            {
              if (!parameter.ReadOnly)
                dataSet1.Query.QueryParameters.Add(ConvertDataSetParameter(parameter));
            }
          }
          if (dataSet1.Query == null)
            dataSet1.Query = new RdlObjectModel.Query();
          dataSet1.Query.DataSourceName = Path.GetFileNameWithoutExtension(dataSet.Query.DataSourceReference);
        }
      }
      return dataSet1;
    }

    internal static DataSetParameter ConvertQueryParameter(QueryParameter parameter)
    {
      DataSetParameter dataSetParameter1 = new DataSetParameter();
      bool flag = false;
      dataSetParameter1.Name = parameter.Name;
      dataSetParameter1.DbType = parameter.DbType;
      dataSetParameter1.IsMultiValued = parameter.IsMultiValued;
      dataSetParameter1.UserDefined = parameter.UserDefined;
      DataSetParameter dataSetParameter2 = dataSetParameter1;
      bool? isNullable = parameter.IsNullable;
      int num = isNullable.HasValue ? (isNullable.GetValueOrDefault() ? 1 : 0) : 0;
      dataSetParameter2.Nullable = num != 0;
      ReportExpression? expression1 = new ReportExpression?();
      if (parameter.DefaultValue != null && parameter.DefaultValue.Values != null && (parameter.DefaultValue.Values.Count > 0 && !ReportExpression.TryBuildArray(parameter.DefaultValue.Values, out expression1)))
        expression1 = parameter.DefaultValue.Values[0];
      if (expression1.HasValue || parameter.Value != null)
      {
        ExpressionParser.Expression expression2 = !expression1.HasValue ? ExpressionFactory.CreateExpression(parameter.Value.Expression, true) : ExpressionFactory.CreateExpression(expression1.Value.Expression, true);
        if (expression2 != null && expression2.ObjectDependencyList != null && expression2.ObjectDependencyList.Count > 0)
        {
          foreach (IInternalExpression objectDependency in expression2.ObjectDependencyList)
          {
            if (objectDependency is FunctionReportParameter)
            {
              dataSetParameter1.Nullable = true;
              flag = true;
              break;
            }
          }
        }
        if (!flag && expression1.HasValue)
          dataSetParameter1.DefaultValue = new ReportExpression?(expression1.Value);
      }
      return dataSetParameter1;
    }

    internal static QueryParameter ConvertDataSetParameter(DataSetParameter parameter)
    {
      QueryParameter queryParameter = new QueryParameter();
      queryParameter.Name = parameter.Name;
      if (parameter.DefaultValue.HasValue)
      {
        queryParameter.DefaultValue = new DefaultValue();
        queryParameter.DefaultValue.Values.Add(parameter.DefaultValue);
      }
      queryParameter.DbType = parameter.DbType;
      queryParameter.IsMultiValued = parameter.IsMultiValued;
      queryParameter.UserDefined = parameter.UserDefined;
      if (parameter.Nullable)
        queryParameter.IsNullable = new bool?(parameter.Nullable);
      return queryParameter;
    }

    internal class SharedDataSetSerializerHost : ISerializerHost
    {
      public Type GetSubstituteType(Type type)
      {
        return type;
      }

      public void OnDeserialization(object value)
      {
      }

      public IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
      {
        return new ExtensionNamespace[1]
        {
	        new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false)
        };
      }
    }

    internal class Definition : DefinitionStore<SharedDataSet, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        Description,
        DataSet,
      }
    }

    internal class RDDefinition : DefinitionStore<DataSet, RDDefinition.Properties>
    {
      public enum Properties
      {
        SharedDataSetInfo = 134217729,
      }
    }
    
  }
}
