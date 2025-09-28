using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator.ViewModels;

public partial class CalculatorUI(
  Services.MathTokenProcessor tokenProcessor
) : ObservableRecipient
{

  [ObservableProperty]
  public partial string Display { set; get; } = "";

  protected override void OnActivated()
  {
    tokenProcessor.OnDigitProcessed += ProcessDigit;
    tokenProcessor.OnReplacingOperator += ReplacingOperator;
    tokenProcessor.OnAddingNewOperator += AddingNewOperator;
    tokenProcessor.OnEqualSymbolProcessed += ProcessEqualSymbol;
    tokenProcessor.OnCleanSymbolProcessed += ProcessCleanSymbol;
  }

  [RelayCommand]
  void Compute(string mathToken) =>
    tokenProcessor.Process(mathToken);

  private void ProcessDigit(string mathToken) =>
    Display += mathToken;

  void ReplacingOperator(string operatorSymbol) =>
    Display = $"{Display[..^3]} {operatorSymbol} ";

  void AddingNewOperator(string operatorSymbol) =>
    Display += $" {operatorSymbol} ";

  private void ProcessEqualSymbol() =>
    Display = tokenProcessor.TextResult;

  private void ProcessCleanSymbol() =>
    Display = "";
}