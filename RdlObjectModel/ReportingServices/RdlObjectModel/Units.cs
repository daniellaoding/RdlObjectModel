using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal static class Units
  {
    public static readonly float dpiX;
    public static readonly float dpiY;

    public static GraphicsUnit PageUnit => GraphicsUnit.Point;

	  public static bool DisplayMetricUnits => RegionInfo.CurrentRegion.IsMetric;

	  public static SizeTypes DisplayUnit => !DisplayMetricUnits ? SizeTypes.Inch : SizeTypes.Cm;

	  static Units()
    {
      using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
      {
        dpiX = graphics.DpiX;
        dpiY = graphics.DpiY;
      }
    }

    public static double PageUnitsToPoints(double n)
    {
      return n;
    }

    public static double PointsToPageUnits(double n)
    {
      return n;
    }

    public static double InchesToPageUnits(double n)
    {
      return n * 72.0;
    }

    public static double CentimetersToPageUnits(double n)
    {
      return n * (3600.0 / sbyte.MaxValue);
    }

    public static float PageUnitsToPixelsXFloat(float n)
    {
      return n / 72f * dpiX;
    }

    public static int PageUnitsToPixelsX(double n)
    {
      return (int) (0.5 + n / 72.0 * dpiX);
    }

    public static float PageUnitsToPixelsYFloat(float n)
    {
      return n / 72f * dpiY;
    }

    public static int PageUnitsToPixelsY(double n)
    {
      return (int) (0.5 + n / 72.0 * dpiY);
    }

    public static double PixelsToPageUnitsX(double n)
    {
      return n / dpiX * 72.0;
    }

    public static double PixelsToPageUnitsY(double n)
    {
      return n / dpiY * 72.0;
    }

    public static void ConvertUnits(Graphics g, GraphicsUnit dstUnits, GraphicsUnit srcUnits, ref System.Drawing.Rectangle rectangle)
    {
      Point[] pts = new Point[2]
      {
        new Point(rectangle.Left, rectangle.Top),
        new Point(rectangle.Right, rectangle.Bottom)
      };
      ConvertUnits(g, dstUnits, srcUnits, ref pts);
      rectangle = System.Drawing.Rectangle.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
    }

    public static void ConvertUnits(Graphics g, GraphicsUnit dstUnits, GraphicsUnit srcUnits, ref RectangleF rectangle)
    {
      PointF[] pts = new PointF[2]
      {
        new PointF(rectangle.Left, rectangle.Top),
        new PointF(rectangle.Right, rectangle.Bottom)
      };
      ConvertUnits(g, dstUnits, srcUnits, ref pts);
      rectangle = RectangleF.FromLTRB(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
    }

    public static void ConvertUnits(Graphics g, GraphicsUnit dstUnits, GraphicsUnit srcUnits, ref Point[] pts)
    {
      GraphicsUnit pageUnit = g.PageUnit;
      g.PageUnit = srcUnits;
      g.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, pts);
      g.PageUnit = dstUnits;
      g.TransformPoints(CoordinateSpace.Page, CoordinateSpace.Device, pts);
      g.PageUnit = pageUnit;
    }

    public static void ConvertUnits(Graphics g, GraphicsUnit dstUnits, GraphicsUnit srcUnits, ref PointF[] pts)
    {
      GraphicsUnit pageUnit = g.PageUnit;
      g.PageUnit = srcUnits;
      g.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, pts);
      g.PageUnit = dstUnits;
      g.TransformPoints(CoordinateSpace.Page, CoordinateSpace.Device, pts);
      g.PageUnit = pageUnit;
    }

    public static System.Drawing.Rectangle PageUnitsToPixels(RectangleF r)
    {
      float pixelsXfloat1 = PageUnitsToPixelsXFloat(r.Left);
      float pixelsYfloat1 = PageUnitsToPixelsYFloat(r.Top);
      float pixelsXfloat2 = PageUnitsToPixelsXFloat(r.Right);
      float pixelsYfloat2 = PageUnitsToPixelsYFloat(r.Bottom);
      return new System.Drawing.Rectangle((int) Math.Round(pixelsXfloat1), (int) Math.Round(pixelsYfloat1), (int) Math.Round(pixelsXfloat2 - (double) pixelsXfloat1), (int) Math.Round(pixelsYfloat2 - (double) pixelsYfloat1));
    }

    public static Point PageUnitsToPixels(PointF p)
    {
      return new Point(PageUnitsToPixelsX(p.X), PageUnitsToPixelsY(p.Y));
    }

    public static ReportSize PointsToReportSize(double points, SizeTypes sizeType)
    {
      switch (sizeType)
      {
        case SizeTypes.Inch:
          return new ReportSize(points / 72.0, SizeTypes.Inch);
        case SizeTypes.Cm:
          return new ReportSize(points / 72.0 * 2.54, SizeTypes.Cm);
        case SizeTypes.Mm:
          return new ReportSize(points / 72.0 * 25.4, SizeTypes.Mm);
        case SizeTypes.Point:
          return new ReportSize(points, SizeTypes.Point);
        case SizeTypes.Pica:
          return new ReportSize(points / 12.0, SizeTypes.Pica);
        default:
          return new ReportSize();
      }
    }

    public static ReportSize PageUnitsToReportSize(double pageUnits, SizeTypes sizeType)
    {
      return PointsToReportSize(PageUnitsToPoints(pageUnits), sizeType);
    }

    public static double ReportSizeToPageUnits(ReportSize reportSize)
    {
      return InchesToPageUnits(reportSize.ToInches());
    }

    internal static int CompareDistances(double a, double b)
    {
      double pageUnits = PointsToPageUnits(0.05);
      if (Math.Abs(a - b) < pageUnits)
        return 0;
      return a >= b ? 1 : -1;
    }

	  public static ReportExpression<ReportSize> ParseReportSizeExpression(string value, IFormatProvider provider, SizeTypes defaultSizeType)
	  {
		  if (value == null)
			  return new ReportExpression<ReportSize>();
		  value = value.Trim();
		  if (value.Length == 0)
			  return new ReportExpression<ReportSize>();
		  ReportExpression<ReportSize> reportExpression = new ReportExpression<ReportSize>(value, provider);
		  if (!reportExpression.IsExpression)
			  reportExpression = new ReportSize(value, provider, defaultSizeType);
		  return reportExpression;
	  }

	}
}
