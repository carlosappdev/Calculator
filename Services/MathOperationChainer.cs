namespace Calculator.Services;

// MathOperationChainer
//  - Chain operations together
public class MathOperationChainer
{
  public Models.Operation? OperationBuilder { set; get; }
  public Models.Operation? LastOperation { set; get; }
  public string TextResult => LastOperation?.Result.ToString() ?? "0";
  public bool IsBuilding => OperationBuilder is not null;

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
    if (IsBuildingIfNotBuilding(number))
    {
      OperationBuilder!.B = number;
      LastOperation = OperationBuilder;
      OperationBuilder = new() { A = number, Previous = LastOperation };
    }
  }

  bool IsBuildingIfNotBuilding(double number)
  {
    bool isBuilding = IsBuilding;
    OperationBuilder ??= new() { A = number };
    return isBuilding;
  }
}
