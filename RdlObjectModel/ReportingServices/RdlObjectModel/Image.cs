using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Image : ReportItem
  {
    public SourceType Source
    {
      get
      {
        return (SourceType) PropertyStore.GetInteger(18);
      }
      set
      {
        PropertyStore.SetInteger(18, (int) value);
      }
    }

    public ReportExpression Value
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(19);
      }
      set
      {
        PropertyStore.SetObject(19, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression MIMEType
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(20);
      }
      set
      {
        PropertyStore.SetObject(20, value);
      }
    }

    [DefaultValue(Sizings.AutoSize)]
    public Sizings Sizing
    {
      get
      {
        return (Sizings) PropertyStore.GetInteger(21);
      }
      set
      {
        PropertyStore.SetInteger(21, (int) value);
      }
    }

    public Image()
    {
    }

    internal Image(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      Value = nameChanges.GetNewName(NameChanges.EntryType.EmbeddedImage, Value.Expression);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      GetEmbeddedImgDependencies(GetAncestor<Report>(), dependencies, Source, Value);
    }

    internal static void GetEmbeddedImgDependencies(Report report, ICollection<ReportObject> dependencies, SourceType imageSource, ReportExpression imageValue)
    {
      if (report == null || dependencies == null || (string.IsNullOrEmpty(imageValue.Expression) || imageSource != SourceType.Embedded))
        return;
      EmbeddedImage embeddedImage;
      if (!imageValue.IsExpression)
      {
        embeddedImage = report.GetEmbeddedImageByName(imageValue.Expression);
        if (embeddedImage == null || dependencies.Contains(embeddedImage))
          return;
        dependencies.Add(embeddedImage);
      }
      else
      {
        ExpressionParser.Expression expression = ExpressionFactory.CreateExpression(imageValue.Expression, true);
        if (expression == null)
          return;
	      ExpressionParser.Expression.IterateExpressionTree(expressionNode =>
	      {
		      if (!(expressionNode is ConstantString))
			      return;
		      embeddedImage = report.GetEmbeddedImageByName(expressionNode.Evaluate() as string);
		      if (embeddedImage == null || dependencies.Contains(embeddedImage))
			      return;
		      dependencies.Add(embeddedImage);
	      }, expression.InternalExpression);
      }
    }

    internal class Definition : DefinitionStore<Image, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Style,
        Name,
        ActionInfo,
        Top,
        Left,
        Height,
        Width,
        ZIndex,
        Visibility,
        ToolTip,
        ToolTipLocID,
        DocumentMapLabel,
        DocumentMapLabelLocID,
        Bookmark,
        RepeatWith,
        CustomProperties,
        DataElementName,
        DataElementOutput,
        Source,
        Value,
        MIMEType,
        Sizing,
      }
    }
  }
}
