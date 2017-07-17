using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Field : ReportObject, INamedObject
  {
    private string m_typeName;
    private bool m_userDefined;
    private string m_caption;

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

    public string DataField
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

    [ReportExpressionDefaultValue]
    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string TypeName
    {
      get
      {
        return m_typeName;
      }
      set
      {
        if (!(m_typeName != value))
          return;
        SavePropertyValue("TypeName", m_typeName, (string newValue, out string oldValue) =>
        {
	        oldValue = m_typeName;
	        m_typeName = newValue;
        });
        m_typeName = value;
      }
    }

    [DefaultValue(false)]
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
    public bool IsCalculatedField => DataField == null;

	  [XmlIgnore]
    public bool TreatAsNumeric
    {
      get
      {
        DataTypes? dataType = GetDataType();
        if (!dataType.HasValue)
          return IsCalculatedField;
        DataTypes? nullable1 = dataType;
        if ((nullable1.GetValueOrDefault() != DataTypes.Integer ? 0 : (nullable1.HasValue ? 1 : 0)) != 0)
          return true;
        DataTypes? nullable2 = dataType;
        if (nullable2.GetValueOrDefault() == DataTypes.Float)
          return nullable2.HasValue;
        return false;
      }
    }

    [XmlIgnore]
    public string DefaultAggregateExpression
    {
      get
      {
        string str = ReportExpression.BuildFieldReference(Name);
        if (!TreatAsNumeric)
          return "=" + str;
        return "=" + ReportExpression.BuildFunctionCall("Sum", str);
      }
    }

    public Field()
    {
    }

    internal Field(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public DataTypes? GetDataType()
    {
      if (m_typeName == null)
        return new DataTypes?();
      Type dataType;
      if (TryGetFieldDataType(out dataType))
      {
        if (dataType == typeof (string) || dataType == typeof (char))
          return new DataTypes?(DataTypes.String);
        if (dataType == typeof (int) || dataType == typeof (short) || (dataType == typeof (long) || dataType == typeof (ushort)) || (dataType == typeof (uint) || dataType == typeof (ulong) || (dataType == typeof (byte) || dataType == typeof (sbyte))))
          return new DataTypes?(DataTypes.Integer);
        if (dataType == typeof (double) || dataType == typeof (float) || dataType == typeof (Decimal))
          return new DataTypes?(DataTypes.Float);
        if (dataType == typeof (bool))
          return new DataTypes?(DataTypes.Boolean);
        if (dataType == typeof (DateTime) || dataType == typeof (DateTimeOffset) || dataType == typeof (TimeSpan))
          return new DataTypes?(DataTypes.DateTime);
      }
      return new DataTypes?();
    }

    internal bool TryGetFieldDataType(out Type dataType)
    {
      dataType = Type.GetType(m_typeName, false, true);
      return dataType != null;
    }

    public override object DeepClone()
    {
      Field field = (Field) base.DeepClone();
      field.m_typeName = m_typeName;
      field.m_caption = m_caption;
      field.m_userDefined = m_userDefined;
      return field;
    }

    public bool Equals(Field field)
    {
      if (field == null || !(DataField == field.DataField) || (!(DefaultAggregateExpression == field.DefaultAggregateExpression) || IsCalculatedField != field.IsCalculatedField) || (!(Name == field.Name) || TreatAsNumeric != field.TreatAsNumeric || (!(TypeName == field.TypeName) || UserDefined != field.UserDefined)))
        return false;
      return Value == field.Value;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Field);
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      Field field = rdlObj as Field;
      return field != null && string.Equals(DataField, field.DataField, StringComparison.OrdinalIgnoreCase) && string.Equals(TypeName, field.TypeName, StringComparison.OrdinalIgnoreCase) && Value.Equals(field.Value);
    }

    internal class Definition : DefinitionStore<Field, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        DataField,
        Value,
      }
    }
    
  }
}
