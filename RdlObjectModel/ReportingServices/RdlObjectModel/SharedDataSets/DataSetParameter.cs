using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
  public class DataSetParameter : ReportObject, INamedObject
  {
    private bool m_userDefined;
    private string m_caption;
    private System.Data.DbType? m_dbType;
    private bool? m_allowBlank;
    private bool? m_isMultiValued;
    private DataSet m_validValuesDataSet;
    private string m_validValuesDataSetQueryParameterReference;

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

    public ReportExpression? DefaultValue
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression?>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public bool ReadOnly
    {
      get
      {
        return PropertyStore.GetBoolean(2);
      }
      set
      {
        PropertyStore.SetBoolean(2, value);
      }
    }

    public bool Nullable
    {
      get
      {
        return PropertyStore.GetBoolean(3);
      }
      set
      {
        PropertyStore.SetBoolean(3, value);
      }
    }

    public bool OmitFromQuery
    {
      get
      {
        return PropertyStore.GetBoolean(4);
      }
      set
      {
        PropertyStore.SetBoolean(4, value);
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public bool UserDefined
    {
      get
      {
        return m_userDefined;
      }
      set
      {
        if (m_userDefined == value)
          return;
        SavePropertyValue("UserDefined", m_userDefined, (bool newValue, out bool oldValue) =>
        {
	        oldValue = m_userDefined;
	        m_userDefined = newValue;
        });
        m_userDefined = value;
      }
    }

    [XmlIgnore]
    public bool? AllowBlank
    {
      get
      {
        return m_allowBlank;
      }
      set
      {
        bool? nullable = value;
        bool? allowBlank = m_allowBlank;
        if ((nullable.GetValueOrDefault() != allowBlank.GetValueOrDefault() ? 1 : (nullable.HasValue != allowBlank.HasValue ? 1 : 0)) == 0)
          return;
        SavePropertyValue("AllowBlank", m_allowBlank, (bool? newValue, out bool? oldValue) =>
        {
	        oldValue = m_allowBlank;
	        m_allowBlank = newValue;
        });
        m_allowBlank = value;
      }
    }

    [XmlIgnore]
    public string Caption
    {
      get
      {
        return m_caption;
      }
      set
      {
        if (!(value != m_caption))
          return;
        SavePropertyValue("Caption", m_caption, (string newValue, out string oldValue) =>
        {
	        oldValue = m_caption;
	        m_caption = newValue;
        });
        m_caption = value;
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public System.Data.DbType? DbType
    {
      get
      {
        return m_dbType;
      }
      set
      {
        System.Data.DbType? nullable = value;
        System.Data.DbType? dbType = m_dbType;
        if ((nullable.GetValueOrDefault() != dbType.GetValueOrDefault() ? 1 : (nullable.HasValue != dbType.HasValue ? 1 : 0)) == 0)
          return;
        SavePropertyValue("DbType", m_dbType, (System.Data.DbType? newValue, out System.Data.DbType? oldValue) =>
        {
	        oldValue = m_dbType;
	        m_dbType = newValue;
        });
        m_dbType = value;
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public bool? IsMultiValued
    {
      get
      {
        return m_isMultiValued;
      }
      set
      {
        bool? nullable = value;
        bool? isMultiValued = m_isMultiValued;
        if ((nullable.GetValueOrDefault() != isMultiValued.GetValueOrDefault() ? 1 : (nullable.HasValue != isMultiValued.HasValue ? 1 : 0)) == 0)
          return;
        SavePropertyValue("IsMultiValued", m_isMultiValued, (bool? newValue, out bool? oldValue) =>
        {
	        oldValue = m_isMultiValued;
	        m_isMultiValued = newValue;
        });
        m_isMultiValued = value;
      }
    }

    [XmlIgnore]
    public DataSet ValidValuesDataSet
    {
      get
      {
        return m_validValuesDataSet;
      }
      set
      {
        if (value == m_validValuesDataSet)
          return;
        SavePropertyValue("ValidValuesDataSet", m_validValuesDataSet, (DataSet newValue, out DataSet oldValue) =>
        {
	        oldValue = m_validValuesDataSet;
	        m_validValuesDataSet = newValue;
        });
        m_validValuesDataSet = value;
      }
    }

    [XmlIgnore]
    public string ValidValuesDataSetQueryParameterReference
    {
      get
      {
        return m_validValuesDataSetQueryParameterReference;
      }
      set
      {
        if (!(value != m_validValuesDataSetQueryParameterReference))
          return;
        SavePropertyValue("ValidValuesDataSetQueryParameterReference", m_validValuesDataSetQueryParameterReference, (string newValue, out string oldValue) =>
        {
	        oldValue = m_validValuesDataSetQueryParameterReference;
	        m_validValuesDataSetQueryParameterReference = newValue;
        });
        m_validValuesDataSetQueryParameterReference = value;
      }
    }

    public DataSetParameter()
    {
    }

    internal DataSetParameter(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ReadOnly = false;
      Nullable = false;
      OmitFromQuery = false;
    }

    public override object DeepClone()
    {
      DataSetParameter dataSetParameter = (DataSetParameter) base.DeepClone();
      dataSetParameter.m_allowBlank = m_allowBlank;
      dataSetParameter.m_caption = m_caption;
      dataSetParameter.m_dbType = m_dbType;
      dataSetParameter.m_isMultiValued = m_isMultiValued;
      dataSetParameter.Nullable = Nullable;
      dataSetParameter.OmitFromQuery = OmitFromQuery;
      dataSetParameter.m_validValuesDataSet = m_validValuesDataSet != null ? (DataSet) m_validValuesDataSet.DeepClone() : null;
      dataSetParameter.m_validValuesDataSetQueryParameterReference = m_validValuesDataSetQueryParameterReference;
      return dataSetParameter;
    }

    public bool Equals(DataSetParameter dataSetParameter)
    {
      if (dataSetParameter == null)
        return false;
      bool? allowBlank1 = AllowBlank;
      bool? allowBlank2 = dataSetParameter.AllowBlank;
      if ((allowBlank1.GetValueOrDefault() != allowBlank2.GetValueOrDefault() ? 0 : (allowBlank1.HasValue == allowBlank2.HasValue ? 1 : 0)) != 0 && Caption == dataSetParameter.Caption)
      {
        System.Data.DbType? dbType1 = DbType;
        System.Data.DbType? dbType2 = dataSetParameter.DbType;
        if ((dbType1.GetValueOrDefault() != dbType2.GetValueOrDefault() ? 0 : (dbType1.HasValue == dbType2.HasValue ? 1 : 0)) != 0)
        {
          ReportExpression? defaultValue1 = DefaultValue;
          ReportExpression? defaultValue2 = dataSetParameter.DefaultValue;
          if ((defaultValue1.HasValue != defaultValue2.HasValue ? 0 : (!defaultValue1.HasValue ? 1 : (defaultValue1.GetValueOrDefault() == defaultValue2.GetValueOrDefault() ? 1 : 0))) != 0)
          {
            bool? isMultiValued1 = IsMultiValued;
            bool? isMultiValued2 = dataSetParameter.IsMultiValued;
            if ((isMultiValued1.GetValueOrDefault() != isMultiValued2.GetValueOrDefault() ? 0 : (isMultiValued1.HasValue == isMultiValued2.HasValue ? 1 : 0)) != 0 && Name == dataSetParameter.Name && (Nullable == dataSetParameter.Nullable && OmitFromQuery == dataSetParameter.OmitFromQuery) && (ReadOnly == dataSetParameter.ReadOnly && UserDefined == dataSetParameter.UserDefined && ValidValuesDataSet == dataSetParameter.ValidValuesDataSet))
              return ValidValuesDataSetQueryParameterReference == dataSetParameter.ValidValuesDataSetQueryParameterReference;
          }
        }
      }
      return false;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as DataSetParameter);
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    internal class Definition : DefinitionStore<DataSetParameter, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        DefaultValue,
        ReadOnly,
        Nullable,
        OmitFromQuery,
      }
    }
  }
}
