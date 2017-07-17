using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Parameter : ReportObject, INamedObject
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

    public ReportExpression Value
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

    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Omit
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Parameter()
    {
    }

    internal Parameter(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override bool Equals(object obj)
    {
      Parameter parameter = obj as Parameter;
      if (parameter == null || !(parameter.Name == Name))
        return false;
      return parameter.Value.Equals(Value);
    }

    public override int GetHashCode()
    {
      if (Name == null)
        return 0;
      return Name.GetHashCode();
    }

    internal class Definition : DefinitionStore<Parameter, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        Value,
        Omit,
      }
    }
    
  }
}
