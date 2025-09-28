using System.Text.RegularExpressions;

namespace Calculator.Services;

// MathTokenProcessor
//  - Process math tokens (digits, operators, equal symbol, clean symbol)
public partial class MathTokenProcessor(
  MathOperationChainer operationChainer
)
{
  [GeneratedRegex(@"\d")]
  private static partial Regex DigitRegex();

  [GeneratedRegex(@"[+\-x÷]")]
  private static partial Regex OperatorRegex();

  bool hasDigitBeenEntered = false;
  bool isOperatorBeingProcessed = false;
  string numberBuffer = "";

  public string TextResult => operationChainer.TextResult;

  public event Action<string>? OnDigitProcessed;
  public event Action<string>? OnAddingNewOperator;
  public event Action<string>? OnReplacingOperator;
  public event Action? OnEqualSymbolProcessed;
  public event Action? OnCleanSymbolProcessed;

  bool IsDigit(string mathToken) =>
    DigitRegex().IsMatch(mathToken);

  bool IsOperator(string mathToken) =>
    OperatorRegex().IsMatch(mathToken);

  public void Process(string mathToken)
  {
    if (IsDigit(mathToken))
    {
      hasDigitBeenEntered = true;
      isOperatorBeingProcessed = false;
      numberBuffer += mathToken;
      OnDigitProcessed?.Invoke(mathToken);
    }
    else if (IsOperator(mathToken) && hasDigitBeenEntered)
    {
      if (isOperatorBeingProcessed)
      {
        OnReplacingOperator?.Invoke(mathToken);
      }
      else
      {
        operationChainer.OnNewValue(numberBuffer);
        OnAddingNewOperator?.Invoke(mathToken);
        numberBuffer = "";
        isOperatorBeingProcessed = true;
      }
      operationChainer.OnNewOperator(mathToken);
    }
    else if (mathToken == "=" && operationChainer.IsBuilding && !isOperatorBeingProcessed)
    {
      operationChainer.OnNewValue(numberBuffer);
      OnEqualSymbolProcessed?.Invoke();
      numberBuffer = TextResult;
      operationChainer.Reset();
    }
    else if (mathToken == "C")
    {
      OnCleanSymbolProcessed?.Invoke();
      isOperatorBeingProcessed = false;
      hasDigitBeenEntered = false;
      numberBuffer = "";
      operationChainer.Reset();
    }
  }
}
