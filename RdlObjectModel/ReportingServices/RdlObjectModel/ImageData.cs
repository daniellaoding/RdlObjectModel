using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public struct ImageData : IXmlSerializable
  {
	  public byte[] Bytes { get; set; }

	  public ImageData(byte[] bytes)
    {
      Bytes = bytes;
    }

    public static implicit operator ImageData(byte[] bytes)
    {
      return new ImageData(bytes);
    }

    public static explicit operator byte[](ImageData value)
    {
      return value.Bytes;
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      Bytes = Convert.FromBase64String(reader.ReadString());
      reader.Skip();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      if (Bytes == null)
        return;
      string base64String = Convert.ToBase64String(Bytes);
      int startIndex = 0;
      while (startIndex < base64String.Length)
      {
        if (startIndex > 0)
          writer.WriteString("\n");
        int length = Math.Min(1000, base64String.Length - startIndex);
        char[] charArray = base64String.ToCharArray(startIndex, length);
        writer.WriteChars(charArray, 0, charArray.Length);
        startIndex += 1000;
      }
    }
  }
}
