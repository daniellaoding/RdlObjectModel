using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class MetadataValue : ReportObject, IXmlSerializable
  {
    public string Value
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

    public MetadataValue()
    {
    }

    internal MetadataValue(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public MetadataValue(string value)
    {
      Value = value;
    }

    public override object DeepClone()
    {
      return new MetadataValue(Value);
    }

    public XmlSchema GetSchema()
    {
      return null;
    }

    public void ReadXml(XmlReader reader)
    {
      int content = (int) reader.MoveToContent();
      Value = reader.ReadElementContentAsString();
    }

    public void WriteXml(XmlWriter writer)
    {
      writer.WriteString(Value);
    }

    internal class Definition : DefinitionStore<MetadataValue, Definition.Properties>
    {
      internal enum Properties
      {
        Value,
      }
    }
  }
}
