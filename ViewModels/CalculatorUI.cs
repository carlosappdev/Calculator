using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator.ViewModels;

public partial class CalculatorUI(
  Services.MathTokenProcessor tokenProcessor,
  Services.MathOperationChainer operationChainer
) : ObservableRecipient
{

  [ObservableProperty]
  public partial string Display { set; get; } = "";

  string numberBuffer = "";

  //bool canOperatorBeAdded = false;

  bool isOperatorBeingProcessed = false;

  protected override void OnActivated()
  {
    tokenProcessor.OnDigitProcessed += ProcessDigit;
    tokenProcessor.OnOperatorProcessed += ProcessOperator;
    tokenProcessor.OnEqualSymbolProcessed += ProcessEqualSymbol;
    tokenProcessor.OnCleanSymbolProcessed += ProcessCleanSymbol;
  }

  [RelayCommand]
  void Compute(string mathToken)
  {
    tokenProcessor.Process(mathToken);
  }

  private void ProcessDigit(string mathToken)
  {
    //canOperatorBeAdded = true;
    isOperatorBeingProcessed = false;

    numberBuffer += mathToken;
    Display += mathToken;
  }

  private void ProcessOperator(string mathToken)
  {
    //if (!canOperatorBeAdded) return;

    if (isOperatorBeingProcessed)
    {
      Display = $"{Display[..^3]} {mathToken} ";
    }
    else
    {
      operationChainer.OnNewValue(numberBuffer);
      Display += $" {mathToken} ";
      numberBuffer = "";
      isOperatorBeingProcessed = true;
    }
    operationChainer.OnNewOperator(mathToken);
  }

  private void ProcessEqualSymbol()
  {
    if (operationChainer.OperationBuilder is null || isOperatorBeingProcessed)
      return;

    operationChainer.OnNewValue(numberBuffer);
    Clean(operationChainer.TextResult);
  }

  private void ProcessCleanSymbol()
  {
    //canOperatorBeAdded = false;
    isOperatorBeingProcessed = false;
    Clean("");
  }

  void Clean(string output)
  {
    Display = output;
    numberBuffer = output;
    operationChainer.Reset();
  }
}