using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class QueryParameter : ReportObject, INamedObject
  {
    private bool m_userDefined;
    private string m_caption;
    private System.Data.DbType? m_dbType;
    private bool? m_allowBlank;
    private bool? m_isNullable;
    private bool? m_isMultiValued;
    private DataSet m_validValuesDataSet;
    private DefaultValue m_defaultValue;
    private string m_validValuesDataSetQueryParameterReference;
    private string m_validValuesDataSetLabelField;
    private string m_validValuesDataSetValueField;

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

    public ReportExpression Value
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

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(false)]
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

    [XmlIgnore]
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

    [XmlIgnore]
    public bool? IsNullable
    {
      get
      {
        return m_isNullable;
      }
      set
      {
        bool? nullable = value;
        bool? isNullable = m_isNullable;
        if ((nullable.GetValueOrDefault() != isNullable.GetValueOrDefault() ? 1 : (nullable.HasValue != isNullable.HasValue ? 1 : 0)) == 0)
          return;
        SavePropertyValue("IsNullable", m_isNullable, (bool? newValue, out bool? oldValue) =>
        {
	        oldValue = m_isNullable;
	        m_isNullable = newValue;
        });
        m_isNullable = value;
      }
    }

    [XmlIgnore]
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
    public string ValidValuesDataSetLabelField
    {
      get
      {
        return m_validValuesDataSetLabelField;
      }
      set
      {
        if (!(value != m_validValuesDataSetLabelField))
          return;
        SavePropertyValue("ValidValuesDataSetLabelField", m_validValuesDataSetLabelField, (string newValue, out string oldValue) =>
        {
	        oldValue = m_validValuesDataSetLabelField;
	        m_validValuesDataSetLabelField = newValue;
        });
        m_validValuesDataSetLabelField = value;
      }
    }

    [XmlIgnore]
    public string ValidValuesDataSetValueField
    {
      get
      {
        return m_validValuesDataSetValueField;
      }
      set
      {
        if (!(value != m_validValuesDataSetValueField))
          return;
        SavePropertyValue("ValidValuesDataSetValueField", m_validValuesDataSetValueField, (string newValue, out string oldValue) =>
        {
	        oldValue = m_validValuesDataSetValueField;
	        m_validValuesDataSetValueField = newValue;
        });
        m_validValuesDataSetValueField = value;
      }
    }

    [XmlIgnore]
    public DefaultValue DefaultValue
    {
      get
      {
        return m_defaultValue;
      }
      set
      {
        if (value == m_defaultValue)
          return;
        SavePropertyValue("DefaultValue", m_defaultValue, (DefaultValue newValue, out DefaultValue oldValue) =>
        {
	        oldValue = m_defaultValue;
	        m_defaultValue = newValue;
        });
        m_defaultValue = value;
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

    public QueryParameter()
    {
    }

    internal QueryParameter(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public string GetReportParameterName()
    {
      if (!(Value != null) || !Value.IsExpression)
        return null;
      Match match = Regex.Match(Value.Expression, "^=Parameters!(\\w+)\\.Value$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
      if (match.Success)
        return match.Groups[1].Value;
      return null;
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      QueryParameter queryParameter = rdlObj as QueryParameter;
      return queryParameter != null && CompareReportParamterExpression(Value, this, queryParameter.Value, queryParameter, visitedList);
    }

    public override object DeepClone()
    {
      QueryParameter queryParameter = (QueryParameter) base.DeepClone();
      queryParameter.m_allowBlank = m_allowBlank;
      queryParameter.m_caption = m_caption;
      queryParameter.m_dbType = m_dbType;
      queryParameter.m_isNullable = m_isNullable;
      queryParameter.m_isMultiValued = m_isMultiValued;
      queryParameter.m_validValuesDataSet = m_validValuesDataSet != null ? (DataSet) m_validValuesDataSet.DeepClone() : null;
      queryParameter.m_defaultValue = m_defaultValue != null ? (DefaultValue) m_defaultValue.DeepClone() : null;
      queryParameter.m_validValuesDataSetQueryParameterReference = m_validValuesDataSetQueryParameterReference;
      queryParameter.m_validValuesDataSetLabelField = m_validValuesDataSetLabelField;
      queryParameter.m_validValuesDataSetValueField = m_validValuesDataSetValueField;
      return queryParameter;
    }

    internal class Definition : DefinitionStore<QueryParameter, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        Value,
      }
    }
  }
}
