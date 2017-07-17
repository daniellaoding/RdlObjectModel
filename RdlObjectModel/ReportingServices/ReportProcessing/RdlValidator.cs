using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.ReportingServices.ReportProcessing
{
  internal sealed class RdlValidator
  {
	  private readonly XmlReader m_reader;
    private RdlElementStack m_rdlElementStack;
    private readonly HashSet<string> m_validationNamespaces;

    public RdlValidator(XmlReader xmlReader, IEnumerable<string> validationNamespaces)
    {
      m_reader = xmlReader;
      m_validationNamespaces = new HashSet<string>(validationNamespaces);
    }

    public bool ValidateStartElement(out string message)
    {
      message = null;
      XmlSchemaComplexType schemaComplexType = null;
      ArrayList elementDeclsInContentModel = null;
      if (m_rdlElementStack == null)
        m_rdlElementStack = new RdlElementStack();
      if (m_reader.SchemaInfo != null && m_validationNamespaces.Contains(m_reader.NamespaceURI))
        schemaComplexType = m_reader.SchemaInfo.SchemaType as XmlSchemaComplexType;
      if (schemaComplexType != null)
      {
        elementDeclsInContentModel = new ArrayList();
        TraverseParticle(schemaComplexType.ContentTypeParticle, elementDeclsInContentModel);
      }
      if (schemaComplexType != null && 1 < elementDeclsInContentModel.Count && ("MapLayersType" != schemaComplexType.Name && "ReportItemsType" != schemaComplexType.Name))
        m_rdlElementStack.Add(new Hashtable(elementDeclsInContentModel.Count)
	        {
		        {
			        "_ParentName",
			        m_reader.LocalName
		        },
		        {
			        "_Type",
			        schemaComplexType
		        }
	        });
      else
        m_rdlElementStack.Add(null);
      if (0 < m_reader.Depth && m_rdlElementStack != null)
      {
        Hashtable rdlElement = m_rdlElementStack[m_reader.Depth - 1];
        if (rdlElement != null)
        {
          if (rdlElement.ContainsKey(m_reader.LocalName))
          {
            message = ValidationMessage("rdlValidationInvalidElement", (string) rdlElement["_ParentName"], m_reader.LocalName);
            return false;
          }
          rdlElement.Add(m_reader.LocalName, null);
        }
      }
      string str1 = (m_reader.GetAttribute("MustUnderstand") ?? string.Empty).Trim();
      if (!string.IsNullOrEmpty(str1))
      {
        foreach (string str2 in str1.Split())
        {
          string prefix = m_reader.LookupNamespace(str2);
          if (!m_validationNamespaces.Contains(prefix))
          {
            IXmlLineInfo reader = (IXmlLineInfo) m_reader;
            int lineNumber = reader.LineNumber;
            int linePosition = reader.LinePosition;
            message = string.Format(RDLValidatingReaderStrings.rdlValidationUnknownRequiredNamespaces, str2, prefix, "Microsoft SQL Server Fast Train 1 2016JulMR", lineNumber.ToString(CultureInfo.InvariantCulture.NumberFormat), linePosition.ToString(CultureInfo.InvariantCulture.NumberFormat));
            return false;
          }
        }
      }
      return true;
    }

    public bool ValidateEndElement(out string message)
    {
      message = null;
      bool flag = true;
      if (m_rdlElementStack != null)
      {
        Hashtable rdlElement = m_rdlElementStack[m_rdlElementStack.Count - 1];
        if (rdlElement != null)
        {
          XmlSchemaComplexType schemaComplexType = rdlElement["_Type"] as XmlSchemaComplexType;
          ArrayList elementDeclsInContentModel = new ArrayList();
          TraverseParticle(schemaComplexType.ContentTypeParticle, elementDeclsInContentModel);
          for (int index = 0; index < elementDeclsInContentModel.Count; ++index)
          {
            XmlSchemaElement xmlSchemaElement = elementDeclsInContentModel[index] as XmlSchemaElement;
            if (xmlSchemaElement.MinOccurs > new Decimal(0) && !rdlElement.ContainsKey(xmlSchemaElement.Name))
            {
              flag = false;
              message = ValidationMessage("rdlValidationMissingChildElement", rdlElement["_ParentName"] as string, xmlSchemaElement.Name);
            }
          }
          m_rdlElementStack[m_rdlElementStack.Count - 1] = null;
        }
        m_rdlElementStack.RemoveAt(m_rdlElementStack.Count - 1);
      }
      return flag;
    }

    private static void TraverseParticle(XmlSchemaParticle particle, ArrayList elementDeclsInContentModel)
    {
      if (particle is XmlSchemaElement)
      {
        XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
        elementDeclsInContentModel.Add(xmlSchemaElement);
      }
      else
      {
        if (!(particle is XmlSchemaGroupBase))
          return;
        foreach (XmlSchemaParticle particle1 in (particle as XmlSchemaGroupBase).Items)
          TraverseParticle(particle1, elementDeclsInContentModel);
      }
    }

    private string ValidationMessage(string id, string parentType, string childType)
    {
      int num1 = 0;
      int num2 = 0;
      IXmlLineInfo reader = m_reader as IXmlLineInfo;
      if (reader != null)
      {
        num1 = reader.LineNumber;
        num2 = reader.LinePosition;
      }
      return string.Format(RDLValidatingReaderStrings.ResourceManager.GetString(id), (object) parentType, (object) childType, (object) num1.ToString(CultureInfo.InvariantCulture.NumberFormat), (object) num2.ToString(CultureInfo.InvariantCulture.NumberFormat));
    }

    private sealed class RdlElementStack : ArrayList
    {
      internal Hashtable this[int index]
      {
        get
        {
          return (Hashtable) base[index];
        }
        set
        {
          base[index] = value;
        }
      }
    }
  }
}
