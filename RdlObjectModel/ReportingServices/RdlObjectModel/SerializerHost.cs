using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal class SerializerHost : ISerializerHost
  {
    public Type GetSubstituteType(Type type)
    {
      return type;
    }

    public void OnDeserialization(object value)
    {
    }

    public IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
    {
      return new ExtensionNamespace[3]
      {
	      new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false),
	      new ExtensionNamespace("cl", "http://schemas.microsoft.com/sqlserver/reporting/2010/01/componentdefinition", false),
	      new ExtensionNamespace("df", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition/defaultfontfamily", true)
      };
    }
  }
}
