using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  internal static class Utils
  {
    private const float VERY_SMALL = 0.001f;

    public static Font WatermarkFont => SystemFonts.DialogFont;

	  public static Bitmap LoadImageResource(string resourceName)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
      {
        Bitmap bitmap = new Bitmap(manifestResourceStream);
        bitmap.MakeTransparent();
        return bitmap;
      }
    }

    public static StringFormat CreateWatermarkStringFormat()
    {
      return new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center,
        Trimming = StringTrimming.EllipsisCharacter
      };
    }

    public static Bitmap MakeTransparentBitmap(Bitmap b, Color transparentColor)
    {
      Bitmap bitmap;
      using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
        bitmap = new Bitmap(b.Width, b.Height, g);
      using (Graphics graphics = Graphics.FromImage(bitmap))
        graphics.DrawImage(b, 0, 0, b.Width, b.Height);
      bitmap.MakeTransparent(transparentColor);
      return bitmap;
    }

    public static string GetMimeType(string fileExtension)
    {
      switch (fileExtension.ToUpperInvariant())
      {
        case ".PNG":
          return "image/png";
        case ".GIF":
          return "image/gif";
        case ".JPG":
          return "image/jpeg";
        case ".JPE":
          return "image/jpeg";
        case ".JPEG":
          return "image/jpeg";
        case ".BMP":
          return "image/bmp";
        default:
          return string.Empty;
      }
    }

    public static void HSBtoRGB(float h, float s, float b, out float red, out float green, out float blue)
    {
      if (Math.Abs(s) < 1.0 / 1000.0)
      {
        red = green = blue = b;
      }
      else
      {
        if (h == 360.0)
          h = 0.0f;
        h /= 60f;
        int num1 = (int) Math.Floor(h);
        float num2 = h - num1;
        float num3 = b * (1f - s);
        float num4 = b * (float) (1.0 - s * (double) num2);
        float num5 = b * (float) (1.0 - s * (1.0 - num2));
        switch (num1)
        {
          case 1:
            red = num4;
            green = b;
            blue = num3;
            break;
          case 2:
            red = num3;
            green = b;
            blue = num5;
            break;
          case 3:
            red = num3;
            green = num4;
            blue = b;
            break;
          case 4:
            red = num5;
            green = num3;
            blue = b;
            break;
          case 5:
            red = b;
            green = num3;
            blue = num4;
            break;
          default:
            red = b;
            green = num5;
            blue = num3;
            break;
        }
      }
    }

    public static void RGBtoHSB(float red, float green, float blue, out float hue, out float saturation, out float brightness)
    {
      float num1 = Math.Max(red, Math.Max(green, blue));
      float num2 = Math.Min(red, Math.Min(green, blue));
      float num3 = num1 - num2;
      brightness = num1;
      saturation = (double) Math.Abs(num1) >= 1.0 / 1000.0 ? num3 / num1 : 0.0f;
      if (Math.Abs(saturation) < 1.0 / 1000.0)
      {
        hue = 0.0f;
      }
      else
      {
        hue = (double) red != (double) num1 ? ((double) green != (double) num1 ? (float) (4.0 + (red - (double) green) / num3) : (float) (2.0 + (blue - (double) red) / num3)) : (green - blue) / num3;
        hue *= 60f;
        if (hue >= 0.0)
          return;
        hue += 360f;
      }
    }

    public static Color AdjustBrightness(Color color, float multiplicationFactor)
    {
      return AdjustBrightnessCore(color, multiplicationFactor, true);
    }

    public static Color AdjustBrightnessAbsolute(Color color, float brightness)
    {
      return AdjustBrightnessCore(color, brightness, false);
    }

    private static Color AdjustBrightnessCore(Color color, float brightness, bool isFactor)
    {
      float hue;
      float saturation;
      float brightness1;
      RGBtoHSB(color.R / (float) byte.MaxValue, color.G / (float) byte.MaxValue, color.B / (float) byte.MaxValue, out hue, out saturation, out brightness1);
      float b = !isFactor ? brightness : brightness1 * brightness;
      if (b > 1.0)
        b = 1f;
      else if (b < 0.0)
        b = 0.0f;
      float red;
      float green;
      float blue;
      HSBtoRGB(hue, saturation, b, out red, out green, out blue);
      return Color.FromArgb((int) Math.Round(red * (double) byte.MaxValue), (int) Math.Round(green * (double) byte.MaxValue), (int) Math.Round(blue * (double) byte.MaxValue));
    }

    public static bool GetXOverlap(double left1, double right1, double left2, double right2, out double left, out double right)
    {
      left = Math.Max(left1, left2);
      right = Math.Min(right1, right2);
      return left < right;
    }

    public static bool GetYOverlap(double top1, double bottom1, double top2, double bottom2, out double top, out double bottom)
    {
      top = Math.Max(top1, top2);
      bottom = Math.Min(bottom1, bottom2);
      return top < bottom;
    }

    public static void TransformPoint(Matrix transform, ref PointF point)
    {
      float x = point.X;
      float y = point.Y;
      float[] elements = transform.Elements;
      float num1 = (float) (x * (double) elements[0] + y * (double) elements[2]) + elements[4];
      float num2 = (float) (x * (double) elements[1] + y * (double) elements[3]) + elements[5];
      point.X = num1;
      point.Y = num2;
    }

    public static void TransformSize(Matrix transform, ref SizeF size)
    {
      float width = size.Width;
      float height = size.Height;
      float[] elements = transform.Elements;
      float num1 = (float) (width * (double) elements[0] + height * (double) elements[2]);
      float num2 = (float) (width * (double) elements[1] + height * (double) elements[3]);
      size.Width = num1;
      size.Height = num2;
    }

    public static void Swap<T>(ref T a, ref T b)
    {
      T obj = a;
      a = b;
      b = obj;
    }


    public static string GetClsCompliantIdentifier(string candidate)
    {
      return Report.GetClsCompliantIdentifier(candidate);
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

    internal static string MakeDisplayName(string name, int maxDisplayNameLength)
    {
      if (maxDisplayNameLength <= 4 || name == null || name.Length <= maxDisplayNameLength)
        return name;
      return name.Substring(0, maxDisplayNameLength - 3) + '…' + name.Substring(name.Length - 2, 2);
    }
    
  }
}
