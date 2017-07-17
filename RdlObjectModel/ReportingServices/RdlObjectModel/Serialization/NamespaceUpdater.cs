using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
  internal sealed class NamespaceUpdater
  {
    public string[] Update(XmlWriter writer, string xml, ISerializerHost host = null)
    {
      XmlDocument document = new XmlDocument();
      document.LoadXml(xml);
      string[] strArray = new NamespaceUpdater().Update(document, host);
      document.Save(writer);
      return strArray;
    }

    public string[] Update(XmlDocument document, ISerializerHost host = null)
    {
      if (document == null)
        throw new ArgumentNullException("document");
      Dictionary<string, ExtensionNamespaceCounter> dictionary = host == null ? new Dictionary<string, ExtensionNamespaceCounter>() : host.GetExtensionNamespaces().ToDictionary(v => v.Namespace, v => new ExtensionNamespaceCounter(v));
      XmlElement documentElement = document.DocumentElement;
      if (documentElement == null)
        throw new InvalidOperationException("Document contains no elements");
      AddMustUnderstandNamespaces(dictionary, documentElement);
      AddXmlnsNamespaces(dictionary, documentElement);
      UpdateLocalNames(documentElement, dictionary);
      ExtensionNamespace[] array1 = dictionary.Where(v => v.Value.Count > 0).Select(v => v.Value.ExtensionNamespace).ToArray();
      UpdateRootNamespaces(array1, documentElement);
      string[] array2 = array1.Where(v => v.MustUnderstand).Select(v => v.LocalName).ToArray();
      string source = string.Join(" ", array2);
      if (source.Any())
        documentElement.SetAttribute("MustUnderstand", source);
      else
        documentElement.RemoveAttribute("MustUnderstand");
      return array2;
    }

    private void AddXmlnsNamespaces(Dictionary<string, ExtensionNamespaceCounter> xmlnsDictionary, XmlElement rootElem)
    {
      foreach (XmlAttribute xmlAttribute in rootElem.Attributes.Cast<XmlAttribute>().Where(a =>
      {
	      if (a.Prefix == "xmlns")
		      return !xmlnsDictionary.ContainsKey(a.Value);
	      return false;
      }))
      {
        ExtensionNamespace extensionNamespace = new ExtensionNamespace(xmlAttribute.LocalName, xmlAttribute.Value, false);
        xmlnsDictionary.Add(xmlAttribute.Value, new ExtensionNamespaceCounter(extensionNamespace));
      }
    }

    private static void UpdateRootNamespaces(IEnumerable<ExtensionNamespace> namespaces, XmlElement rootElem)
    {
      int count = rootElem.Attributes.Count;
      while (count-- > 0)
      {
        if (rootElem.Attributes[count].Prefix == "xmlns")
          rootElem.Attributes.RemoveAt(count);
      }
      foreach (ExtensionNamespace extensionNamespace in namespaces)
        rootElem.SetAttribute(string.Format("xmlns:{0}", extensionNamespace.LocalName), extensionNamespace.Namespace);
    }

    private void AddMustUnderstandNamespaces(Dictionary<string, ExtensionNamespaceCounter> xmlnsDictionary, XmlElement rootElem)
    {
      XmlNode namedItem = rootElem.Attributes.GetNamedItem("MustUnderstand");
      if (namedItem == null || string.IsNullOrEmpty(namedItem.Value))
        return;
      foreach (string str in namedItem.Value.Split())
      {
        string namespaceOfPrefix = rootElem.GetNamespaceOfPrefix(str);
        if (!string.IsNullOrEmpty(namespaceOfPrefix) && !xmlnsDictionary.ContainsKey(namespaceOfPrefix))
          xmlnsDictionary.Add(namespaceOfPrefix, new ExtensionNamespaceCounter(new ExtensionNamespace(str, namespaceOfPrefix, true)));
      }
    }

    private void UpdateLocalNames(XmlElement xmlElement, Dictionary<string, ExtensionNamespaceCounter> xmlnsDictionary)
    {
      Stack<XmlNode> xmlNodeStack = new Stack<XmlNode>(new XmlElement[1]
      {
	      xmlElement
      });
      while (xmlNodeStack.Count != 0)
      {
        XmlNode xmlNode1 = xmlNodeStack.Pop();
        ExtensionNamespaceCounter namespaceCounter;
        if (xmlnsDictionary.TryGetValue(xmlNode1.NamespaceURI, out namespaceCounter))
        {
          xmlNode1.Prefix = namespaceCounter.ExtensionNamespace.LocalName;
          XmlElement xmlElement1 = xmlNode1 as XmlElement;
          if (xmlElement1 != null)
            xmlElement1.RemoveAttribute("xmlns");
          ++namespaceCounter.Count;
        }
        foreach (XmlNode xmlNode2 in (xmlNode1.Attributes == null ? (IEnumerable<XmlNode>) new XmlNode[0] : xmlNode1.Attributes.Cast<XmlNode>()).Concat(xmlNode1.ChildNodes.Cast<XmlNode>()))
          xmlNodeStack.Push(xmlNode2);
      }
    }

    private sealed class ExtensionNamespaceCounter
    {
      public ExtensionNamespace ExtensionNamespace { get; private set; }

      public int Count { get; set; }

      public ExtensionNamespaceCounter(ExtensionNamespace extensionNamespace)
      {
        ExtensionNamespace = extensionNamespace;
        Count = 0;
      }
    }
  }
}
