using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Group : ReportObject, IGlobalNamedObject, INamedObject, IDataScope, IContainedObject
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

    [ReportExpressionDefaultValue]
    public ReportExpression DocumentMapLabel
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

    [XmlElement(typeof (RdlCollection<ReportExpression>))]
    [XmlArrayItem("GroupExpression", typeof (ReportExpression))]
    public IList<ReportExpression> GroupExpressions
    {
      get
      {
        return (IList<ReportExpression>) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlArrayItem("GroupExpression", typeof (ReportExpression))]
    [XmlElement(typeof (RdlCollection<ReportExpression>))]
    public IList<ReportExpression> ReGroupExpressions
    {
      get
      {
        return (IList<ReportExpression>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    public PageBreak PageBreak
    {
      get
      {
        return (PageBreak) PropertyStore.GetObject(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression PageName
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(11);
      }
      set
      {
        PropertyStore.SetObject(11, value);
      }
    }

    [XmlElement(typeof (RdlCollection<Filter>))]
    public IList<Filter> Filters
    {
      get
      {
        return (IList<Filter>) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression Parent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [DefaultValue(DataElementOutputTypes.Output)]
    [ValidEnumValues("GroupDataElementOutputTypes")]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(9);
      }
      set
      {
        ((EnumProperty) DefinitionStore<Group, Definition.Properties>.GetProperty(9)).Validate(this, (int) value);
        PropertyStore.SetInteger(9, (int) value);
      }
    }

    [XmlElement(typeof (RdlCollection<Variable>))]
    public IList<Variable> Variables
    {
      get
      {
        return (IList<Variable>) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [DefaultValue("")]
    public string DomainScope
    {
      get
      {
        return (string) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    [XmlChildAttribute("DocumentMapLabel", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string DocumentMapLabelLocID
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

    [Browsable(false)]
    [XmlIgnore]
    public TablixMember ParentTablixMember => (ReportObject) base.Parent as TablixMember;

	  Group IDataScope.Group => this;

	  public Group()
    {
    }

    internal Group(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      GroupExpressions = new RdlCollection<ReportExpression>();
      ReGroupExpressions = new RdlCollection<ReportExpression>();
      Filters = new RdlCollection<Filter>();
      DataElementOutput = DataElementOutputTypes.Output;
      Variables = new RdlCollection<Variable>();
    }

    internal class Definition : DefinitionStore<Group, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        DocumentMapLabel,
        DocumentMapLabelLocID,
        GroupExpressions,
        ReGroupExpressions,
        PageBreak,
        Filters,
        Parent,
        DataElementName,
        DataElementOutput,
        Variables,
        PageName,
        DomainScope,
        PropertyCount,
      }
    }
    
  }
}
