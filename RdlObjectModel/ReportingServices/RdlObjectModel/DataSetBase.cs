using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public abstract class DataSetBase : ReportObject, IGlobalNamedObject, INamedObject, IDataScopeService, IDataScope, IContainedObject
  {
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

    [DefaultValue(CaseSensitivities.Auto)]
    public CaseSensitivities CaseSensitivity
    {
      get
      {
        return (CaseSensitivities) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    [DefaultValue("")]
    public string Collation
    {
      get
      {
        return (string) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    [DefaultValue(AccentSensitivities.Auto)]
    public AccentSensitivities AccentSensitivity
    {
      get
      {
        return (AccentSensitivities) PropertyStore.GetInteger(3);
      }
      set
      {
        PropertyStore.SetInteger(3, (int) value);
      }
    }

    [DefaultValue(KanatypeSensitivities.Auto)]
    public KanatypeSensitivities KanatypeSensitivity
    {
      get
      {
        return (KanatypeSensitivities) PropertyStore.GetInteger(4);
      }
      set
      {
        PropertyStore.SetInteger(4, (int) value);
      }
    }

    [DefaultValue(WidthSensitivities.Auto)]
    public WidthSensitivities WidthSensitivity
    {
      get
      {
        return (WidthSensitivities) PropertyStore.GetInteger(5);
      }
      set
      {
        PropertyStore.SetInteger(5, (int) value);
      }
    }

    [DefaultValue(InterpretSubtotalsAsDetailsTypes.Auto)]
    public InterpretSubtotalsAsDetailsTypes InterpretSubtotalsAsDetails
    {
      get
      {
        return (InterpretSubtotalsAsDetailsTypes) PropertyStore.GetInteger(6);
      }
      set
      {
        PropertyStore.SetInteger(6, (int) value);
      }
    }

    [XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
    [DefaultValue(null)]
    public DataSetInfo DataSetInfo
    {
      get
      {
        return PropertyStore.GetObject<DataSetInfo>(134217729);
      }
      set
      {
        PropertyStore.SetObject(134217729, value);
      }
    }

    Group IDataScope.Group => null;

	  public DataSetBase()
    {
    }

    internal DataSetBase(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public abstract QueryBase GetQuery();

    public override void Initialize()
    {
      base.Initialize();
    }

    public IEnumerable<IDataScope> GetDataScopesFor(IContainedObject obj)
    {
      if (!IsChildOf(obj, this))
        throw new InvalidOperationException();
      return GetContainingDataScopes();
    }

    public bool Equals(DataSetBase dataSetBase)
    {
      if (dataSetBase == null || AccentSensitivity != dataSetBase.AccentSensitivity || (CaseSensitivity != dataSetBase.CaseSensitivity || !(Collation == dataSetBase.Collation)) || (DataSetInfo != dataSetBase.DataSetInfo || InterpretSubtotalsAsDetails != dataSetBase.InterpretSubtotalsAsDetails || (KanatypeSensitivity != dataSetBase.KanatypeSensitivity || !(Name == dataSetBase.Name))))
        return false;
      return WidthSensitivity == dataSetBase.WidthSensitivity;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as DataSetBase);
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      if (CheckVisitedAndUpdate(this, visitedList))
        return true;
      DataSetBase dataSetBase = rdlObj as DataSetBase;
      return dataSetBase != null && CaseSensitivity == dataSetBase.CaseSensitivity && (AccentSensitivity == dataSetBase.AccentSensitivity && WidthSensitivity == dataSetBase.WidthSensitivity) && (KanatypeSensitivity == dataSetBase.KanatypeSensitivity && InterpretSubtotalsAsDetails == dataSetBase.InterpretSubtotalsAsDetails && string.Equals(Collation, dataSetBase.Collation, StringComparison.OrdinalIgnoreCase));
    }

    internal class Definition : DefinitionStore<DataSetBase, Definition.Properties>
    {
      internal enum Properties
      {
        Name,
        CaseSensitivity,
        Collation,
        AccentSensitivity,
        KanatypeSensitivity,
        WidthSensitivity,
        InterpretSubtotalsAsDetails,
      }
    }

    internal class RDDefinition : DefinitionStore<DataSet, RDDefinition.Properties>
    {
      public enum Properties
      {
        DataSetInfo = 134217729,
      }
    }
  }
}
