using System.Windows;
using GestionCSkin.ViewModels;

namespace GestionCSkin.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}