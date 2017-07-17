using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class PropertyDefinition
  {
	  public string Name { get; }

	  protected PropertyDefinition(string name)
    {
      Name = name;
    }

    public static IPropertyDefinition Create(Type componentType, string propertyName)
    {
      PropertyInfo property = componentType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
      if (property == null)
        property = componentType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
      Type enumType = property.PropertyType;
      object defaultValue = null;
      object obj1 = null;
      object obj2 = null;
      IList<int> validValues = null;
      foreach (object customAttribute in property.GetCustomAttributes(true))
      {
        if (customAttribute is DefaultValueAttribute)
        {
          defaultValue = ((DefaultValueAttribute) customAttribute).Value;
          if (defaultValue is IExpression)
            defaultValue = ((IExpression) defaultValue).Value;
        }
        else if (customAttribute is ValidValuesAttribute)
        {
          obj1 = ((ValidValuesAttribute) customAttribute).Minimum;
          obj2 = ((ValidValuesAttribute) customAttribute).Maximum;
        }
        else if (customAttribute is ValidEnumValuesAttribute)
          validValues = ((ValidEnumValuesAttribute) customAttribute).ValidValues;
      }
      if (enumType.IsGenericType && enumType.GetGenericTypeDefinition() == typeof (ReportExpression<>))
        enumType = enumType.GetGenericArguments()[0];
      if (enumType == typeof (int))
        return new IntProperty(propertyName, (int?) defaultValue, (int?) obj1, (int?) obj2);
      if (enumType == typeof (double))
        return new DoubleProperty(propertyName, (double?) defaultValue, (double?) obj1, (double?) obj2);
      if (enumType == typeof (string))
        return new StringProperty(propertyName, (string) defaultValue);
      if (enumType == typeof (ReportSize))
        return new SizeProperty(propertyName, (ReportSize?) defaultValue, (ReportSize?) obj1, (ReportSize?) obj2);
      if (enumType == typeof (ReportColor))
        return new ColorProperty(propertyName, (ReportColor?) defaultValue);
      if (enumType.IsSubclassOf(typeof (Enum)))
        return new EnumProperty(propertyName, enumType, defaultValue, validValues);
      return null;
    }
  }
}
