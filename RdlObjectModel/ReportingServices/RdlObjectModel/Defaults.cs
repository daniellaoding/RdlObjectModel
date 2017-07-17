using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal class Defaults
  {
    public static readonly Color OutlineColor = SystemColors.ControlDark;
    public const string TabString = "    ";
    public const int MaxListLevel = 9;
    public const int MinListLevel = 0;
    public const int MaxRenderingTicks = 2000000;
    public const int EdgeProximityInDeviceUnits = 3;
    public const string FontFamily = "Arial";

    public static ReportSize DefaultPadding => new ReportSize(0.0, SizeTypes.Point);

	  public static ReportSize TextboxPadding => new ReportSize(2.0, SizeTypes.Point);

	  public static Pen GetOutlinePen()
    {
      return new Pen(OutlineColor, 0.0f)
      {
        DashStyle = DashStyle.Custom,
        DashPattern = new float[2]{ 2f, 1f }
      };
    }

    public static string GetGroupContextMenuTitle(TablixMember member, bool columnGroup)
    {
      string str = columnGroup ? SRRdl.ColumnGroupTitle : SRRdl.RowGroupTitle;
      if (member != null && member.Group != null && !string.IsNullOrEmpty(member.Group.Name))
      {
        
        str = string.Format(SRRdl.Group, Utils.MakeDisplayName(member.Group.Name, 32));
      }
      return str;
    }

    public static ReportSize GetFontSize()
    {
      return new ReportSize(10.0, SizeTypes.Point);
    }

    public static ReportSize GetPageMargin()
    {
      if (!Units.DisplayMetricUnits)
        return new ReportSize(1.0, SizeTypes.Inch);
      return new ReportSize(2.0, SizeTypes.Cm);
    }

    public static double GetReportWidth()
    {
      return Units.InchesToPageUnits(6.5);
    }

    public static double GetHeaderHeight()
    {
      return Units.InchesToPageUnits(0.25);
    }

    public static double GetFooterHeight()
    {
      return Units.InchesToPageUnits(0.25);
    }

    public static double GetBodyHeight()
    {
      return Units.InchesToPageUnits(2.0);
    }

    public static ReportSize GetPageWidth()
    {
      if (!Units.DisplayMetricUnits)
        return new ReportSize(8.5, SizeTypes.Inch);
      return new ReportSize(21.0, SizeTypes.Cm);
    }

    public static ReportSize GetPageHeight()
    {
      if (!Units.DisplayMetricUnits)
        return new ReportSize(11.0, SizeTypes.Inch);
      return new ReportSize(29.7, SizeTypes.Cm);
    }

    public static ReportSize GetColumnSpacing()
    {
      if (!Units.DisplayMetricUnits)
        return new ReportSize(0.5, SizeTypes.Inch);
      return new ReportSize(0.13, SizeTypes.Cm);
    }

    internal static double GetDefaultReportItemWidth(Type type, Report report)
    {
      bool flag = IsMetricReport(report);
      if (type.Name == "TextboxReportItem")
      {
        if (!flag)
          return Units.InchesToPageUnits(1.0);
        return Units.CentimetersToPageUnits(2.5);
      }
      if (type.Name == "ImageReportItem")
      {
        if (!flag)
          return Units.InchesToPageUnits(0.5);
        return Units.CentimetersToPageUnits(1.5);
      }
      if (type.Name == "LineReportItem")
      {
        if (!flag)
          return Units.InchesToPageUnits(1.0);
        return Units.CentimetersToPageUnits(2.5);
      }
      if (type.Name == "RectangleReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(2.0);
        return Units.CentimetersToPageUnits(5.0);
      }
      if (type.Name == "SubreportReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(3.0);
        return Units.CentimetersToPageUnits(7.5);
      }
      if (type.Name == "ChartReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(3.0);
        return Units.CentimetersToPageUnits(7.5);
      }
      if (type.Name == "GaugeReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(3.0);
        return Units.CentimetersToPageUnits(7.5);
      }
      if (type.Name == "MapReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(6.0);
        return Units.CentimetersToPageUnits(15.0);
      }
      if (!flag)
        return Units.InchesToPageUnits(0.5);
      return Units.CentimetersToPageUnits(0.5);
    }

    private static bool IsMetricReport(Report report)
    {
      if (report == null)
        return Units.DisplayMetricUnits;
      if (report.ReportUnitType != SizeTypes.Cm)
        return report.ReportUnitType == SizeTypes.Mm;
      return true;
    }

    internal static double GetDefaultReportItemHeight(Type type, Report report)
    {
      bool flag = IsMetricReport(report);
      if (type.Name == "TextboxReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(0.25);
        return Units.CentimetersToPageUnits(0.6);
      }
      if (type.Name == "ImageReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(0.5);
        return Units.CentimetersToPageUnits(1.5);
      }
      if (type.Name == "LineReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(0.25);
        return Units.CentimetersToPageUnits(0.6);
      }
      if (type.Name == "RectangleReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(1.0);
        return Units.CentimetersToPageUnits(2.5);
      }
      if (type.Name == "SubreportReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(3.0);
        return Units.CentimetersToPageUnits(7.6);
      }
      if (type.Name == "ChartReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(2.0);
        return Units.CentimetersToPageUnits(5.0);
      }
      if (type.Name == "GaugeReportItem")

	  {
        if (!flag)
          return Units.InchesToPageUnits(2.0);
        return Units.CentimetersToPageUnits(5.0);
      }
      if (type.Name == "MapReportItem")
      {
        if (!flag)
          return Units.InchesToPageUnits(4.0);
        return Units.CentimetersToPageUnits(10.0);
      }
      if (!flag)
        return Units.InchesToPageUnits(0.5);
      return Units.CentimetersToPageUnits(0.5);
    }

    public static double GetTablixColumnWidth(Report report)
    {
      if (!IsMetricReport(report))
        return Units.InchesToPageUnits(1.0);
      return Units.CentimetersToPageUnits(2.5);
    }

    public static double GetTablixRowHeight(Report report)
    {
      if (!IsMetricReport(report))
        return Units.InchesToPageUnits(0.25);
      return Units.CentimetersToPageUnits(0.6);
    }

    public static double GetListColumnWidth(Report report)
    {
      if (!IsMetricReport(report))
        return Units.InchesToPageUnits(2.0);
      return Units.CentimetersToPageUnits(5.0);
    }

    public static double GetListRowHeight(Report report)
    {
      if (!IsMetricReport(report))
        return Units.InchesToPageUnits(1.0);
      return Units.CentimetersToPageUnits(2.5);
    }

    public static ItemBorderStyle GetTablixCellBorderStyle()
    {
      return new ItemBorderStyle(BorderStyles.Solid, new BorderStyles?(), new BorderStyles?(), new BorderStyles?(), new BorderStyles?());
    }

    public static ItemBorderColor GetTablixCellBorderColor()
    {
      return new ItemBorderColor(new ReportColor("LightGray"), new ReportColor?(), new ReportColor?(), new ReportColor?(), new ReportColor?());
    }

    public static double ItemMoveSmallStep()
    {
      return Units.InchesToPageUnits(1.0 / 80.0);
    }

    public static double ItemMoveLargeStep()
    {
      return Units.InchesToPageUnits(0.125);
    }

    public static double ItemResizeSmallStep()
    {
      return Units.InchesToPageUnits(1.0 / 80.0);
    }

    public static double ItemResizeLargeStep()
    {
      return Units.InchesToPageUnits(0.125);
    }

    public static double SpaceAroundPage()
    {
      return Units.InchesToPageUnits(0.22);
    }

    public static double ReportItemLocationOffset()
    {
      return Units.InchesToPageUnits(0.1);
    }

    public static ReportSize GetIndentAmount()
    {
      if (!Units.DisplayMetricUnits)
        return new ReportSize(0.5, SizeTypes.Inch);
      return new ReportSize(12.7, SizeTypes.Mm);
    }

    public static ReportSize GetOutdentAmount()
    {
      ReportSize indentAmount = GetIndentAmount();
      return new ReportSize(-1.0 * indentAmount.Value, indentAmount.Type);
    }
  }
}
