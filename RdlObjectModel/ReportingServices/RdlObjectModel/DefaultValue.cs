using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DefaultValue : ReportObject
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

    [XmlElement(typeof (RdlCollection<ReportExpression?>))]
    [XmlArrayItem("Value", typeof (ReportExpression), IsNullable = true)]
    public IList<ReportExpression?> Values
    {
      get
      {
        return (IList<ReportExpression?>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public DefaultValue()
    {
    }

    internal DefaultValue(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Values = new RdlCollection<ReportExpression?>();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      DefaultValue defaultValue = rdlObj as DefaultValue;
      if (defaultValue == null || !SemanticCompare(DataSetReference, defaultValue.DataSetReference, visitedList) || Values != null && defaultValue.Values == null || Values == null && defaultValue.Values != null)
        return false;
      if (Values != null)
      {
        if (Values.Count != defaultValue.Values.Count)
          return false;
        for (int index = 0; index < Values.Count; ++index)
        {
          if (Values[index].HasValue && !defaultValue.Values[index].HasValue || !Values[index].HasValue && defaultValue.Values[index].HasValue || Values[index].HasValue && !Values[index].Value.Equals(defaultValue.Values[index].Value))
            return false;
        }
      }
      return true;
    }

    internal class Definition : DefinitionStore<DefaultValue, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataSetReference,
        Values,
      }
    }
  }
}
