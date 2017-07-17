using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal class RdlWriter : RdlReaderWriterBase
  {
    public RdlWriter(RdlSerializerSettings settings)
      : base(settings)
    {
    }

    public void Serialize(XmlWriter writer, object root)
    {
      XmlDocument document = new XmlDocument();
      using (XmlWriter writer1 = document.CreateNavigator().AppendChild())
      {
        writer1.WriteStartDocument();
        WriteObject(writer1, root, null, null, null, 0);
        writer1.WriteEndDocument();
      }
      new NamespaceUpdater().Update(document, Host);
      document.Save(writer);
    }

    private void WriteObject(XmlWriter writer, object obj, string name, string ns, MemberMapping member, int nestingLevel)
    {
      if (obj == null || obj is IShouldSerialize && !((IShouldSerialize) obj).ShouldSerializeThis())
        return;
      WriteObjectContent(writer, null, obj, name, ns, member, nestingLevel);
    }

    private void WriteObjectContent(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, int nestingLevel)
    {
      TypeMapping typeMapping = TypeMapper.GetTypeMapping(GetSerializationType(obj));
      if (name == null)
      {
        name = typeMapping.Name;
        ns = typeMapping.Namespace;
      }
      if (typeMapping is PrimitiveMapping)
        WritePrimitive(writer, component, obj, name, ns, member, typeMapping);
      else if (typeMapping is ArrayMapping)
        WriteArray(writer, component, obj, name, ns, member, (ArrayMapping) typeMapping, nestingLevel);
      else if (typeMapping is SpecialMapping)
      {
        WriteSpecialMapping(writer, component, obj, name, ns, member);
      }
      else
      {
        if (!(typeMapping is StructMapping))
          return;
        WriteStructure(writer, component, obj, name, ns, member, (StructMapping) typeMapping);
      }
    }

    private void WriteStartElement(XmlWriter writer, object component, string name, string ns, MemberMapping member)
    {
      writer.WriteStartElement(name, ns);
      if (component == null || member == null || member.ChildAttributes == null)
        return;
      foreach (MemberMapping childAttribute in member.ChildAttributes)
        WriteChildAttribute(writer, childAttribute.GetValue(component), childAttribute);
    }

    private void WriteSpecialContent(XmlWriter writer, object obj)
    {
      IXmlSerializable xmlSerializable = (IXmlSerializable) obj;
      if (xmlSerializable == null)
        return;
      xmlSerializable.WriteXml(writer);
    }

    private void WriteSpecialMapping(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member)
    {
      WriteStartElement(writer, component, name, ns, member);
      WriteSpecialContent(writer, obj);
      writer.WriteEndElement();
    }

    private void WritePrimitiveContent(XmlWriter writer, TypeMapping mapping, object obj)
    {
      if (obj == null)
        return;
      Type type = obj.GetType();
      string text;
      if (type == typeof (string))
      {
        text = (string) obj;
        if (text == "")
          return;
      }
      else
        text = !(type == typeof (bool)) ? (!(type == typeof (DateTime)) ? obj.ToString() : XmlCustomFormatter.FromDateTime((DateTime) obj)) : ((bool) obj ? "true" : "false");
      writer.WriteString(text);
    }

    private void WritePrimitive(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, TypeMapping mapping)
    {
      WriteStartElement(writer, component, name, ns, member);
      WritePrimitiveContent(writer, mapping, obj);
      writer.WriteEndElement();
    }

    private void WriteArrayContent(XmlWriter writer, object array, ArrayMapping mapping, MemberMapping member, int nestingLevel, string ns)
    {
      Dictionary<string, Type> elementTypes = mapping.ElementTypes;
      foreach (object obj in (IEnumerable) array)
      {
        string name = null;
        bool flag = false;
        if (member != null && member.XmlAttributes.XmlArrayItems.Count > nestingLevel)
        {
          XmlArrayItemAttribute xmlArrayItem = member.XmlAttributes.XmlArrayItems[nestingLevel];
          name = xmlArrayItem.ElementName;
          flag = xmlArrayItem.IsNullable;
        }
        else
        {
          Type serializationType = GetSerializationType(obj);
          TypeMapping typeMapping = TypeMapper.GetTypeMapping(serializationType);
          if (typeMapping != null)
          {
            name = typeMapping.Name;
            ns = typeMapping.Namespace;
          }
          else
          {
            foreach (KeyValuePair<string, Type> keyValuePair in elementTypes)
            {
              if (keyValuePair.Value == serializationType)
              {
                name = keyValuePair.Key;
                break;
              }
            }
          }
        }
        if (name == null)
          throw new Exception("No array element name.");
        if (obj != null)
          WriteObject(writer, obj, name, ns, member, nestingLevel + 1);
        else if (flag)
          WriteNilElement(writer, name, ns);
      }
    }

    private bool ShouldSerializeArray(object array)
    {
      if (array is ICollection && ((ICollection) array).Count == 0)
        return false;
      foreach (object obj in (IEnumerable) array)
      {
        if (!(obj is IShouldSerialize) || ((IShouldSerialize) obj).ShouldSerializeThis())
          return true;
      }
      return false;
    }

    private void WriteArray(XmlWriter writer, object component, object array, string name, string ns, MemberMapping member, ArrayMapping mapping, int nestingLevel)
    {
      if (!ShouldSerializeArray(array))
        return;
      WriteStartElement(writer, component, name, ns, member);
      WriteArrayContent(writer, array, mapping, member, nestingLevel, ns);
      writer.WriteEndElement();
    }

    private bool ShouldSerializeValue(object component, object obj, MemberMapping memberMapping)
    {
      if (obj == null || obj is IShouldSerialize && !((IShouldSerialize) obj).ShouldSerializeThis())
        return false;
      if (component is IShouldSerialize)
      {
        switch (((IShouldSerialize) component).ShouldSerializeProperty(memberMapping.Name))
        {
          case SerializationMethod.Never:
            return false;
          case SerializationMethod.Always:
            return true;
        }
      }
      object xmlDefaultValue = memberMapping.XmlAttributes.XmlDefaultValue;
      return xmlDefaultValue == null || !obj.Equals(xmlDefaultValue);
    }

    private void WriteMember(XmlWriter writer, object component, object obj, MemberMapping memberMapping, string name, string ns)
    {
      if (!ShouldSerializeValue(component, obj, memberMapping))
        return;
      XmlElementAttributes xmlElements = memberMapping.XmlAttributes.XmlElements;
      if (xmlElements.Count > 0)
      {
        Type serializationType = GetSerializationType(obj);
        foreach (XmlElementAttribute elementAttribute in xmlElements)
        {
          if (serializationType == elementAttribute.Type)
          {
            if (!string.IsNullOrEmpty(elementAttribute.ElementName))
              name = elementAttribute.ElementName;
            if (!string.IsNullOrEmpty(elementAttribute.Namespace))
            {
              ns = elementAttribute.Namespace;
              break;
            }
            break;
          }
        }
      }
      WriteObjectContent(writer, component, obj, name, ns, memberMapping, 0);
    }

    private void WriteStructContent(XmlWriter writer, object obj, StructMapping mapping, string ns)
    {
      foreach (MemberMapping memberMapping in mapping.Attributes.Values)
      {
        if (memberMapping.Type == typeof (string) && memberMapping.XmlAttributes.XmlElements.Count == 0)
        {
          object obj1 = memberMapping.GetValue(obj);
          if (ShouldSerializeValue(obj, obj1, memberMapping))
            writer.WriteAttributeString(memberMapping.Name, memberMapping.Namespace, obj1 != null ? (string) obj1 : "");
        }
      }
      foreach (MemberMapping member in mapping.Members)
      {
        if (member.XmlAttributes.XmlAttribute == null)
        {
          string ns1 = member.Namespace != string.Empty ? member.Namespace : ns;
          WriteMember(writer, obj, member.GetValue(obj), member, member.Name, ns1);
        }
      }
    }

    private void WriteStructure(XmlWriter writer, object component, object obj, string name, string ns, MemberMapping member, StructMapping mapping)
    {
      if (obj == null)
        return;
      WriteStartElement(writer, component, name, ns, member);
      WriteStructContent(writer, obj, mapping, ns);
      writer.WriteEndElement();
    }

    private void WriteNilElement(XmlWriter writer, string name, string ns)
    {
      writer.WriteStartElement(name, ns);
      writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
      writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
      writer.WriteEndElement();
    }

    private void WriteChildAttribute(XmlWriter writer, object obj, MemberMapping mapping)
    {
      if (obj == null)
        return;
      XmlAttributeAttribute xmlAttribute = mapping.XmlAttributes.XmlAttribute;
      string str = obj.ToString();
      if (string.IsNullOrEmpty(str))
        return;
      writer.WriteAttributeString(xmlAttribute.AttributeName, xmlAttribute.Namespace, str);
    }
  }
}
