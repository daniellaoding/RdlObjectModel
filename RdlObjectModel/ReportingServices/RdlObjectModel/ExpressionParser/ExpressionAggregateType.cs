namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal enum ExpressionAggregateType
  {
    Sum,
    Avg,
    Max,
    Min,
    Count,
    CountDistinct,
    CountRows,
    StDev,
    StDevP,
    Var,
    VarP,
    First,
    Last,
    Previous,
    RunningValue,
    RowNumber,
    Aggregate,
    NoAggregate,
  }
}
