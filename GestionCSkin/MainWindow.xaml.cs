using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionCSkin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProfitButton_Click(object sender, RoutedEventArgs e)
        {
            // Créer une instance de la fenêtre ProfitCalculator
            ProfitCalculator profitCalculatorWindow = new ProfitCalculator();

            // Afficher la fenêtre ProfitCalculator
            profitCalculatorWindow.Show();
        }


        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez ici votre logique pour le bouton Statistiques
            MessageBox.Show("Statistiques clicked");
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez ici votre logique pour le bouton Ventes
            MessageBox.Show("Ventes clicked");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez ici votre logique pour le bouton Ajouter
            MessageBox.Show("Ajouter clicked");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
