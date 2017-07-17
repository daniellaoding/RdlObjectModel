using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class UserSort : ReportObject, IShouldSerialize
  {
    public ReportExpression SortExpression
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue("")]
    public string SortExpressionScope
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [DefaultValue("")]
    public string SortTarget
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

    public UserSort()
    {
    }

    internal UserSort(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return !string.IsNullOrEmpty(SortExpression.ToString());
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<UserSort, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        SortExpression,
        SortExpressionScope,
        SortTarget,
      }
    }
    
  }
}
