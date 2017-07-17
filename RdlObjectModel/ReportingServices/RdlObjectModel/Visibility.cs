using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Visibility : ReportObject, IShouldSerialize
  {
    [ReportExpressionDefaultValue(typeof (bool), false)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<bool>>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [DefaultValue("")]
    public string ToggleItem
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

    public Visibility()
    {
    }

    internal Visibility(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      ToggleItem = nameChanges.GetNewName(NameChanges.EntryType.ReportItem, ToggleItem);
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      if (!Hidden.IsExpression && !Hidden.Value)
        return !string.IsNullOrEmpty(ToggleItem);
      return true;
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<Visibility, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Hidden,
        ToggleItem,
      }
    }

  }
}
