using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartMember : HierarchyMember, IHierarchyMember, IDataScopeService
  {
    public override Group Group
    {
      get
      {
        return (Group) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<SortExpression>))]
    public IList<SortExpression> SortExpressions
    {
      get
      {
        return (IList<SortExpression>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartMember>))]
    public IList<ChartMember> ChartMembers
    {
      get
      {
        return (IList<ChartMember>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ReportExpression Label
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlElement(typeof (RdlCollection<CustomProperty>))]
    public IList<CustomProperty> CustomProperties
    {
      get
      {
        return (IList<CustomProperty>) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
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

    [ValidEnumValues("ChartMemberDataElementOutputTypes")]
    [DefaultValue(DataElementOutputTypes.Auto)]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(7);
      }
      set
      {
        ((EnumProperty) DefinitionStore<ChartMember, Definition.Properties>.GetProperty(7)).Validate(this, (int) value);
        PropertyStore.SetInteger(7, (int) value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchyMember.Members
    {
      get
      {
        foreach (IHierarchyMember chartMember in ChartMembers)
          yield return chartMember;
      }
    }

    [XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string LabelLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public ChartMember()
    {
    }

    internal ChartMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      SortExpressions = new RdlCollection<SortExpression>();
      ChartMembers = new RdlCollection<ChartMember>();
      CustomProperties = new RdlCollection<CustomProperty>();
    }

    internal class Definition : DefinitionStore<ChartMember, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Group,
        SortExpressions,
        ChartMembers,
        Label,
        LabelLocID,
        CustomProperties,
        DataElementName,
        DataElementOutput,
        PropertyCount,
      }
    }
  }
}
