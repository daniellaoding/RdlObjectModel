using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Filter : ReportObject
  {
    public ReportExpression FilterExpression
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public Operators Operator
    {
      get
      {
        return (Operators) PropertyStore.GetInteger(1);
      }
      set
      {
        PropertyStore.SetInteger(1, (int) value);
      }
    }

    [XmlElement(typeof (RdlCollection<ReportExpression>))]
    [XmlArrayItem("FilterValue", typeof (ReportExpression))]
    public IList<ReportExpression> FilterValues
    {
      get
      {
        return (IList<ReportExpression>) PropertyStore.GetObject(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Filter()
    {
    }

    internal Filter(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      FilterValues = new RdlCollection<ReportExpression>();
    }

    public bool Equals(Filter filter)
    {
      if (filter == null || !(FilterExpression == filter.FilterExpression) || (FilterValues != filter.FilterValues || Operator != filter.Operator))
        return false;
      return base.Equals(filter);
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Filter);
    }

    public override int GetHashCode()
    {
      return FilterExpression.GetHashCode();
    }

    protected override bool RdlSemanticEqualsCore(ReportObject rdlObj, ICollection<ReportObject> visitedList)
    {
      Filter filter = rdlObj as Filter;
      if (filter == null || Operator != filter.Operator || !CompareReportParamterExpression(FilterExpression, this, filter.FilterExpression, filter, visitedList) || (FilterValues != null && filter.FilterValues == null || FilterValues == null && filter.FilterValues != null))
        return false;
      if (FilterValues != null)
      {
        if (FilterValues.Count != filter.FilterValues.Count)
          return false;
        for (int index = 0; index < FilterValues.Count; ++index)
        {
          if (!CompareReportParamterExpression(FilterValues[index], this, filter.FilterValues[index], filter, visitedList))
            return false;
        }
      }
      return true;
    }

    internal class Definition
    {
      internal enum Properties
      {
        FilterExpression,
        Operator,
        FilterValues,
      }
    }
    
  }
}
