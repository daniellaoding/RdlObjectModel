using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class ContainedObject : IContainedObject
  {
	  [XmlIgnore]
    public IContainedObject Parent { get; set; }
  }
}
