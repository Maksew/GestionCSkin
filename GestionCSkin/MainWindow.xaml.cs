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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProfitButton_Click(object sender, RoutedEventArgs e)
        {
            // Ajoutez ici votre logique pour le bouton Calculer profit
            MessageBox.Show("Calculer profit clicked");
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
    }
}
