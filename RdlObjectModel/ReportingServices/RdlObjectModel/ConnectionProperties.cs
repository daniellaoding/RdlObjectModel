using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ConnectionProperties : ReportObject
  {
    public string DataProvider
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

    public ReportExpression ConnectString
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [DefaultValue(false)]
    public bool IntegratedSecurity
    {
      get
      {
        return PropertyStore.GetBoolean(2);
      }
      set
      {
        PropertyStore.SetBoolean(2, value);
      }
    }

    [DefaultValue("")]
    public string Prompt
    {
      get
      {
        return (string) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlChildAttribute("Prompt", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string PromptLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public ConnectionProperties()
    {
    }

    internal ConnectionProperties(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      ConnectionProperties connectionProperties = rdlObj as ConnectionProperties;
      return connectionProperties != null && IntegratedSecurity == connectionProperties.IntegratedSecurity && string.Equals(DataProvider, connectionProperties.DataProvider, StringComparison.OrdinalIgnoreCase) && string.Equals(ConnectString.Expression, connectionProperties.ConnectString.Expression, !ConnectString.IsExpression || !connectionProperties.ConnectString.IsExpression ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
    }

    internal class Definition : DefinitionStore<ConnectionProperties, Definition.Properties>
    {
      internal enum Properties
      {
        DataProvider,
        ConnectString,
        IntegratedSecurity,
        Prompt,
        PromptLocID,
      }
    }
    
  }
}
