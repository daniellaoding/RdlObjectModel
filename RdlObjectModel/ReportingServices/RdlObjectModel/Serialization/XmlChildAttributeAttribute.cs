using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  internal sealed class XmlChildAttributeAttribute : XmlAttributeAttribute
  {
	  public string ElementName { get; }

	  public XmlChildAttributeAttribute(string elementName, string attributeName)
      : this(elementName, attributeName, null)
    {
    }

    public XmlChildAttributeAttribute(string elementName, string attributeName, string namespaceUri)
      : base(attributeName)
    {
      ElementName = elementName;
      Namespace = namespaceUri;
    }
  }
}
