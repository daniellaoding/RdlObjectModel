using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RPLObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class TextRun : ReportElement
  {
    private int m_indexInParagraph;

    [DefaultValue("")]
    public string Label
    {
      get
      {
        return PropertyStore.GetObject<string>(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public ActionInfo ActionInfo
    {
      get
      {
        return PropertyStore.GetObject<ActionInfo>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue("")]
    public ReportExpression ToolTip
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (MarkupType), RdlObjectModel.MarkupType.None)]
    public ReportExpression<MarkupType> MarkupType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<MarkupType>>(7);
      }
      set
      {
        PropertyStore.SetObject(7, value);
      }
    }

    [XmlChildAttribute("Value", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    public string ValueLocID
    {
      get
      {
        return (string) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [XmlIgnore]
    public FontStyles _FontStyle
    {
      get
      {
        if (Style.FontStyle.IsExpression)
          return FontStyles.Normal;
        return Style.FontStyle.Value;
      }
    }

    [XmlIgnore]
    public string _FontFamily
    {
      get
      {
        if (Style.FontFamily.IsExpression || Style.FontFamily.IsEmpty)
          return "Arial";
        return Style.FontFamily.Value;
      }
    }

    [XmlIgnore]
    public ReportSize _FontSize
    {
      get
      {
        if (Style.FontSize.IsExpression || Style.FontSize.IsEmpty)
          return Constants.DefaultFontSize;
        return Style.FontSize.Value;
      }
    }

    [XmlIgnore]
    public FontWeights _FontWeight
    {
      get
      {
        if (Style.FontWeight.IsExpression)
          return FontWeights.Normal;
        return Style.FontWeight.Value;
      }
    }

    [XmlIgnore]
    public Color _Color
    {
      get
      {
        if (Style.Color.IsExpression)
          return Color.Black;
        return Style.Color.Value.Color;
      }
    }

    [XmlIgnore]
    public CultureInfo _CultureInfo
    {
      get
      {
        if (Style.Language.IsExpression)
          return CultureInfo.CurrentCulture;
        string name;
        if (Style.Language.IsEmpty)
        {
          IContainedObject parent = Parent;
          while (parent != null && !(parent is Report))
            parent = parent.Parent;
          Report report = parent as Report;
          if (report == null || report.Language.IsExpression || report.Language.IsEmpty)
            return CultureInfo.CurrentCulture;
          name = report.Language.Value;
        }
        else
          name = Style.Language.Value;
        try
        {
          return CultureInfo.CreateSpecificCulture(name);
        }
        catch
        {
          return CultureInfo.CurrentCulture;
        }
      }
    }

    public TextRun()
    {
    }

    internal TextRun(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      Value = new ReportExpression();
    }

    internal Action GetAction()
    {
      if (ActionInfo != null && ActionInfo.Actions.Count > 0)
        return ActionInfo.Actions[0];
      return null;
    }

    internal void SetAction(Action action)
    {
      if (action != null)
      {
        if (ActionInfo == null)
          ActionInfo = new ActionInfo();
        if (ActionInfo.Actions.Count > 0)
          ActionInfo.Actions[0] = action;
        else
          ActionInfo.Actions.Add(action);
      }
      else
        ActionInfo = null;
    }

    private RPLFormat.TextDecorations TranslateTextDecoration(TextDecorations decoration)
    {
      switch (decoration)
      {
        case TextDecorations.Underline:
          return RPLFormat.TextDecorations.Underline;
        case TextDecorations.Overline:
          return RPLFormat.TextDecorations.Overline;
        case TextDecorations.LineThrough:
          return RPLFormat.TextDecorations.LineThrough;
        default:
          return RPLFormat.TextDecorations.None;
      }
    }

    internal class Definition : DefinitionStore<TextRun, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Label,
        Value,
        ValueLocID,
        ActionInfo,
        ToolTip,
        ToolTipLocID,
        MarkupType,
      }
    }
  }
}
