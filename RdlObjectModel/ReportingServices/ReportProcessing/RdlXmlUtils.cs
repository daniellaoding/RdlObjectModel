using System;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.ReportProcessing
{
  internal sealed class RdlXmlUtils
  {
    private XPathNavigator _navigator;
    private XmlNamespaceManager _manager;

    public RdlXmlUtils(XPathDocument rdlDoc)
    {
      Init(rdlDoc);
    }

    public bool UsesNamespacePrefixes(string nsPrefixes)
    {
      string xpath = NamespacePrefixesToXPath(nsPrefixes);
      if (string.IsNullOrEmpty(xpath))
        return false;
      return _navigator.Select(xpath, _manager).Count > 0;
    }

    private void Init(XPathDocument rdlDoc)
    {
      _navigator = rdlDoc.CreateNavigator();
      _manager = new XmlNamespaceManager(_navigator.NameTable);
      XPathNodeIterator xpathNodeIterator = _navigator.Select("//namespace::*[not(. = ../../namespace::*)]");
      while (xpathNodeIterator.MoveNext())
        _manager.AddNamespace(xpathNodeIterator.Current.Name, xpathNodeIterator.Current.Value);
    }

    private static string NamespacePrefixesToXPath(string nsPrefixes)
    {
      string[] strArray = nsPrefixes.Split(new char[1]
      {
        ' '
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length == 0)
        return null;
      string str1 = "";
      foreach (string str2 in strArray)
        str1 = str1 + "//" + str2 + ":*|";
      return "(" + str1.TrimEnd('|') + ")[1]";
    }

    internal class TestAgent
    {
      public static string NamespacePrefixesToXPath(string nsPrefixes)
      {
        return RdlXmlUtils.NamespacePrefixesToXPath(nsPrefixes);
      }
    }
  }
}
