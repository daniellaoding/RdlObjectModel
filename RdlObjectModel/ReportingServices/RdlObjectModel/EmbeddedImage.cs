using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class EmbeddedImage : ReportObject, INamedObject
  {
    private System.Drawing.Image m_cachedImage;
    private bool m_createImageFailed;

    [XmlAttribute(typeof (string))]
    public string Name
    {
      get
      {
        return (string) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public string MIMEType
    {
      get
      {
        return (string) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ImageData ImageData
    {
      get
      {
        return (ImageData) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [XmlIgnore]
    public System.Drawing.Image Image
    {
      get
      {
        if (m_cachedImage == null)
          CreateImageFromData();
        return m_cachedImage;
      }
    }

    [XmlIgnore]
    public double Width
    {
      get
      {
        if (m_cachedImage == null && !CreateImageFromData())
          return 0.0;
        return Units.InchesToPageUnits(m_cachedImage.Width / (double) m_cachedImage.HorizontalResolution);
      }
    }

    [XmlIgnore]
    public double Height
    {
      get
      {
        if (m_cachedImage == null && !CreateImageFromData())
          return 0.0;
        return Units.InchesToPageUnits(m_cachedImage.Height / (double) m_cachedImage.VerticalResolution);
      }
    }

    [XmlIgnore]
    public SizeF Size => new SizeF((float) Width, (float) Height);

	  public EmbeddedImage()
    {
    }

    internal EmbeddedImage(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (CheckVisitedAndUpdate(this, visitedList))
        return true;
      EmbeddedImage embeddedImage = rdlObj as EmbeddedImage;
      if (embeddedImage == null || ImageData.Bytes != null && embeddedImage.ImageData.Bytes == null || ImageData.Bytes == null && embeddedImage.ImageData.Bytes != null)
        return false;
      if (ImageData.Bytes != null)
      {
        if (ImageData.Bytes.Length != embeddedImage.ImageData.Bytes.Length)
          return false;
        for (int index = 0; index < ImageData.Bytes.Length; ++index)
        {
          if (ImageData.Bytes[index] != embeddedImage.ImageData.Bytes[index])
            return false;
        }
      }
      return true;
    }

    protected internal override void OnPropertyChanged(int propertyIndex, object oldValue, object newValue)
    {
      base.OnPropertyChanged(propertyIndex, oldValue, newValue);
      if (propertyIndex != 2)
        return;
      m_createImageFailed = false;
    }

    private bool CreateImageFromData()
    {
      if (m_createImageFailed)
        return false;
      try
      {
        using (MemoryStream memoryStream = new MemoryStream(ImageData.Bytes))
        {
          using (System.Drawing.Image original = System.Drawing.Image.FromStream(memoryStream))
            m_cachedImage = new Bitmap(original);
        }
        return true;
      }
      catch (ArgumentException ex)
      {
        m_createImageFailed = true;
        return false;
      }
    }

    public static bool IsValidMimeType(string mimeType)
    {
      if (string.IsNullOrEmpty(mimeType))
        return false;
      switch (mimeType.ToUpperInvariant())
      {
        case "IMAGE/PNG":
        case "IMAGE/GIF":
        case "IMAGE/JPEG":
        case "IMAGE/BMP":
          return true;
        default:
          return false;
      }
    }

    internal class Definition : DefinitionStore<EmbeddedImage, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Name,
        MIMEType,
        ImageData,
      }
    }
  }
}
