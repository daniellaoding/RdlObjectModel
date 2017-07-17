using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  public class RdlSerializer
  {
	  public RdlSerializerSettings Settings { get; }

	  public RdlSerializer()
    {
      Settings = new RdlSerializerSettings();
    }

    public RdlSerializer(RdlSerializerSettings settings)
    {
      Settings = settings;
    }

    public Report Deserialize(Stream stream)
    {
      return (Report) Deserialize(stream, typeof (Report));
    }

    public Report Deserialize(TextReader textReader)
    {
      return (Report) Deserialize(textReader, typeof (Report));
    }

    public Report Deserialize(XmlReader xmlReader)
    {
      return (Report) Deserialize(xmlReader, typeof (Report));
    }

    public object Deserialize(Stream stream, Type objectType)
    {
      return new RdlReader(Settings).Deserialize(stream, objectType);
    }

    public object Deserialize(TextReader textReader, Type objectType)
    {
      return new RdlReader(Settings).Deserialize(textReader, objectType);
    }

    public object Deserialize(XmlReader xmlReader, Type objectType)
    {
      return new RdlReader(Settings).Deserialize(xmlReader, objectType);
    }

    public void Serialize(Stream stream, object o)
    {
      Serialize(XmlWriter.Create(stream, GetXmlWriterSettings()), o);
    }

    public void Serialize(TextWriter textWriter, object o)
    {
      Serialize(XmlWriter.Create(textWriter, GetXmlWriterSettings()), o);
    }

    public void Serialize(XmlWriter xmlWriter, object o)
    {
      new RdlWriter(Settings).Serialize(xmlWriter, o);
    }

    private XmlWriterSettings GetXmlWriterSettings()
    {
      return new XmlWriterSettings() { Indent = true };
    }
  }
}
