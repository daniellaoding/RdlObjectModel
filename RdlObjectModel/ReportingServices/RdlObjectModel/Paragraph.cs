using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RichText;
using Microsoft.ReportingServices.RPLObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Paragraph : ReportElement, IParagraphProps
  {
	  [XmlElement(typeof (RdlCollection<TextRun>))]
    public IList<TextRun> TextRuns
    {
      get
      {
        return PropertyStore.GetObject<IList<TextRun>>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> LeftIndent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> RightIndent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> HangingIndent
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [DefaultValue(ListStyle.None)]
    public ListStyle ListStyle
    {
      get
      {
        return (ListStyle) PropertyStore.GetInteger(5);
      }
      set
      {
        PropertyStore.SetInteger(5, (int) value);
      }
    }

    [DefaultValue(0)]
    public int ListLevel
    {
      get
      {
        return PropertyStore.GetInteger(6);
      }
      set
      {
        PropertyStore.SetInteger(6, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> SpaceBefore
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [ReportExpressionDefaultValueConstant(typeof (ReportSize), "DefaultZeroSize")]
    public ReportExpression<ReportSize> SpaceAfter
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportSize>>(8);
      }
      set
      {
        PropertyStore.SetObject(8, value);
      }
    }

    [XmlIgnore]
    public TextAlignments _TextAlign
    {
      get
      {
        if (Style.TextAlign.IsExpression)
          return TextAlignments.General;
        return Style.TextAlign.Value;
      }
    }

    [XmlIgnore]
    public ReportSize _HangingIndent
    {
      get
      {
        if (HangingIndent.IsExpression)
          return new ReportSize(0.0, SizeTypes.Point);
        return HangingIndent.Value;
      }
    }

    [XmlIgnore]
    public ReportSize _LeftIndent
    {
      get
      {
        if (LeftIndent.IsExpression)
          return new ReportSize(0.0, SizeTypes.Point);
        return LeftIndent.Value;
      }
    }

    [XmlIgnore]
    public ReportSize _RightIndent
    {
      get
      {
        if (RightIndent.IsExpression)
          return new ReportSize(0.0, SizeTypes.Point);
        return RightIndent.Value;
      }
    }

    [XmlIgnore]
    public int _ListLevel => ListLevel;

	  [XmlIgnore]
    public ListStyle _ListStyle => ListStyle;

	  [XmlIgnore]
    public ReportSize _SpaceAfter
    {
      get
      {
        if (SpaceAfter.IsExpression)
          return new ReportSize(0.0, SizeTypes.Point);
        return SpaceAfter.Value;
      }
    }

    [XmlIgnore]
    public ReportSize _SpaceBefore
    {
      get
      {
        if (SpaceBefore.IsExpression)
          return new ReportSize(0.0, SizeTypes.Point);
        return SpaceBefore.Value;
      }
    }

    [XmlIgnore]
    RPLFormat.TextAlignments IParagraphProps.Alignment
    {
      get
      {
        switch (_TextAlign)
        {
          case TextAlignments.Default:
          case TextAlignments.General:
            return RPLFormat.TextAlignments.General;
          case TextAlignments.Center:
            return RPLFormat.TextAlignments.Center;
          case TextAlignments.Right:
            return RPLFormat.TextAlignments.Right;
          default:
            return RPLFormat.TextAlignments.Left;
        }
      }
    }

    [XmlIgnore]
    float IParagraphProps.HangingIndent => (float) _HangingIndent.ToMillimeters();

	  [XmlIgnore]
    float IParagraphProps.LeftIndent => (float) _LeftIndent.ToMillimeters();

	  [XmlIgnore]
    RPLFormat.ListStyles IParagraphProps.ListStyle
    {
      get
      {
        switch (_ListStyle)
        {
          case ListStyle.Numbered:
            return RPLFormat.ListStyles.Numbered;
          case ListStyle.Bulleted:
            return RPLFormat.ListStyles.Bulleted;
          default:
            return RPLFormat.ListStyles.None;
        }
      }
    }

    [XmlIgnore]
    int IParagraphProps.ListLevel => _ListLevel;

	  [XmlIgnore]
    float IParagraphProps.RightIndent => (float) _RightIndent.ToMillimeters();

	  [XmlIgnore]
    float IParagraphProps.SpaceAfter => (float) _SpaceAfter.ToMillimeters();

	  [XmlIgnore]
    float IParagraphProps.SpaceBefore => (float) _SpaceBefore.ToMillimeters();

	  [XmlIgnore]
    public int ParagraphNumber { get; set; }

	  public Paragraph()
    {
    }

    internal Paragraph(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      TextRuns = new RdlCollection<TextRun>();
      TextRuns.Add(new TextRun());
    }

    internal class Definition : DefinitionStore<Paragraph, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        TextRuns,
        LeftIndent,
        RightIndent,
        HangingIndent,
        ListStyle,
        ListLevel,
        SpaceBefore,
        SpaceAfter,
      }
    }
  }
}
