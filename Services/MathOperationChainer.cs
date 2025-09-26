namespace Calculator.Services;

// MathOperationChainer
//  - Chain operations together
public class MathOperationChainer
{
  public Models.Operation? OperationBuilder { set; get; }
  public Models.Operation? LastOperation { set; get; }

  public string TextResult => LastOperation?.Result.ToString() ?? "0";

  public void OnNewOperator(string operatorSymbol)
  {
    OperationBuilder!.Operator = operatorSymbol;
  }

  public void OnNewValue(string number) =>
    OnNewValue(double.Parse(number));

  public void Reset()
  {
    OperationBuilder = null;
    LastOperation = null;
  }

  void OnNewValue(double digit)
  {
    if (OperationBuilder is null)
    {
      OperationBuilder = new() { A = digit };
    }
    else
    {
      OperationBuilder.B = digit;
      LastOperation = OperationBuilder;
      OperationBuilder = new() { A = digit, Previous = LastOperation };
    }
  }
}
