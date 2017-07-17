using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal class EnvironmentContext : NamespaceContext
  {
	  protected IEnvironmentFilter m_filter;

    internal virtual ReportObjectModelContext ReportObjectModelContext { get; }

	  internal virtual LateBoundContext LateBoundContext { get; }

	  internal static EnvironmentContext DefaultEnvironment { get; } = new EnvironmentContext(new ReportObjectModelContext(), DefaultEnvironmentFilter.Instance);

	  internal EnvironmentContext(ReportObjectModelContext reportOmContext, IEnvironmentFilter filter)
      : this()
    {
      ReportObjectModelContext = reportOmContext;
      LateBoundContext = new LateBoundContext(filter);
      m_filter = filter;
      InitEnvironment(filter);
    }

    protected EnvironmentContext()
      : base("EnvironmentContext")
    {
    }

    internal EnvironmentContext InitializeCustomAssemblies(List<Assembly> assemblies)
    {
      return new CustomEnvironmentContext(this, assemblies);
    }

    private sealed class CustomEnvironmentContext : EnvironmentContext
    {
      private readonly EnvironmentContext m_defaultEnvironment;

      internal override LateBoundContext LateBoundContext => m_defaultEnvironment.LateBoundContext;

	    internal override ReportObjectModelContext ReportObjectModelContext => m_defaultEnvironment.ReportObjectModelContext;

	    internal CustomEnvironmentContext(EnvironmentContext defaultEnvironment, List<Assembly> customAssemblies)
      {
        m_defaultEnvironment = defaultEnvironment;
        foreach (Assembly customAssembly in customAssemblies)
          ProcessAssembly(customAssembly, m_defaultEnvironment.m_filter, null, null);
      }

      internal override bool TryMatchMember(string identifier, out MemberContext member)
      {
        if (!base.TryMatchMember(identifier, out member))
          return m_defaultEnvironment.TryMatchMember(identifier, out member);
        return true;
      }

      internal override bool TryMatchSubContext(string identifier, out LookupContext subContext)
      {
        if (!base.TryMatchSubContext(identifier, out subContext))
          return m_defaultEnvironment.TryMatchSubContext(identifier, out subContext);
        return true;
      }
    }
  }
}
