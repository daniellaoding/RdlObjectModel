using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class FunctionSystem : FunctionMultiArgument
  {
    private static Dictionary<string, List<MemberInfo>> _FunctionsMap = new Dictionary<string, List<MemberInfo>>(StringUtil.CaseInsensitiveComparer);
	  private TypeCode _ReturnTypeCode;
    private readonly Type[] _ArgTypes;
    private MemberInfo _MemberInfo;
    private ParameterInfo[] _ParameterInfos;
    private readonly IInternalExpression _ObjectExpression;
    private object _CallObject;

    public Type ClassType { get; internal set; }

	  public string Func { get; internal set; }

	  internal FunctionSystem(IReportLink container, Type classType, string functionName, IInternalExpression[] args, Dictionary<string, List<MemberInfo>> functionsMap)
      : this(container, classType, functionName, args, null, functionsMap)
    {
    }

    internal FunctionSystem(IReportLink container, Type classType, string functionName, IInternalExpression[] args, IInternalExpression owningObject, Dictionary<string, List<MemberInfo>> functionsMap)
      : base(args)
    {
      ClassType = classType;
      Func = functionName;
      _FunctionsMap = functionsMap;
      _ReturnTypeCode = System.TypeCode.Boolean;
      IsArray = false;
      _ObjectExpression = owningObject;
      _ArgTypes = new Type[Arguments.Length];
      for (int index = 0; index < _ArgTypes.Length; ++index)
        _ArgTypes[index] = !Arguments[index].IsArray || Arguments[index].TypeCode() == System.TypeCode.Object ? Type.GetType("System." + Arguments[index].TypeCode().ToString()) : Type.GetType("System." + Arguments[index].TypeCode().ToString() + "[]");
      List<MemberInfo> members = _ObjectExpression == null ? SearchMembers(classType, Func) : SearchMembers(_ObjectExpression.TypeCode(), Func);
      if (members.Count == 0)
        throw new NotSupportedException("Method " + Func + " is not supported.");
      if (!DesignTimeValidate(members))
        throw new ArgumentException(ParamDescription());
    }

    public override TypeCode TypeCode()
    {
      return _ReturnTypeCode;
    }

    public override string WriteSource(NameChanges nameChanges)
    {
      string str1 = "";
      if (_ObjectExpression != null)
        str1 = str1 + _ObjectExpression.WriteSource(nameChanges) + ".";
      string str2 = str1 + (ClassType == (Type) null ? string.Empty : ClassType.FullName + ".");
      string[] strArray = new string[Arguments.Length];
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = Arguments[index].WriteSource(nameChanges);
      string str3 = str2 + _MemberInfo.Name;
      if (_MemberInfo.MemberType == MemberTypes.Method || strArray.Length > 0)
        str3 = str3 + "(" + string.Join(",", strArray) + ")";
      return str3;
    }

    public override object Evaluate()
    {
      if (_ObjectExpression != null)
        _CallObject = _ObjectExpression.Evaluate();
      if ((object) (_MemberInfo as FieldInfo) != null)
        return ((FieldInfo) _MemberInfo).GetValue(_CallObject);
      if ((object) (_MemberInfo as PropertyInfo) != null)
        return ((PropertyInfo) _MemberInfo).GetValue(_CallObject, null);
      if ((object) (_MemberInfo as MethodInfo) == null)
        return "";
      if (_ObjectExpression == null && Attribute.IsDefined(_ParameterInfos[_ParameterInfos.Length - 1], typeof (ParamArrayAttribute)))
        return InvokeWithParamArray();
      if (_ParameterInfos.Length > Arguments.Length)
        return InvokeWithOptionalParam();
      return InvokeWithRegularParam();
    }

    private List<MemberInfo> SearchMembers(Type type, string memberName)
    {
      List<MemberInfo> memberInfoList = new List<MemberInfo>();
      if (type == null)
      {
        if (!_FunctionsMap.ContainsKey(memberName))
          throw new NotSupportedException("Method " + Func + " is not supported.");
        memberInfoList = _FunctionsMap[memberName];
      }
      else
      {
        foreach (MemberInfo memberInfo in type.Name == "Code" ? type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) : type.GetMembers(BindingFlags.Static | BindingFlags.Public))
        {
          if (string.Equals(memberName, memberInfo.Name, StringComparison.OrdinalIgnoreCase))
            memberInfoList.Add(memberInfo);
        }
      }
      return memberInfoList;
    }

    private List<MemberInfo> SearchMembers(TypeCode typeCode, string memberName)
    {
      List<MemberInfo> memberInfoList = new List<MemberInfo>();
      foreach (Type type in Assembly.GetAssembly(typeof (string)).GetTypes())
      {
        if (type.Name == GetObjectName(typeCode))
        {
          foreach (MemberInfo member in type.GetMembers())
          {
            if (string.Equals(memberName, member.Name, StringComparison.OrdinalIgnoreCase))
              memberInfoList.Add(member);
          }
          return memberInfoList;
        }
      }
      return memberInfoList;
    }

    private bool MatchMethod(MethodInfo methodInfo)
    {
      _MemberInfo = methodInfo;
      _ReturnTypeCode = Type.GetTypeCode(methodInfo.ReturnType);
      IsArray = methodInfo.ReturnType.IsArray;
      if (!ArgumentValidation(methodInfo, false))
        return ArgumentValidation(methodInfo, true);
      return true;
    }

    private bool MatchProperty(PropertyInfo propertyInfo)
    {
      _MemberInfo = propertyInfo;
      _ReturnTypeCode = Type.GetTypeCode(propertyInfo.PropertyType);
      IsArray = propertyInfo.PropertyType.IsArray;
      if (_ArgTypes.Length > 0)
        throw new ArgumentException();
      return true;
    }

    private bool MatchField(FieldInfo fieldInfo)
    {
      _MemberInfo = fieldInfo;
      _ReturnTypeCode = Type.GetTypeCode(fieldInfo.DeclaringType);
      IsArray = fieldInfo.DeclaringType.IsArray;
      if (_ArgTypes.Length > 0)
        throw new ArgumentException();
      return true;
    }

    private bool DesignTimeValidate(List<MemberInfo> members)
    {
      foreach (MemberInfo member in members)
      {
        if ((object) (member as PropertyInfo) != null && MatchProperty(member as PropertyInfo) || (object) (member as MethodInfo) != null && MatchMethod(member as MethodInfo) || (object) (member as FieldInfo) != null && MatchField(member as FieldInfo))
          return true;
      }
      if ((object) (members[0] as MethodInfo) != null)
        MatchMethod(members[0] as MethodInfo);
      if ((object) (members[0] as PropertyInfo) != null)
        MatchProperty(members[0] as PropertyInfo);
      if ((object) (members[0] as FieldInfo) != null)
        MatchField(members[0] as FieldInfo);
      return false;
    }

    private void DesignTimeValidate()
    {
    }

    private string ParamDescription()
    {
      string[] strArray1 = new string[_ParameterInfos.Length];
      for (int index1 = 0; index1 < _ParameterInfos.Length; ++index1)
      {
        strArray1[index1] = "ByVal ";
        if (Attribute.IsDefined(_ParameterInfos[index1], typeof (ParamArrayAttribute)))
          strArray1[index1] = "ParamArray ";
        else if (_ParameterInfos[index1].IsOptional)
          strArray1[index1] = "Optional ";
        string[] strArray2;
        int index2 = 0;
        (strArray2 = strArray1)[index2 = index1] = strArray2[index2] + _ParameterInfos[index1].Name + " As " + _ParameterInfos[index1].ParameterType.Name;
      }
      return "function " + Func + "(" + string.Join(",", strArray1) + ")";
    }

    private bool ArgumentValidation(MethodInfo methodInfo, bool allowTypeConvert)
    {
      _ParameterInfos = methodInfo.GetParameters();
      if (_ParameterInfos.Length == 0 && Arguments.Length > 0 || _ParameterInfos.Length < Arguments.Length && !Attribute.IsDefined(_ParameterInfos[_ParameterInfos.Length - 1], typeof (ParamArrayAttribute)) || _ParameterInfos.Length > Arguments.Length && !_ParameterInfos[Arguments.Length].IsOptional)
        return false;
      for (int index = 0; index < Arguments.Length; ++index)
      {
        if ((!(_ParameterInfos[index].ParameterType == typeof (bool)) || !(_ArgTypes[index] == typeof (int))) && !(_ParameterInfos[index].ParameterType == typeof (object)) && ((!(_ParameterInfos[index].ParameterType == typeof (object[])) || !_ArgTypes[index].IsArray) && _ParameterInfos[index].ParameterType != _ArgTypes[index]))
        {
          if (Attribute.IsDefined(_ParameterInfos[index], typeof (ParamArrayAttribute)))
          {
            if (!(_ParameterInfos[index].ParameterType.GetElementType() == typeof (object)))
            {
              if (_ParameterInfos[index].ParameterType.GetElementType() == _ArgTypes[index])
                break;
            }
            else
              break;
          }
          try
          {
            object obj = Arguments[index].Evaluate();
            if (_ParameterInfos[index].Attributes == ParameterAttributes.Out || _ParameterInfos[index].Attributes == ParameterAttributes.In)
            {
              if (!allowTypeConvert)
                return obj == null || obj.GetType() == _ParameterInfos[index].ParameterType.GetElementType();
              if (!(obj is IConvertible))
                return false;
              Convert.ChangeType(obj, _ParameterInfos[index].ParameterType.GetElementType(), CultureInfo.CurrentCulture);
            }
            else
            {
              if (!allowTypeConvert)
                return obj == null || obj.GetType() == _ParameterInfos[index].ParameterType;
              if (!(obj is IConvertible))
                return false;
              Convert.ChangeType(obj, _ParameterInfos[index].ParameterType, CultureInfo.CurrentCulture);
              if (RDLUtil.IsNarrowingConversion(obj.GetType(), _ParameterInfos[index].ParameterType))
                return false;
            }
          }
          catch
          {
            return false;
          }
        }
      }
      return true;
    }

    private object[] PrepareArgumentType()
    {
      object[] objArray = new object[Arguments.Length];
      for (int index = 0; index < objArray.Length; ++index)
      {
        objArray[index] = Arguments[index].Evaluate();
        if (_ParameterInfos[index].ParameterType.IsArray && !objArray[index].GetType().IsArray)
        {
          Array instance = Array.CreateInstance(objArray[index].GetType(), 1);
          instance.SetValue(objArray[index], 0);
          objArray[index] = instance;
        }
        if (objArray[index] != null)
          objArray[index] = Convert.ChangeType(objArray[index], _ParameterInfos[index].ParameterType, CultureInfo.CurrentCulture);
      }
      return objArray;
    }

    private object InvokeWithRegularParam()
    {
      object[] parameters = PrepareArgumentType();
      try
      {
        return ((MethodBase) _MemberInfo).Invoke(_CallObject, parameters);
      }
      catch
      {
        throw new ExpressionParserException("HELP");
      }
    }

    private object InvokeWithParamArray()
    {
      object[] parameters = new object[_ParameterInfos.Length];
      object[] objArray = new object[Arguments.Length - _ParameterInfos.Length + 1];
      Type elementType = _ParameterInfos[_ParameterInfos.Length - 1].ParameterType.GetElementType();
      for (int index = 0; index < Arguments.Length; ++index)
      {
        if (index < _ParameterInfos.Length - 1)
        {
          parameters[index] = Arguments[index].Evaluate();
          parameters[index] = Convert.ChangeType(parameters[index], _ParameterInfos[index].ParameterType, CultureInfo.CurrentCulture);
        }
        else
        {
          objArray[index - _ParameterInfos.Length + 1] = Arguments[index].Evaluate();
          objArray[index - _ParameterInfos.Length + 1] = Convert.ChangeType(objArray[index - _ParameterInfos.Length + 1], elementType, CultureInfo.CurrentCulture);
        }
      }
      parameters[_ParameterInfos.Length - 1] = objArray;
      try
      {
        return ((MethodBase) _MemberInfo).Invoke(_CallObject, parameters);
      }
      catch
      {
        throw new ExpressionParserException("HELP");
      }
    }

    private object InvokeWithOptionalParam()
    {
      object[] parameters = new object[_ParameterInfos.Length];
      for (int index = 0; index < Arguments.Length; ++index)
      {
        parameters[index] = Arguments[index].Evaluate();
        parameters[index] = Convert.ChangeType(parameters[index], _ParameterInfos[index].ParameterType, CultureInfo.CurrentCulture);
      }
      for (int length = Arguments.Length; length < parameters.Length; ++length)
        parameters[length] = _ParameterInfos[length].DefaultValue;
      try
      {
        return ((MethodBase) _MemberInfo).Invoke(_CallObject, parameters);
      }
      catch
      {
        throw new ExpressionParserException("HELP");
      }
    }

    private string GetObjectName(TypeCode typeCode)
    {
      FunctionSystem objectExpression = _ObjectExpression as FunctionSystem;
      if (objectExpression != null && objectExpression.IsArray)
        return "Array";
      switch (typeCode)
      {
        case System.TypeCode.Empty:
          return "Empty";
        case System.TypeCode.Object:
          return "Object";
        case System.TypeCode.DBNull:
          return "DBNull";
        case System.TypeCode.Boolean:
          return "Boolean";
        case System.TypeCode.Char:
          return "Char";
        case System.TypeCode.SByte:
          return "SByte";
        case System.TypeCode.Byte:
          return "Byte";
        case System.TypeCode.Int16:
          return "Int16";
        case System.TypeCode.UInt16:
          return "UInt16";
        case System.TypeCode.Int32:
          return "Int32";
        case System.TypeCode.UInt32:
          return "UInt32";
        case System.TypeCode.Int64:
          return "Int64";
        case System.TypeCode.UInt64:
          return "UInt64";
        case System.TypeCode.Single:
          return "Single";
        case System.TypeCode.Double:
          return "Double";
        case System.TypeCode.Decimal:
          return "Decimal";
        case System.TypeCode.DateTime:
          return "DateTime";
        case System.TypeCode.String:
          return "String";
        default:
          return "String";
      }
    }
  }
}
