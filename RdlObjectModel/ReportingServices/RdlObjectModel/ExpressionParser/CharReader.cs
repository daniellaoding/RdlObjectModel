using System.IO;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
  internal sealed class CharReader
  {
	  private int _TempColumn = 1;
    private readonly string _TextRun;
    private int _CurrentPosition;

    internal char Peek
    {
      get
      {
        if (FileEnd())
          return char.MinValue;
        return _TextRun[_CurrentPosition];
      }
    }

    internal char Peek2
    {
      get
      {
        ++_CurrentPosition;
        char peek = Peek;
        --_CurrentPosition;
        return peek;
      }
    }

    internal int Line { get; private set; } = 1;

	  internal int Column { get; private set; } = 1;

	  internal CharReader(TextReader file)
    {
      _TextRun = file.ReadToEnd();
      file.Close();
    }

    internal bool FileEnd()
    {
      return _CurrentPosition >= _TextRun.Length;
    }

    internal char Get()
    {
      if (FileEnd())
        return char.MinValue;
      ++Column;
      if (_TextRun[_CurrentPosition] == 10)
      {
        ++Line;
        Column = 1;
        _TempColumn = Column;
      }
      ++_CurrentPosition;
      return _TextRun[_CurrentPosition - 1];
    }

    internal void PutBack()
    {
      --_CurrentPosition;
      if (_CurrentPosition < 0)
        throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.EndExpected", "UnGet before first character", 1, 1);
      if (_TextRun[_CurrentPosition] != 10)
        return;
      Column = _TempColumn;
      --Line;
    }
  }
}
