namespace Calculator.Services;

// MathOperationChainer
//  - Chain operations together
public class MathOperationChainer
{
  public Models.Operation? OperationBuilder { set; get; }
  public Models.Operation? LastOperation { set; get; }

  public string TextResult => LastOperation?.Result.ToString() ?? "0";

  public void Reset()
  {
    OperationBuilder = null;
    LastOperation = null;
  }

  public void OnNewOperator(string operatorSymbol)
  {
    OperationBuilder!.Operator = operatorSymbol;
  }

  public void OnNewValue(string number) =>
    OnNewValue(double.Parse(number));

  void OnNewValue(double number)
  {
    // bool isBuilding = OperationBuilder is not null;
    // OperationBuilder ??= new() { A = number };

    if (IsBuilding(number))
    {
      OperationBuilder!.B = number;
      LastOperation = OperationBuilder;
      OperationBuilder = new() { A = number, Previous = LastOperation };
    }
  }

  bool IsBuilding(double number)
  {
    bool isBuilding = OperationBuilder is not null;
    OperationBuilder ??= new() { A = number };
    return isBuilding;
  }
}
