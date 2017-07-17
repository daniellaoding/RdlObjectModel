using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartSeriesHierarchy : ReportObject, IHierarchy
  {
    [XmlElement(typeof (RdlCollection<ChartMember>))]
    public IList<ChartMember> ChartMembers
    {
      get
      {
        return (IList<ChartMember>) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchy.Members
    {
      get
      {
        foreach (IHierarchyMember chartMember in ChartMembers)
          yield return chartMember;
      }
    }

    public ChartSeriesHierarchy()
    {
    }

    internal ChartSeriesHierarchy(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartMembers = new RdlCollection<ChartMember>();
    }

    internal class Definition : DefinitionStore<ChartSeriesHierarchy, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ChartMembers,
        PropertyCount,
      }
    }
  }
}
