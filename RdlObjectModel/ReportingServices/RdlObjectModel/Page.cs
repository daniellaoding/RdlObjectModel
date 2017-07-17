using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Page : ReportObject
  {
    public PageSection PageHeader
    {
      get
      {
        return (PageSection) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public PageSection PageFooter
    {
      get
      {
        return (PageSection) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [DefaultValueConstant("DefaultPageHeight")]
    public ReportSize PageHeight
    {
      get
      {
        return PropertyStore.GetSize(2);
      }
      set
      {
        PropertyStore.SetSize(2, value);
      }
    }

    [DefaultValueConstant("DefaultPageWidth")]
    public ReportSize PageWidth
    {
      get
      {
        return PropertyStore.GetSize(3);
      }
      set
      {
        PropertyStore.SetSize(3, value);
      }
    }

    public ReportSize InteractiveHeight
    {
      get
      {
        return PropertyStore.GetSize(4);
      }
      set
      {
        PropertyStore.SetSize(4, value);
      }
    }

    public ReportSize InteractiveWidth
    {
      get
      {
        return PropertyStore.GetSize(5);
      }
      set
      {
        PropertyStore.SetSize(5, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize LeftMargin
    {
      get
      {
        return PropertyStore.GetSize(6);
      }
      set
      {
        PropertyStore.SetSize(6, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize RightMargin
    {
      get
      {
        return PropertyStore.GetSize(7);
      }
      set
      {
        PropertyStore.SetSize(7, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize TopMargin
    {
      get
      {
        return PropertyStore.GetSize(8);
      }
      set
      {
        PropertyStore.SetSize(8, value);
      }
    }

    [DefaultValueConstant("DefaultZeroSize")]
    public ReportSize BottomMargin
    {
      get
      {
        return PropertyStore.GetSize(9);
      }
      set
      {
        PropertyStore.SetSize(9, value);
      }
    }

    [ValidValues(1, 100)]
    [DefaultValue(1)]
    public int Columns
    {
      get
      {
        return PropertyStore.GetInteger(10);
      }
      set
      {
        ((ComparablePropertyDefinition<int>) DefinitionStore<Page, Definition.Properties>.GetProperty(10)).Validate(this, value);
        PropertyStore.SetInteger(10, value);
      }
    }

    [DefaultValueConstant("DefaultColumnSpacing")]
    public ReportSize ColumnSpacing
    {
      get
      {
        return PropertyStore.GetSize(11);
      }
      set
      {
        PropertyStore.SetSize(11, value);
      }
    }

    public Style Style
    {
      get
      {
        return (Style) PropertyStore.GetObject(12);
      }
      set
      {
        PropertyStore.SetObject(12, value);
      }
    }

    public Page()
    {
    }

    internal Page(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Columns = 1;
      PageHeight = Constants.DefaultPageHeight;
      PageWidth = Constants.DefaultPageWidth;
      ColumnSpacing = Constants.DefaultColumnSpacing;
    }

    protected override void InitializeForDesigner()
    {
      base.InitializeForDesigner();
      Style = new Style();
    }

    internal class Definition : DefinitionStore<Page, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        PageHeader,
        PageFooter,
        PageHeight,
        PageWidth,
        InteractiveHeight,
        InteractiveWidth,
        LeftMargin,
        RightMargin,
        TopMargin,
        BottomMargin,
        Columns,
        ColumnSpacing,
        Style,
      }
    }
  }
}
