using System.Collections.ObjectModel;
using System.Windows.Input;
using GestionCSkin.Model;
using GestionCSkin.Views;

namespace GestionCSkin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<Skin> _skins;
        public ObservableCollection<Skin> Skins
        {
            get => _skins;
            set
            {
                _skins = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddSkinCommand { get; }
        public ICommand OpenProfitCalculatorCommand { get; }

        public MainWindowViewModel()
        {
            Skins = new ObservableCollection<Skin>();

            OpenAddSkinCommand = new RelayCommand(_ => OpenAddSkin());
            OpenProfitCalculatorCommand = new RelayCommand(_ => OpenProfitCalculator());
        }

        private void OpenAddSkin()
        {
            var newSkinWindow = NewSkin.Instance;
            newSkinWindow.Show();
            newSkinWindow.Focus();
        }

        private void OpenProfitCalculator()
        {
            var profitCalcWindow = ProfitCalculator.Instance;
            profitCalcWindow.Show();
            profitCalcWindow.Focus();
        }
    }
}