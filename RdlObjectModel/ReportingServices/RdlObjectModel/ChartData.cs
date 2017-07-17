using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartData : DataRegionBody
  {
    [XmlElement(typeof (RdlCollection<ChartSeries>))]
    public IList<ChartSeries> ChartSeriesCollection
    {
      get
      {
        return (IList<ChartSeries>) PropertyStore.GetObject(0);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        PropertyStore.SetObject(0, value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartDerivedSeries>))]
    public IList<ChartDerivedSeries> ChartDerivedSeriesCollection
    {
      get
      {
        return (IList<ChartDerivedSeries>) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    public ChartData()
    {
    }

    internal ChartData(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartSeriesCollection = new RdlCollection<ChartSeries>();
      ChartDerivedSeriesCollection = new RdlCollection<ChartDerivedSeries>();
    }

    internal class Definition : DefinitionStore<ChartData, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ChartSeriesCollection,
        ChartDerivedSeriesCollection,
        PropertyCount,
      }
    }
  }
}
