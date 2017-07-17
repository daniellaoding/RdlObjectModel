using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public struct Padding
  {
	  public ReportExpression<ReportSize> Left { get; }


	  public ReportExpression<ReportSize> Right { get; }


	  public ReportExpression<ReportSize> Top { get; }


	  public ReportExpression<ReportSize> Bottom { get; }

	  [Browsable(false)]
    internal int _LeftPx
    {
      get
      {
        if (Left.IsExpression)
          return 0;
        return (int) Left.Value.ToPixels();
      }
    }

    [Browsable(false)]
    internal int _RightPx
    {
      get
      {
        if (Right.IsExpression)
          return 0;
        return (int) Right.Value.ToPixels();
      }
    }

    [Browsable(false)]
    internal int _TopPx
    {
      get
      {
        if (Top.IsExpression)
          return 0;
        return (int) Top.Value.ToPixels();
      }
    }

    [Browsable(false)]
    internal int _BottomPx
    {
      get
      {
        if (Bottom.IsExpression)
          return 0;
        return (int) Bottom.Value.ToPixels();
      }
    }

    [Browsable(false)]
    internal int _LeftPts
    {
      get
      {
        if (Left.IsExpression)
          return 0;
        return (int) Left.Value.ToPoints();
      }
    }

    [Browsable(false)]
    internal int _RightPts
    {
      get
      {
        if (Right.IsExpression)
          return 0;
        return (int) Right.Value.ToPoints();
      }
    }

    [Browsable(false)]
    internal int _TopPts
    {
      get
      {
        if (Top.IsExpression)
          return 0;
        return (int) Top.Value.ToPoints();
      }
    }

    [Browsable(false)]
    internal int _BottomPts
    {
      get
      {
        if (Bottom.IsExpression)
          return 0;
        return (int) Bottom.Value.ToPoints();
      }
    }

    public static Padding Default => new Padding(Defaults.DefaultPadding, Defaults.DefaultPadding, Defaults.DefaultPadding, Defaults.DefaultPadding);

	  public Padding(ReportExpression<ReportSize> left, ReportExpression<ReportSize> right, ReportExpression<ReportSize> top, ReportExpression<ReportSize> bottom)
    {
      Left = left;
      Top = top;
      Right = right;
      Bottom = bottom;
    }

    public void GetPaddingInPageUnits(out float left, out float right, out float top, out float bottom)
    {
      left = right = top = bottom = 0.0f;
      if (!Left.IsExpression)
        left = (float) Units.ReportSizeToPageUnits(Left.Value);
      if (!Right.IsExpression)
        right = (float) Units.ReportSizeToPageUnits(Right.Value);
      if (!Top.IsExpression)
        top = (float) Units.ReportSizeToPageUnits(Top.Value);
      if (Bottom.IsExpression)
        return;
      bottom = (float) Units.ReportSizeToPageUnits(Bottom.Value);
    }

    internal static ReportExpression<ReportSize> ParseOne(string value, IFormatProvider provider)
    {
      return Utils.ParseReportSizeExpression(value, provider, SizeTypes.Point);
    }
  }
}
