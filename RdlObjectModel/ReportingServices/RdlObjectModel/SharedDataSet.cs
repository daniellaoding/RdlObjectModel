using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class SharedDataSet : ReportObject
  {
	  public string SharedDataSetReference
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

    [XmlElement(typeof (RdlCollection<QueryParameter>))]
    public IList<QueryParameter> QueryParameters
    {
      get
      {
        return (IList<QueryParameter>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue("")]
    public string ReportServerUrl { get; set; }

	  public SharedDataSet()
    {
    }

    internal SharedDataSet(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      QueryParameters = new RdlCollection<QueryParameter>();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      SharedDataSet sharedDataSet = rdlObj as SharedDataSet;
      return sharedDataSet != null && string.Equals(SharedDataSetReference, sharedDataSet.SharedDataSetReference, StringComparison.OrdinalIgnoreCase) && SemanticCompare(QueryParameters, sharedDataSet.QueryParameters, visitedList);
    }

    internal class Definition : DefinitionStore<SharedDataSet, Definition.Properties>
    {
      internal enum Properties
      {
        SharedDataSetReference,
        QueryParameters,
      }
    }
  }
}
