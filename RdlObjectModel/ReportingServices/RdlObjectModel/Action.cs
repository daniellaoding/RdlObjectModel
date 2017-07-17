namespace Microsoft.ReportingServices.RdlObjectModel
{
  public class Action : ReportObject
  {
    [ReportExpressionDefaultValue]
    public ReportExpression Hyperlink
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

    public Drillthrough Drillthrough
    {
      get
      {
        return (Drillthrough) PropertyStore.GetObject(1);
      }
      set
      {
        PropertyStore.SetObject(1, value);
      }
    }

    [ReportExpressionDefaultValue]
    public ReportExpression BookmarkLink
    {
      get
      {
        return PropertyStore.GetObject<ReportExpression>(2);
      }
      set
      {
        PropertyStore.SetObject(2, value);
      }
    }

    public Action()
    {
    }

    internal Action(IPropertyStore propertyStore)
      : base(propertyStore)
    {
    }

    public override bool Equals(object obj)
    {
      Action action = obj as Action;
      if (action == null)
        return false;
      bool flag1 = BookmarkLink == action.BookmarkLink || BookmarkLink != null && BookmarkLink.Equals(action.BookmarkLink);
      bool flag2 = Hyperlink == action.Hyperlink || Hyperlink != null && Hyperlink.Equals(action.Hyperlink);
      bool flag3 = Drillthrough == action.Drillthrough || Drillthrough != null && Drillthrough.Equals(action.Drillthrough);
      if (flag1 && flag2)
        return flag3;
      return false;
    }

    public override int GetHashCode()
    {
      if (BookmarkLink != null)
        return BookmarkLink.GetHashCode();
      if (Hyperlink != null)
        return Hyperlink.GetHashCode();
      if (Drillthrough != null)
        return Drillthrough.GetHashCode();
      return 0;
    }

    internal class Definition : DefinitionStore<Action, Definition.Properties>
    {
      private Definition()
      {
      }

      internal enum Properties
      {
        Hyperlink,
        Drillthrough,
        BookmarkLink,
      }
    }
  }
}
