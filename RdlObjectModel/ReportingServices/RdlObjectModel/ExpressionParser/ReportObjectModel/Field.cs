using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
  internal abstract class Field
  {
    internal abstract object Value { get; }

    internal abstract bool IsMissing { get; }

    internal abstract string UniqueName { get; }

    internal abstract string BackgroundColor { get; }

    internal abstract string Color { get; }

    internal abstract string FontFamily { get; }

    internal abstract string FontSize { get; }

    internal abstract string FontWeight { get; }

    internal abstract string FontStyle { get; }

    internal abstract string TextDecoration { get; }

    internal abstract string FormattedValue { get; }

    internal abstract object Key { get; }

    internal abstract int LevelNumber { get; }

    internal abstract string ParentUniqueName { get; }

    [IndexerName("Properties")]
    internal abstract object this[string key] { get; }
  }
}
