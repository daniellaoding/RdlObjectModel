using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{

  public struct ItemBorderWidth
  {
    public static readonly ItemBorderWidth DefaultWidth = new ItemBorderWidth(new ReportExpression<ReportSize>(new ReportSize(1.0, SizeTypes.Point)), new ReportExpression<ReportSize>(), new ReportExpression<ReportSize>(), new ReportExpression<ReportSize>(), new ReportExpression<ReportSize>());


	  [NotifyParentProperty(true)]
    public ReportExpression<ReportSize> Bottom { get; }

	  public ReportExpression<ReportSize> Right { get; }


	  public ReportExpression<ReportSize> Top { get; }


	  public ReportExpression<ReportSize> Left { get; }


	  public ReportExpression<ReportSize> Default { get; }

	  public ItemBorderWidth(ReportExpression<ReportSize> defaultWidth, ReportExpression<ReportSize> left, ReportExpression<ReportSize> right, ReportExpression<ReportSize> top, ReportExpression<ReportSize> bottom)
    {
      Default = defaultWidth;
      if (!Default.IsExpression && Default.Value.IsEmpty)
        Default = new ReportSize(1.0, SizeTypes.Point);
      Left = left;
      Right = right;
      Top = top;
      Bottom = bottom;
    }

    public static bool IsSpecified(ReportExpression<ReportSize> value)
    {
      if (!value.IsExpression)
        return !value.Value.IsEmpty;
      return true;
    }

    public ItemBorderWidth Modify(BorderSide side, ReportExpression<ReportSize> size)
    {
      return new ItemBorderWidth((side & BorderSide.Default) == BorderSide.None ? Default : size, (side & BorderSide.Left) == BorderSide.None ? Left : size, (side & BorderSide.Right) == BorderSide.None ? Right : size, (side & BorderSide.Top) == BorderSide.None ? Top : size, (side & BorderSide.Bottom) == BorderSide.None ? Bottom : size);
    }

    public ReportSize GetDisplayWidth(BorderSide side)
    {
      switch (side)
      {
        case BorderSide.Left:
          if (Left.IsExpression)
            return new ReportSize(1.0, SizeTypes.Point);
          if (!Left.Value.IsEmpty)
            return Left.Value;
          break;
        case BorderSide.Right:
          if (Right.IsExpression)
            return new ReportSize(1.0, SizeTypes.Point);
          if (!Right.Value.IsEmpty)
            return Right.Value;
          break;
        case BorderSide.Top:
          if (Top.IsExpression)
            return new ReportSize(1.0, SizeTypes.Point);
          if (!Top.Value.IsEmpty)
            return Top.Value;
          break;
        case BorderSide.Bottom:
          if (Bottom.IsExpression)
            return new ReportSize(1.0, SizeTypes.Point);
          if (!Bottom.Value.IsEmpty)
            return Bottom.Value;
          break;
      }
      if (Default.IsExpression)
        return new ReportSize(1.0, SizeTypes.Point);
      if (Default.Value.IsEmpty)
        return new ReportSize(1.0, SizeTypes.Point);
      return Default.Value;
    }

    internal static ReportExpression<ReportSize> ParseOne(string value, IFormatProvider provider)
    {
      return Utils.ParseReportSizeExpression(value, provider, SizeTypes.Point);
    }
  }
}
