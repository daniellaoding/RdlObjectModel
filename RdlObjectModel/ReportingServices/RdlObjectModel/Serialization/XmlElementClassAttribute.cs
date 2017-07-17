using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  internal sealed class XmlElementClassAttribute : XmlElementAttribute
  {
    public XmlElementClassAttribute(string elementName)
      : base(elementName)
    {
    }

    public XmlElementClassAttribute(string elementName, Type type)
      : base(elementName, type)
    {
    }
  }
}
