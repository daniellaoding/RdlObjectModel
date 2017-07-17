using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ValidValues : ReportObject
  {
    public DataSetReference DataSetReference
    {
      get
      {
        return (DataSetReference) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ParameterValue>))]
    public IList<ParameterValue> ParameterValues
    {
      get
      {
        return (IList<ParameterValue>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression ValidationExpression
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ValidValues()
    {
    }

    internal ValidValues(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ParameterValues = new RdlCollection<ParameterValue>();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      ValidValues validValues = rdlObj as ValidValues;
      return validValues != null && SemanticCompare(DataSetReference, validValues.DataSetReference, visitedList) && SemanticCompare(ParameterValues, validValues.ParameterValues, visitedList);
    }

    internal class Definition : DefinitionStore<ValidValues, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataSetReference,
        ParameterValues,
        ValidationExpression,
      }
    }
  }
}
