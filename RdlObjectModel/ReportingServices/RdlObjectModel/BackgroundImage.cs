using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class BackgroundImage : ReportObject, IShouldSerialize
  {
    public SourceType Source
    {
      get
      {
        return (SourceType) PropertyStore.GetInteger(0);
      }
      set
      {
        PropertyStore.SetInteger(0, (int) value);
      }
    }

    public ReportExpression Value
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

    [ReportExpressionDefaultValue]
    public ReportExpression MIMEType
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

    [ReportExpressionDefaultValue(typeof (BackgroundRepeatTypes), BackgroundRepeatTypes.Default)]
    public ReportExpression<BackgroundRepeatTypes> BackgroundRepeat
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BackgroundRepeatTypes>>(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (ReportColor))]
    public ReportExpression<ReportColor> TransparentColor
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
      }
      set
      {
        PropertyStore.SetObject(4, value);
      }
    }

    [ReportExpressionDefaultValue(typeof (BackgroundPositions), BackgroundPositions.Default)]
    public ReportExpression<BackgroundPositions> Position
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression<BackgroundPositions>>(5);
      }
      set
      {
        PropertyStore.SetObject(5, value);
      }
    }

    public BackgroundImage()
    {
    }

    internal BackgroundImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      if (Source != SourceType.Embedded)
        return;
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      if (!Value.IsExpression)
      {
        EmbeddedImage embeddedImageByName = ancestor.GetEmbeddedImageByName(Value.Expression);
        if (embeddedImageByName == null || dependencies.Contains(embeddedImageByName))
          return;
        dependencies.Add(embeddedImageByName);
      }
      else
      {
        ExpressionParser.Expression expression = ExpressionFactory.CreateExpression(Value.Expression, true);
        if (expression == null || expression.ObjectDependencyList == null || expression.ObjectDependencyList.Count <= 0)
          return;
        foreach (IInternalExpression objectDependency in expression.ObjectDependencyList)
        {
          if (objectDependency is ConstantString)
          {
            EmbeddedImage embeddedImageByName = ancestor.GetEmbeddedImageByName((string) objectDependency.Evaluate());
            if (embeddedImageByName != null && !dependencies.Contains(embeddedImageByName))
              dependencies.Add(embeddedImageByName);
          }
        }
      }
    }

    bool IShouldSerialize.ShouldSerializeThis()
    {
      return !string.IsNullOrEmpty(Value.ToString());
    }

    SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
    {
      return SerializationMethod.Auto;
    }

    internal class Definition : DefinitionStore<BackgroundImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Source,
        Value,
        MIMEType,
        BackgroundRepeat,
        TransparentColor,
        Position,
        PropertyCount,
      }
    }
  }
}
