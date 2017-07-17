using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  public class RdlReader : RdlReaderWriterBase
  {
    private readonly HashSet<string> m_validNamespaces = new HashSet<string>()
    {
      "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition",
      "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily"
    };
    private const string m_xsdResourceId = "Microsoft.ReportingServices.RdlObjectModel.Serialization.ReportDefinition.xsd";
    private XmlReader m_reader;
    private RdlValidator m_validator;
    private XmlSchema m_schema;

    public RdlReader(RdlSerializerSettings settings)
      : base(settings)
    {
    }

    public object Deserialize(Stream stream, Type rootType)
    {
      m_reader = XmlReader.Create(stream, GetXmlReaderSettings());
      return Deserialize(rootType);
    }

    public object Deserialize(TextReader textReader, Type rootType)
    {
      m_reader = XmlReader.Create(textReader, GetXmlReaderSettings());
      return Deserialize(rootType);
    }

    public object Deserialize(XmlReader xmlReader, Type rootType)
    {
      m_reader = XmlReader.Create(xmlReader, GetXmlReaderSettings());
      return Deserialize(rootType);
    }

    private object Deserialize(Type rootType)
    {
      List<string> stringList = new List<string>(m_validNamespaces);
      if (m_schema != null)
        stringList.Add(m_schema.TargetNamespace);
      if (Settings.ValidateXml)
        m_validator = new RdlValidator(m_reader, stringList);
      object obj = ReadRoot(rootType);
      m_reader.Close();
      return obj;
    }

    private XmlReaderSettings GetXmlReaderSettings()
    {
      XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
      xmlReaderSettings.CheckCharacters = false;
      xmlReaderSettings.IgnoreComments = true;
      xmlReaderSettings.IgnoreProcessingInstructions = true;
      xmlReaderSettings.IgnoreWhitespace = Settings.IgnoreWhitespace;
      if (Settings.ValidateXml)
      {
        xmlReaderSettings.ValidationType = ValidationType.Schema;
	      var currentAssembly = Assembly.GetExecutingAssembly();

		XmlSchema schema = XmlSchema.Read(currentAssembly.GetManifestResourceStream(m_xsdResourceId), null);
        xmlReaderSettings.Schemas.Add(schema);
        if (Settings.XmlSchema != null)
        {
          if (Settings.XmlSchema.TargetNamespace.EndsWith("/reportdefinition", StringComparison.Ordinal))
            m_schema = Settings.XmlSchema;
          xmlReaderSettings.Schemas.Add(Settings.XmlSchema);
        }
        if (m_schema == null)
          m_schema = schema;
        if (Settings.XmlValidationEventHandler != null)
          xmlReaderSettings.ValidationEventHandler += Settings.XmlValidationEventHandler;
      }
      return xmlReaderSettings;
    }

    private object ReadRoot(Type type)
    {
      try
      {
        int content = (int) m_reader.MoveToContent();
        if (m_reader.NamespaceURI != TypeMapper.GetTypeMapping(type).Namespace)
          throw new XmlException(SRErrors.NoRootElement);
        return ReadObject(type, null, 0);
      }
      catch (XmlException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        Exception innerException = ex;
        if (innerException is TargetInvocationException && innerException.InnerException != null)
          innerException = innerException.InnerException;
        string message;
        if (innerException is TargetInvocationException)
        {
          MethodBase targetSite = innerException.TargetSite;
         message = string.Format(SRErrors.DeserializationFailedMethod, (targetSite != (MethodBase) null ? targetSite.DeclaringType.Name + "." + targetSite.Name : null));
        }
        else
        {
          message = string.Format(SRErrors.DeserializationFailed, innerException.Message);
        }
        IXmlLineInfo reader = m_reader as IXmlLineInfo;
        throw reader == null ? new XmlException(message, innerException) : new XmlException(message, innerException, reader.LineNumber, reader.LinePosition);
      }
    }

    private object ReadObject(Type type, MemberMapping member, int nestingLevel)
    {
      ValidateStartElement();
      object obj = !TypeMapper.IsPrimitiveType(type) ? ReadClassObject(type, member, nestingLevel) : ReadPrimitive(type);
      ValidateEndElement();
      return obj;
    }

    private object ReadObjectContent(object value, MemberMapping member, int nestingLevel)
    {
      TypeMapping typeMapping = TypeMapper.GetTypeMapping(value.GetType());
      if (typeMapping is ArrayMapping)
        ReadArrayContent(value, (ArrayMapping) typeMapping, member, nestingLevel);
      else if (typeMapping is StructMapping)
        ReadStructContent(value, (StructMapping) typeMapping);
      else if (typeMapping is SpecialMapping)
        ReadSpecialContent(value);
      else
        m_reader.Skip();
      if (Host != null)
        Host.OnDeserialization(value);
      return value;
    }

    private object ReadPrimitive(Type type)
    {
      object obj = null;
      string str = m_reader.ReadString();
      if (type.IsPrimitive)
      {
        switch (Type.GetTypeCode(type))
        {
          case TypeCode.Boolean:
            obj = XmlConvert.ToBoolean(str);
            break;
          case TypeCode.Int16:
            obj = XmlConvert.ToInt16(str);
            break;
          case TypeCode.Int32:
            obj = XmlConvert.ToInt32(str);
            break;
          case TypeCode.Int64:
            obj = XmlConvert.ToInt64(str);
            break;
          case TypeCode.Single:
            obj = XmlConvert.ToSingle(str);
            break;
          case TypeCode.Double:
            obj = XmlConvert.ToDouble(str);
            break;
        }
      }
      else if (type == typeof (string))
      {
        obj = str;
        if (Settings.Normalize)
          obj = Regex.Replace(str, "(?<!\r)\n", "\r\n");
      }
      else if (type.IsEnum)
        obj = Enum.Parse(type, str, true);
      else if (type == typeof (Guid))
        obj = new Guid(str);
      else if (type == typeof (DateTime))
        obj = XmlCustomFormatter.ToDateTime(str);
      m_reader.Skip();
      return obj;
    }

    private object ReadClassObject(Type type, MemberMapping member, int nestingLevel)
    {
      type = GetSerializationType(type);
      object instance = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
      ReadObjectContent(instance, member, nestingLevel);
      return instance;
    }

    private object ReadSpecialContent(object obj)
    {
      IXmlSerializable xmlSerializable = (IXmlSerializable) obj;
      if (xmlSerializable != null)
        xmlSerializable.ReadXml(m_reader);
      return obj;
    }

    private object ReadArrayContent(object array, ArrayMapping mapping, MemberMapping member, int nestingLevel)
    {
      IList list = (IList) array;
      if (m_reader.IsEmptyElement)
      {
        m_reader.Skip();
      }
      else
      {
        m_reader.ReadStartElement();
        int content1 = (int) m_reader.MoveToContent();
        while (m_reader.NodeType != XmlNodeType.EndElement && m_reader.NodeType != XmlNodeType.None)
        {
          if (m_reader.NodeType == XmlNodeType.Element)
          {
            string localName = m_reader.LocalName;
            string namespaceUri = m_reader.NamespaceURI;
            Type type = null;
            bool flag = false;
            if (member != null && member.XmlAttributes.XmlArrayItems.Count > nestingLevel)
            {
              if (localName == member.XmlAttributes.XmlArrayItems[nestingLevel].ElementName)
              {
                XmlArrayItemAttribute xmlArrayItem = member.XmlAttributes.XmlArrayItems[nestingLevel];
                type = xmlArrayItem.Type;
                flag = xmlArrayItem.IsNullable;
              }
            }
            else
            {
              XmlElementAttributes elementAttributes = null;
              if (XmlOverrides != null)
              {
                XmlAttributes xmlOverride = XmlOverrides[mapping.ItemType];
                if (xmlOverride != null && xmlOverride.XmlElements != null)
                  elementAttributes = xmlOverride.XmlElements;
              }
              if (elementAttributes == null)
              {
                mapping.ElementTypes.TryGetValue(localName, out type);
              }
              else
              {
                foreach (XmlElementAttribute elementAttribute in elementAttributes)
                {
                  if (localName == elementAttribute.ElementName)
                  {
                    type = elementAttribute.Type;
                    break;
                  }
                }
              }
            }
            if (type != null)
            {
              object obj;
              if (flag && m_reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance") == "true")
              {
                m_reader.Skip();
                obj = null;
              }
              else
                obj = ReadObject(type, member, nestingLevel + 1);
              list.Add(obj);
            }
            else
              m_reader.Skip();
          }
          else
            m_reader.Skip();
          int content2 = (int) m_reader.MoveToContent();
        }
        m_reader.ReadEndElement();
      }
      return array;
    }

    private void ReadStructContent(object obj, StructMapping mapping)
    {
      int content1 = (int) m_reader.MoveToContent();
      string name = m_reader.Name;
      string namespaceUri1 = m_reader.NamespaceURI;
      ReadStructAttributes(obj, mapping);
      if (m_reader.IsEmptyElement)
      {
        m_reader.Skip();
      }
      else
      {
        m_reader.ReadStartElement();
        int content2 = (int) m_reader.MoveToContent();
        while (m_reader.NodeType != XmlNodeType.EndElement && m_reader.NodeType != XmlNodeType.None)
        {
          string localName = m_reader.LocalName;
          string namespaceUri2 = m_reader.NamespaceURI;
          string ns = namespaceUri1 == namespaceUri2 ? string.Empty : namespaceUri2;
          MemberMapping element = mapping.GetElement(localName, ns);
          Type type = null;
          if (element != null)
          {
            type = element.Type;
          }
          else
          {
            List<MemberMapping> typeNameElements = mapping.GetTypeNameElements();
            if (typeNameElements != null)
            {
              bool flag = false;
              for (int index = 0; index < typeNameElements.Count; ++index)
              {
                element = typeNameElements[index];
                XmlElementAttributes xmlElements = element.XmlAttributes.XmlElements;
                if (XmlOverrides != null)
                {
                  XmlAttributes xmlAttributes = XmlOverrides[obj.GetType()] ?? XmlOverrides[element.Type];
                  if (xmlAttributes != null && xmlAttributes.XmlElements != null)
                    xmlElements = xmlAttributes.XmlElements;
                }
                foreach (XmlElementAttribute elementAttribute in xmlElements)
                {
                  if (elementAttribute.ElementName == localName && elementAttribute.Type != null)
                  {
                    type = elementAttribute.Type;
                    flag = true;
                    break;
                  }
                }
                if (flag)
                  break;
              }
            }
          }
          if (type != null)
          {
            if (element.ChildAttributes != null)
            {
              foreach (MemberMapping childAttribute in element.ChildAttributes)
                ReadChildAttribute(obj, mapping, childAttribute);
            }
            if (element.IsReadOnly)
            {
              if (!TypeMapper.IsPrimitiveType(type))
              {
                object obj1 = element.GetValue(obj);
                if (obj1 != null)
                  ReadObjectContent(obj1, element, 0);
                else
                  m_reader.Skip();
              }
              else
                m_reader.Skip();
            }
            else
            {
              object obj1 = ReadObject(type, element, 0);
              if (obj1 != null)
                element.SetValue(obj, obj1);
            }
          }
          else
          {
            if (ns != string.Empty && m_validNamespaces.Contains(ns))
            {
              IXmlLineInfo reader = (IXmlLineInfo) m_reader;
              throw new XmlException(string.Format(RDLValidatingReaderStrings.rdlValidationInvalidMicroVersionedElement, m_reader.Name, name, reader.LineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), reader.LinePosition.ToString(CultureInfo.InvariantCulture.NumberFormat)));
            }
            m_reader.Skip();
          }
          int content3 = (int) m_reader.MoveToContent();
        }
        m_reader.ReadEndElement();
      }
    }

    private void ReadStructAttributes(object obj, StructMapping mapping)
    {
      if (!m_reader.HasAttributes)
        return;
      foreach (MemberMapping memberMapping in mapping.Attributes.Values)
      {
        if (memberMapping.Type == typeof (string))
        {
          string attribute = m_reader.GetAttribute(memberMapping.Name, memberMapping.Namespace);
          if (attribute != null)
            memberMapping.SetValue(obj, attribute);
        }
      }
    }

    private void ReadChildAttribute(object obj, StructMapping mapping, MemberMapping childMapping)
    {
      XmlAttributeAttribute xmlAttribute = childMapping.XmlAttributes.XmlAttribute;
      string attribute = m_reader.GetAttribute(xmlAttribute.AttributeName, xmlAttribute.Namespace);
      if (attribute == null)
        return;
      childMapping.SetValue(obj, attribute);
    }

    private void ValidateStartElement()
    {
      string message;
      if (Settings.ValidateXml && !m_validator.ValidateStartElement(out message))
        throw new XmlSchemaException(message + "\r\n");
    }

    private void ValidateEndElement()
    {
      string message;
      if (Settings.ValidateXml && !m_validator.ValidateEndElement(out message))
        throw new XmlSchemaException(message + "\r\n");
    }
  }
}
