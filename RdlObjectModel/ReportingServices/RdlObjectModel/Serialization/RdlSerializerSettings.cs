using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  public class RdlSerializerSettings
  {
	  internal ISerializerHost Host { get; set; }

	  internal XmlAttributeOverrides XmlAttributeOverrides { get; set; }

	  internal XmlSchema XmlSchema { get; set; }

	  internal bool ValidateXml { get; set; } = true;

	  internal ValidationEventHandler XmlValidationEventHandler { get; set; }

	  internal bool IgnoreWhitespace { get; set; }

	  internal bool Normalize { get; set; } = true;
  }
}
