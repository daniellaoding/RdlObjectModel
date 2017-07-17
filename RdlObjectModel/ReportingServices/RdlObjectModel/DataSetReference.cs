using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class DataSetReference : ReportObject
  {
    public string DataSetName
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

    public string ValueField
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

    [DefaultValue("")]
    public string LabelField
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

    public DataSetReference()
    {
    }

    internal DataSetReference(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    internal override void UpdateNamedReferences(NameChanges nameChanges)
    {
      base.UpdateNamedReferences(nameChanges);
      DataSetName = nameChanges.GetNewName(NameChanges.EntryType.DataSet, DataSetName);
    }

    protected override void GetDependenciesCore(IList<ReportObject> dependencies)
    {
      base.GetDependenciesCore(dependencies);
      Report ancestor = GetAncestor<Report>();
      if (ancestor == null)
        return;
      DataSet dataSetByName = ancestor.GetDataSetByName(DataSetName);
      if (dataSetByName == null || dependencies.Contains(dataSetByName))
        return;
      dependencies.Add(dataSetByName);
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      DataSetReference dataSetReference = rdlObj as DataSetReference;
      if (dataSetReference == null || !string.Equals(ValueField, dataSetReference.ValueField, StringComparison.Ordinal) || string.IsNullOrEmpty(DataSetName) && !string.IsNullOrEmpty(dataSetReference.DataSetName) || !string.IsNullOrEmpty(DataSetName) && string.IsNullOrEmpty(dataSetReference.DataSetName))
        return false;
      if (DataSetName != null)
      {
        bool flag = false;
        Report ancestor1 = rdlObj.GetAncestor<Report>();
        Report ancestor2 = GetAncestor<Report>();
        if (ancestor1 != null && ancestor2 != null)
        {
          DataSet dataSetByName1 = ancestor1.GetDataSetByName(dataSetReference.DataSetName);
          DataSet dataSetByName2 = ancestor2.GetDataSetByName(DataSetName);
          if (dataSetByName1 != null && dataSetByName2 != null)
            flag = dataSetByName2.RdlSemanticEquals(dataSetByName1, visitedList);
        }
        if (!flag)
          return false;
      }
      return true;
    }

    internal class Definition : DefinitionStore<DataSetReference, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        DataSetName,
        ValueField,
        LabelField,
      }
    }
  }
}
