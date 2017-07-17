namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class ObjectModel
  {
    internal abstract Fields Fields { get; }

    internal abstract Parameters Parameters { get; }

    internal abstract Globals Globals { get; }

    internal abstract User User { get; }

    internal abstract ReportItems ReportItems { get; }

    internal abstract DataSets DataSets { get; }

    internal abstract DataSources DataSources { get; }

    internal abstract Variables Variables { get; }
  }
}
