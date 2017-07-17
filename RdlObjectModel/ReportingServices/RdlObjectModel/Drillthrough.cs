using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Drillthrough : ReportObject
  {
    public ReportExpression ReportName
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

    [XmlElement(typeof (RdlCollection<Parameter>))]
    public IList<Parameter> Parameters
    {
      get
      {
        return (IList<Parameter>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public Drillthrough()
    {
    }

    internal Drillthrough(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Parameters = new RdlCollection<Parameter>();
    }

    public override bool Equals(object obj)
    {
      Drillthrough drillthrough = obj as Drillthrough;
      if (drillthrough == null || ReportName != drillthrough.ReportName || Parameters != null && drillthrough.Parameters == null || Parameters == null && drillthrough.Parameters != null || Parameters != null && drillthrough.Parameters != null && Parameters.Count != drillthrough.Parameters.Count)
        return false;
      if (Parameters != null && drillthrough.Parameters != null)
      {
        for (int index = 0; index < Parameters.Count; ++index)
        {
          if (!Parameters[index].Equals(drillthrough.Parameters[index]))
            return false;
        }
      }
      return true;
    }

    public override int GetHashCode()
    {
      if (!(ReportName != null))
        return 0;
      return ReportName.GetHashCode();
    }

    internal class Definition : DefinitionStore<Drillthrough, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ReportName,
        Parameters,
      }
    }
  }
}
