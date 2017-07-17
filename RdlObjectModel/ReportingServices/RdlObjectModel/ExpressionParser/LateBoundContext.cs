namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class LateBoundContext : LookupContext
  {
    private readonly IEnvironmentFilter m_filter;

    internal LateBoundContext(IEnvironmentFilter filter)
      : base("LateBoundContext")
    {
      m_filter = filter;
    }

    internal override bool TryMatchMember(string identifier, out MemberContext member)
    {
      if (m_filter.IsAllowedMember(identifier))
      {
        member = new MemberContext(identifier, MemberContext.MemberContextTypes.Unknown);
        return true;
      }
      member = null;
      return false;
    }
  }
}
