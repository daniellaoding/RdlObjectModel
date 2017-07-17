using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ActionInfo : ReportObject
  {
    [XmlElement(typeof (RdlCollection<Action>))]
    public IList<Action> Actions
    {
      get
      {
        return (IList<Action>) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public ActionInfo()
    {
    }

    internal ActionInfo(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Actions = new RdlCollection<Action>();
    }

    internal class Definition : DefinitionStore<ActionInfo, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Actions,
        LayoutDirection,
        Style,
      }
    }
  }
}
