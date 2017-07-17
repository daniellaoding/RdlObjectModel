using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ReportParameter : ReportObject, INamedObject
  {
    [XmlAttribute(typeof (string))]
    public string Name
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

    public DataTypes DataType
    {
      get
      {
        return (DataTypes) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    [DefaultValue(false)]
    public bool Nullable
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

    public DefaultValue DefaultValue
    {
      get
      {
        return (DefaultValue) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [DefaultValue(false)]
    public bool AllowBlank
    {
      get
      {
        return PropertyStore.GetBoolean(4);
      }
      set
      {
        PropertyStore.SetBoolean(4, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Prompt
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [DefaultValue(false)]
    public bool Hidden
    {
      get
      {
        return PropertyStore.GetBoolean(7);
      }
      set
      {
        PropertyStore.SetBoolean(7, value);
      }
    }

    public ValidValues ValidValues
    {
      get
      {
        return (ValidValues) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [DefaultValue(false)]
    public bool MultiValue
    {
      get
      {
        return PropertyStore.GetBoolean(9);
      }
      set
      {
        PropertyStore.SetBoolean(9, value);
      }
    }

    [DefaultValue(UsedInQueryTypes.Auto)]
    public UsedInQueryTypes UsedInQuery
    {
      get
      {
        return (UsedInQueryTypes) PropertyStore.GetInteger(10);
      }
      set
      {
        PropertyStore.SetInteger(10, (int) value);
      }
    }

    [XmlChildAttribute("Prompt", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string PromptLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    public ReportParameter()
    {
    }

    internal ReportParameter(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (CheckVisitedAndUpdate(this, visitedList))
        return true;
      ReportParameter reportParameter = rdlObj as ReportParameter;
      return reportParameter != null && DataType == reportParameter.DataType && (Nullable == reportParameter.Nullable && AllowBlank == reportParameter.AllowBlank) && (Hidden == reportParameter.Hidden && MultiValue == reportParameter.MultiValue && (UsedInQuery == reportParameter.UsedInQuery && !(Prompt != reportParameter.Prompt))) && (SemanticCompare(DefaultValue, reportParameter.DefaultValue, visitedList) && SemanticCompare(ValidValues, reportParameter.ValidValues, visitedList));
    }

    internal class Definition : DefinitionStore<ReportParameter, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        DataType,
        Nullable,
        DefaultValue,
        AllowBlank,
        Prompt,
        PromptLocID,
        Hidden,
        ValidValues,
        MultiValue,
        UsedInQuery,
      }
    }
    
    
  }
}
