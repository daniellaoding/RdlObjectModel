using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  public abstract class RdlReaderWriterBase
  {
	  protected RdlSerializerSettings Settings { get; }

	  protected ISerializerHost Host { get; }

	  protected XmlAttributeOverrides XmlOverrides { get; }

	  protected RdlReaderWriterBase(RdlSerializerSettings settings)
    {
      Settings = settings;
      if (Settings == null)
        return;
      Host = Settings.Host;
      XmlOverrides = Settings.XmlAttributeOverrides;
    }

    protected Type GetSerializationType(object obj)
    {
      return GetSerializationType(obj.GetType());
    }

    protected Type GetSerializationType(Type type)
    {
      if (Host != null)
        return Host.GetSubstituteType(type);
      return type;
    }
  }
}
