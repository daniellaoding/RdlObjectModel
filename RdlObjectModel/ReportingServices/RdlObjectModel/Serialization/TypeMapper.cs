using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal static class TypeMapper
  {
    private static readonly Dictionary<Type, TypeMapping> m_mappings = new Dictionary<Type, TypeMapping>();
    private static readonly ReaderWriterLock m_lock = new ReaderWriterLock();

    public static TypeMapping GetTypeMapping(Type type)
    {
      m_lock.AcquireReaderLock(-1);
      try
      {
        if (m_mappings.ContainsKey(type))
          return m_mappings[type];
      }
      finally
      {
        m_lock.ReleaseReaderLock();
      }
      TypeMapping typeMapping = !typeof (IXmlSerializable).IsAssignableFrom(type) ? (!IsPrimitiveType(type) ? (type.IsArray || typeof (IEnumerable).IsAssignableFrom(type) ? ImportArrayMapping(type) : (TypeMapping) ImportStructMapping(type)) : ImportPrimitiveMapping(type)) : ImportSpecialMapping(type);
      m_lock.AcquireWriterLock(-1);
      try
      {
        m_mappings[type] = typeMapping;
      }
      finally
      {
        m_lock.ReleaseWriterLock();
      }
      return typeMapping;
    }

    public static bool IsPrimitiveType(Type type)
    {
      if (!type.IsEnum && !type.IsPrimitive && (!(type == typeof (string)) && !(type == typeof (Guid))))
        return type == typeof (DateTime);
      return true;
    }

    private static SpecialMapping ImportSpecialMapping(Type type)
    {
      SpecialMapping specialMapping = new SpecialMapping(type);
      foreach (XmlElementAttribute customAttribute in (IEnumerable) type.GetCustomAttributes(typeof (XmlElementClassAttribute), true))
      {
        if (customAttribute.Type == null || type == customAttribute.Type)
        {
          if (customAttribute.Namespace != null)
            specialMapping.Namespace = customAttribute.Namespace;
          if (customAttribute.ElementName != null)
          {
            specialMapping.Name = customAttribute.ElementName;
            break;
          }
          break;
        }
      }
      return specialMapping;
    }

    private static PrimitiveMapping ImportPrimitiveMapping(Type type)
    {
      return new PrimitiveMapping(type);
    }

    private static ArrayMapping ImportArrayMapping(Type type)
    {
      ArrayMapping mapping = new ArrayMapping(type);
      mapping.ElementTypes = new Dictionary<string, Type>();
      if (type.IsArray)
      {
        Type elementType = type.GetElementType();
        mapping.ItemType = elementType;
        mapping.ElementTypes.Add(elementType.Name, elementType);
      }
      else
        GetCollectionElementTypes(type, mapping);
      return mapping;
    }

    private static void GetCollectionElementTypes(Type type, ArrayMapping mapping)
    {
      PropertyInfo propertyInfo1 = null;
      for (Type type1 = type; type1 != (Type) null; type1 = type1.BaseType)
      {
        MemberInfo[] defaultMembers = type.GetDefaultMembers();
        if (defaultMembers != null)
        {
          for (int index = 0; index < defaultMembers.Length; ++index)
          {
            if ((object) (defaultMembers[index] as PropertyInfo) != null)
            {
              PropertyInfo propertyInfo2 = (PropertyInfo) defaultMembers[index];
              if (propertyInfo2.CanRead && propertyInfo2.GetGetMethod().GetParameters().Length == 1 && (propertyInfo1 == null || propertyInfo1.PropertyType == typeof (object)))
                propertyInfo1 = propertyInfo2;
            }
          }
        }
        if (propertyInfo1 != null)
          break;
      }
      if (propertyInfo1 == null)
        throw new Exception("NoDefaultAccessors");
      if (type.GetMethod("Add", new Type[1]
      {
        propertyInfo1.PropertyType
      }) == null)
        throw new Exception("NoAddMethod");
      mapping.ItemType = propertyInfo1.PropertyType;
      IList customAttributes = propertyInfo1.PropertyType.GetCustomAttributes(typeof (XmlElementClassAttribute), true);
      if (customAttributes != null && customAttributes.Count > 0)
      {
        foreach (XmlElementClassAttribute elementClassAttribute in customAttributes)
          mapping.ElementTypes.Add(elementClassAttribute.ElementName != string.Empty ? elementClassAttribute.ElementName : elementClassAttribute.Type.Name, elementClassAttribute.Type);
      }
      else
      {
        string name = propertyInfo1.PropertyType.Name;
        mapping.ElementTypes.Add(name, propertyInfo1.PropertyType);
      }
    }

    private static void GetMemberName(XmlAttributes attributes, ref string tagName, ref string ns)
    {
      if (attributes.XmlElements.Count <= 0)
        return;
      if (attributes.XmlElements[0].ElementName != null && attributes.XmlElements[0].ElementName != string.Empty)
        tagName = attributes.XmlElements[0].ElementName;
      if (attributes.XmlElements[0].Namespace == null || !(attributes.XmlElements[0].Namespace != string.Empty))
        return;
      ns = attributes.XmlElements[0].Namespace;
    }

    private static void ImportTypeMembers(StructMapping mapping, Type type)
    {
      foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        ImportPropertyInfo(mapping, property);
    }

    private static void ImportPropertyInfo(StructMapping mapping, PropertyInfo prop)
    {
      Type propertyType = prop.PropertyType;
      bool flag1 = false;
      if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        flag1 = true;
        propertyType = propertyType.GetGenericArguments()[0];
      }
      bool flag2 = false;
      XmlAttributes attributes = new XmlAttributes();
      object[] customAttributes1 = propertyType.GetCustomAttributes(true);
      object[] customAttributes2 = prop.GetCustomAttributes(true);
      bool flag3 = false;
      int length = customAttributes1.Length;
      Array.Resize(ref customAttributes1, length + customAttributes2.Length);
      customAttributes2.CopyTo(customAttributes1, length);
      foreach (object obj in customAttributes1)
      {
        Type type = obj.GetType();
        if (type == typeof (XmlIgnoreAttribute))
          return;
        if (typeof (DefaultValueAttribute).IsAssignableFrom(type))
          attributes.XmlDefaultValue = ((DefaultValueAttribute) obj).Value;
        else if (typeof (XmlElementAttribute).IsAssignableFrom(type))
        {
          XmlElementAttribute attribute = (XmlElementAttribute) obj;
          attributes.XmlElements.Add(attribute);
          if (attribute.Type != null)
          {
            if (string.IsNullOrEmpty(attribute.ElementName))
              propertyType = attribute.Type;
            else
              flag2 = true;
          }
        }
        else if (type == typeof (XmlArrayItemAttribute))
        {
          XmlArrayItemAttribute attribute = (XmlArrayItemAttribute) obj;
          int index = 0;
          while (index < attributes.XmlArrayItems.Count && attributes.XmlArrayItems[index].NestingLevel <= attribute.NestingLevel)
            ++index;
          attributes.XmlArrayItems.Insert(index, attribute);
        }
        else if (typeof (XmlAttributeAttribute).IsAssignableFrom(type))
          attributes.XmlAttribute = (XmlAttributeAttribute) obj;
        else if (type == typeof (ValidValuesAttribute) || type == typeof (ValidEnumValuesAttribute))
          flag3 = true;
      }
      string name = prop.Name;
      string empty = string.Empty;
      if (!flag2)
        GetMemberName(attributes, ref name, ref empty);
      if (mapping.GetElement(name, empty) != null || mapping.GetAttribute(name, empty) != null)
        return;
      PropertyMapping propertyMapping = new PropertyMapping(propertyType, name, empty, prop);
      propertyMapping.XmlAttributes = attributes;
      mapping.Members.Add(propertyMapping);
      if (attributes.XmlAttribute != null)
      {
        if (attributes.XmlAttribute is XmlChildAttributeAttribute)
          mapping.AddChildAttribute(propertyMapping);
        else
          mapping.Attributes[name, empty] = propertyMapping;
      }
      else
      {
        mapping.Elements[name, empty] = propertyMapping;
        if (flag2)
          mapping.AddUseTypeInfo(name, empty);
      }
      Type declaringType = prop.DeclaringType;
      if (!declaringType.IsSubclassOf(typeof (ReportObject)))
        return;
      Type type1 = declaringType.Assembly.GetType(declaringType.FullName + "+Definition+Properties", false);
      FieldInfo field;
      if (!(type1 != null) || !type1.IsEnum || !((field = type1.GetField(prop.Name)) != null))
        return;
      propertyMapping.Index = (int) field.GetRawConstantValue();
      propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Object;
      if (flag1)
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Object;
      else if (propertyType.IsSubclassOf(typeof (IContainedObject)))
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.ContainedObject;
      else if (propertyType == typeof (bool))
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Boolean;
      else if (propertyType == typeof (int))
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Integer;
      else if (propertyType == typeof (ReportSize))
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Size;
      else if (propertyType.IsEnum)
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.Enum;
      else if (propertyType.IsValueType)
        propertyMapping.TypeCode = PropertyMapping.PropertyTypeCode.ValueType;
      if (!flag3)
        return;
      Type type2 = declaringType.Assembly.GetType(declaringType.FullName + "+Definition", false);
      propertyMapping.Definition = (IPropertyDefinition) type2.InvokeMember("GetProperty", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod, null, null, new object[1]
      {
        propertyMapping.Index
      }, CultureInfo.InvariantCulture);
    }

    private static StructMapping ImportStructMapping(Type type)
    {
      StructMapping mapping = new StructMapping(type);
      foreach (XmlElementAttribute customAttribute in (IEnumerable) type.GetCustomAttributes(typeof (XmlElementClassAttribute), true))
      {
        if (customAttribute.Type == null || type == customAttribute.Type)
        {
          if (customAttribute.Namespace != null)
            mapping.Namespace = customAttribute.Namespace;
          if (customAttribute.ElementName != null)
          {
            mapping.Name = customAttribute.ElementName;
            break;
          }
          break;
        }
      }
      ImportTypeMembers(mapping, type);
      mapping.ResolveChildAttributes();
      return mapping;
    }
  }
}
