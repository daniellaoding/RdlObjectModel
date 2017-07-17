using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TablixMember : HierarchyMember, IHierarchyMember, IDataScopeService
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

    public TablixHeader TablixHeader
    {
      get
      {
        return (TablixHeader) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlElement(typeof (RdlCollection<TablixMember>))]
    public IList<TablixMember> TablixMembers
    {
      get
      {
        return (IList<TablixMember>) PropertyStore.GetObject(3);
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
        return (IList<CustomProperty>) PropertyStore.GetObject(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [DefaultValue(false)]
    public bool FixedData
    {
      get
      {
        return PropertyStore.GetBoolean(5);
      }
      set
      {
        PropertyStore.SetBoolean(5, value);
      }
    }

    public Visibility Visibility
    {
      get
      {
        return (Visibility) PropertyStore.GetObject(6);
      }
      set
      {
        PropertyStore.SetObject(6, value);
      }
    }

    [DefaultValue(false)]
    public bool HideIfNoRows
    {
      get
      {
        return PropertyStore.GetBoolean(7);
      }
      set
      {
        PropertyStore.SetBoolean(7, value);
      }
    }

    [DefaultValue(KeepWithGroupTypes.None)]
    public KeepWithGroupTypes KeepWithGroup
    {
      get
      {
        return (KeepWithGroupTypes) PropertyStore.GetInteger(8);
      }
      set
      {
        PropertyStore.SetInteger(8, (int) value);
      }
    }

    [DefaultValue(false)]
    public bool RepeatOnNewPage
    {
      get
      {
        return PropertyStore.GetBoolean(9);
      }
      set
      {
        PropertyStore.SetBoolean(9, value);
      }
    }

    [DefaultValue("")]
    public string DataElementName
    {
      get
      {
        return (string) PropertyStore.GetObject(10);
      }
      set
      {
        PropertyStore.SetObject(10, value);
      }
    }

    [DefaultValue(DataElementOutputTypes.Auto)]
    [ValidEnumValues("TablixMemberDataElementOutputTypes")]
    public DataElementOutputTypes DataElementOutput
    {
      get
      {
        return (DataElementOutputTypes) PropertyStore.GetInteger(11);
      }
      set
      {
        ((EnumProperty) DefinitionStore<TablixMember, Definition.Properties>.GetProperty(11)).Validate(this, (int) value);
        PropertyStore.SetInteger(11, (int) value);
      }
    }

    [DefaultValue(false)]
    public bool KeepTogether
    {
      get
      {
        return PropertyStore.GetBoolean(12);
      }
      set
      {
        PropertyStore.SetBoolean(12, value);
      }
    }

    IEnumerable<IHierarchyMember> IHierarchyMember.Members
    {
      get
      {
        foreach (IHierarchyMember tablixMember in TablixMembers)
          yield return tablixMember;
      }
    }

    [Browsable(false)]
    [XmlIgnore]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Tablix ParentTablix => GetAncestor<Tablix>();

	  [EditorBrowsable(EditorBrowsableState.Never)]
    [XmlIgnore]
    [Browsable(false)]
    public TablixHierarchy ParentTablixHierarchy => GetAncestor<TablixHierarchy>();


	  [XmlIgnore]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ReportExpression<bool> Hidden
    {
      get
      {
        return Visibility.Hidden;
      }
      set
      {
        Visibility.Hidden = value;
      }
    }

    [XmlIgnore]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string ToggleItem
    {
      get
      {
        return Visibility.ToggleItem;
      }
      set
      {
        Visibility.ToggleItem = value;
      }
    }

    public TablixMember()
    {
    }

    internal TablixMember(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      SortExpressions = new RdlCollection<SortExpression>();
      TablixMembers = new RdlCollection<TablixMember>();
      CustomProperties = new RdlCollection<CustomProperty>();
      DataElementOutput = DataElementOutputTypes.Auto;
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      Visibility = new Visibility();
    }

    public void UpdateReferences(NameChanges nameChanges)
    {
      ToggleItem = nameChanges.GetNewName(NameChanges.EntryType.ReportItem, ToggleItem);
    }

    internal IEnumerable<TablixMember> GetDynamicChildMembers()
    {
      return TablixHierarchy.GetDynamicChildMembers(TablixMembers);
    }

    internal void ConvertToStaticMember()
    {
      Group = null;
      SortExpressions.Clear();
      ToggleItem = string.Empty;
      Hidden = false;
    }

    internal class Definition : DefinitionStore<TablixMember, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Group,
        SortExpressions,
        TablixHeader,
        TablixMembers,
        CustomProperties,
        FixedData,
        Visibility,
        HideIfNoRows,
        KeepWithGroup,
        RepeatOnNewPage,
        DataElementName,
        DataElementOutput,
        KeepTogether,
      }
    }
  }
}
