using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ReportParametersLayout : ReportObject, IShouldSerialize
  {
    public GridLayoutDefinition GridLayoutDefinition
    {
      get
      {
        return PropertyStore.GetObject<GridLayoutDefinition>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ReportParametersLayout()
    {
      GridLayoutDefinition = new GridLayoutDefinition();
    }

    internal ReportParametersLayout(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<GridLayoutDefinition, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        GridLayoutDefinition,
      }
    }
  }
}
