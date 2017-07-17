using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class EnumProperty : PropertyDefinition, IPropertyDefinition
  {
	  private readonly IList<int> m_validIntValues;
    private IList<object> m_validValues;
    private readonly Type m_type;

    public object Default { get; }

	  public IList<object> ValidValues
    {
      get
      {
        if (m_validValues == null)
        {
          object[] objArray;
          if (m_validIntValues != null)
          {
            objArray = new object[m_validValues.Count];
            for (int index = 0; index < m_validIntValues.Count; ++index)
              objArray[index] = Enum.ToObject(m_type, m_validValues[index]);
          }
          else
          {
            Array values = Enum.GetValues(m_type);
            objArray = new object[values.Length];
            values.CopyTo(objArray, 0);
          }
          m_validValues = new ReadOnlyCollection<object>(objArray);
        }
        return m_validValues;
      }
    }

    object IPropertyDefinition.Minimum => null;

	  object IPropertyDefinition.Maximum => null;

	  public EnumProperty(string name, Type enumType, object defaultValue, IList<int> validValues)
      : base(name)
    {
      m_type = enumType;
      Default = defaultValue;
      m_validIntValues = validValues;
    }

    void IPropertyDefinition.Validate(object component, object value)
    {
      if (value is IExpression)
      {
        if (((IExpression) value).IsExpression)
          return;
        value = ((IExpression) value).Value;
      }
      if (!(value.GetType() == m_type))
        throw new ArgumentException("Invalid type.");
      Validate(component, (int) value);
    }

    public void Validate(object component, int value)
    {
      if (m_validIntValues != null && !m_validIntValues.Contains(value))
      {
        object obj = Enum.ToObject(m_type, value);
        throw new ArgumentConstraintException(component, Name, obj, null, string.Format(SRErrors.InvalidParam,Name, obj));
      }
    }
  }
}
