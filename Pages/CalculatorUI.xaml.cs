using Softahc.NestCRUD;

namespace Calculator.Pages;

public partial class CalculatorUI : ContentPage
{
	public CalculatorUI(ViewModels.CalculatorUI viewModel)
	{
		InitializeComponent();
		viewModel.Attach(this);
	}
}