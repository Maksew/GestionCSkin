using System.Windows.Input;
using GestionCSkin.Model;

namespace GestionCSkin.ViewModels
{
    public class ProfitCalculatorViewModel : BaseViewModel
    {
        private decimal _buyPrice;
        public decimal BuyPrice
        {
            get => _buyPrice;
            set
            {
                _buyPrice = value;
                OnPropertyChanged();
            }
        }

        private decimal _sellPrice;
        public decimal SellPrice
        {
            get => _sellPrice;
            set
            {
                _sellPrice = value;
                OnPropertyChanged();
            }
        }

        private double _profit;
        public double Profit
        {
            get => _profit;
            set
            {
                _profit = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateProfitCommand { get; }

        public ProfitCalculatorViewModel()
        {
            CalculateProfitCommand = new RelayCommand(_ => CalculateProfit());
        }

        private void CalculateProfit()
        {
            Profit = ProfitCalculatorService.CalculateNetProfit((double)BuyPrice, (double)SellPrice);
        }
    }
}