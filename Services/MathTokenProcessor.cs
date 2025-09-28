using System.Text.RegularExpressions;

namespace Calculator.Services;

// MathTokenProcessor
//  - Process math tokens (digits, operators, equal symbol, clean symbol)
public partial class MathTokenProcessor
{
  bool hasDigitBeenEntered = false;

  [GeneratedRegex(@"\d")]
  private static partial Regex DigitRegex();

  [GeneratedRegex(@"[+\-x÷]")]
  private static partial Regex OperatorRegex();

  bool IsDigit(string mathToken) =>
    DigitRegex().IsMatch(mathToken);

  bool IsOperator(string mathToken) =>
    OperatorRegex().IsMatch(mathToken);

  public event Action<string>? OnDigitProcessed;
  public event Action<string>? OnOperatorProcessed;
  public event Action? OnEqualSymbolProcessed;
  public event Action? OnCleanSymbolProcessed;

  public void Process(string mathToken)
  {
    if (IsDigit(mathToken))
    {
      hasDigitBeenEntered = true;
      OnDigitProcessed?.Invoke(mathToken);
    }
    else if (IsOperator(mathToken) && hasDigitBeenEntered)
    {
      OnOperatorProcessed?.Invoke(mathToken);
    }
    else if (mathToken == "=")
    {
      OnEqualSymbolProcessed?.Invoke();
    }
    else if (mathToken == "C")
    {
      OnCleanSymbolProcessed?.Invoke();
      hasDigitBeenEntered = false;
    }
  }
}
