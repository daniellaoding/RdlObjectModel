using System;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal class PropertyMapping : MemberMapping
  {
	  public PropertyInfo Property { get; }

	  public int Index { get; set; }

	  public PropertyTypeCode TypeCode { get; set; }

	  public IPropertyDefinition Definition { get; set; }

	  public PropertyMapping(Type propertyType, string name, string ns, PropertyInfo property)
      : base(propertyType, name, ns, !property.CanWrite)
    {
      Property = property;
    }

    public override void SetValue(object obj, object value)
    {
      if (TypeCode != PropertyTypeCode.None)
      {
        IPropertyStore propertyStore = ((ReportObjectBase) obj).PropertyStore;
        if (Definition != null)
          Definition.Validate(obj, value);
        switch (TypeCode)
        {
          case PropertyTypeCode.ContainedObject:
            propertyStore.SetObject(Index, (IContainedObject) value);
            break;
          case PropertyTypeCode.Boolean:
            propertyStore.SetBoolean(Index, (bool) value);
            break;
          case PropertyTypeCode.Integer:
          case PropertyTypeCode.Enum:
            propertyStore.SetInteger(Index, (int) value);
            break;
          case PropertyTypeCode.Size:
            propertyStore.SetSize(Index, (ReportSize) value);
            break;
          default:
            propertyStore.SetObject(Index, value);
            break;
        }
      }
      else
        Property.SetValue(obj, value, null);
    }

    public override object GetValue(object obj)
    {
      if (TypeCode == PropertyTypeCode.None)
        return Property.GetValue(obj, null);
      IPropertyStore propertyStore = ((ReportObjectBase) obj).PropertyStore;
      switch (TypeCode)
      {
        case PropertyTypeCode.Boolean:
          return propertyStore.GetBoolean(Index);
        case PropertyTypeCode.Integer:
          return propertyStore.GetInteger(Index);
        case PropertyTypeCode.Size:
          return propertyStore.GetSize(Index);
        case PropertyTypeCode.Enum:
          return Enum.ToObject(Type, propertyStore.GetInteger(Index));
        case PropertyTypeCode.ValueType:
          return propertyStore.GetObject(Index) ?? Activator.CreateInstance(Type);
        default:
          return propertyStore.GetObject(Index);
      }
    }

    public override bool HasValue(object obj)
    {
      if (TypeCode == PropertyTypeCode.None)
        return Property.GetValue(obj, null) != null;
      IPropertyStore propertyStore = ((ReportObjectBase) obj).PropertyStore;
      switch (TypeCode)
      {
        case PropertyTypeCode.Boolean:
          return propertyStore.ContainsBoolean(Index);
        case PropertyTypeCode.Integer:
        case PropertyTypeCode.Enum:
          return propertyStore.ContainsInteger(Index);
        case PropertyTypeCode.Size:
          return propertyStore.ContainsSize(Index);
        default:
          return propertyStore.ContainsObject(Index);
      }
    }

    internal enum PropertyTypeCode
    {
      None,
      Object,
      ContainedObject,
      Boolean,
      Integer,
      Size,
      Enum,
      ValueType,
    }
  }
}
