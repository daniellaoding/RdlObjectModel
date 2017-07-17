using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ExternalXmlElement : IXmlSerializable
  {
    private XmlNode m_xml;

    public XmlElement XmlElement
    {
      get
      {
        if (m_xml == null || m_xml.ChildNodes == null)
          return null;
        foreach (XmlNode childNode in m_xml.ChildNodes)
        {
          if (childNode is XmlElement)
            return (XmlElement) childNode;
        }
        return null;
      }
      set
      {
        if (value == null)
        {
          m_xml = null;
        }
        else
        {
          m_xml = new XmlDocument();
          m_xml.AppendChild(((XmlDocument) m_xml).ImportNode(value, true));
        }
      }
    }

    public ExternalXmlElement()
    {
    }

    public ExternalXmlElement(XmlElement externalXmlElement)
    {
      XmlElement = externalXmlElement;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      m_xml = new XmlSerializer(typeof (XmlNode)).Deserialize(reader) as XmlNode;
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      if (m_xml == null)
        return;
      foreach (XmlNode childNode in m_xml.ChildNodes)
        childNode.WriteTo(writer);
    }
  }
}
