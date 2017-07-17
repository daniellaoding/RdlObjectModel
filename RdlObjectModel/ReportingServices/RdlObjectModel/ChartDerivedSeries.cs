using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class ChartDerivedSeries : ReportObject
  {
    public ChartSeries ChartSeries
    {
      get
      {
        return (ChartSeries) PropertyStore.GetObject(0);
      }
      set
      {
        PropertyStore.SetObject(0, value);
      }
    }

    public string SourceChartSeriesName
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

    public ChartFormulas DerivedSeriesFormula
    {
      get
      {
        return (ChartFormulas) PropertyStore.GetInteger(2);
      }
      set
      {
        PropertyStore.SetInteger(2, (int) value);
      }
    }

    [XmlElement(typeof (RdlCollection<ChartFormulaParameter>))]
    public IList<ChartFormulaParameter> ChartFormulaParameters
    {
      get
      {
        return (IList<ChartFormulaParameter>) PropertyStore.GetObject(3);
      }
      set
      {
        PropertyStore.SetObject(3, value);
      }
    }

    public ChartDerivedSeries()
    {
    }

    internal ChartDerivedSeries(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override void Initialize()
    {
      base.Initialize();
      ChartSeries = new ChartSeries();
      ChartFormulaParameters = new RdlCollection<ChartFormulaParameter>();
    }

    internal class Definition : DefinitionStore<ChartDerivedSeries, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        ChartSeries,
        SourceChartSeriesName,
        DerivedSeriesFormula,
        ChartFormulaParameters,
        PropertyCount,
      }
    }
  }
}
