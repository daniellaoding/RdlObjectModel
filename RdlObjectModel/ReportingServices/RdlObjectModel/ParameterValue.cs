using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ParameterValue : ReportObject
  {
    [DefaultValue(null)]
    public ReportExpression? Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression?>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Label
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

    [DefaultValue("")]
    [XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string LabelLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ParameterValue()
    {
    }

    internal ParameterValue(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      ParameterValue parameterValue = rdlObj as ParameterValue;
      return parameterValue != null && (!Value.HasValue || parameterValue.Value.HasValue) && (Value.HasValue || !parameterValue.Value.HasValue) && (!Value.HasValue || Value.Value.Equals(parameterValue.Value.Value));
    }

    internal class Definition : DefinitionStore<ParameterValue, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Value,
        Label,
        LabelLocID,
      }
    }
  }
}
