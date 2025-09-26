namespace Calculator.Models;

public class Operation
{
  readonly static Dictionary<string, Func<double, double, double>> OPERATIONS = new()
  {
    { "+", (a, b) => a + b },
    { "-", (a, b) => a - b },
    { "x", (a, b) => a * b },
    { "÷", (a, b) => a / b },
  };

  public Operation? Previous { set; get; }
  public string? Operator { set; get; }
  public double A { set; get; }
  public double B { set; get; }
  public double Result
  {
    get
    {
      var a = Previous?.Result ?? A;
      return OPERATIONS[Operator!](a, B);
    }
  }
}