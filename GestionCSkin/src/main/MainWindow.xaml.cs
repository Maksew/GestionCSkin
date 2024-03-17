using System.Windows;


namespace GestionCSkin
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region NavButtons
        private void ProfitButton_Click(object sender, RoutedEventArgs e)
        {
            var profitCalculatorWindow = ProfitCalculator.Instance;
            profitCalculatorWindow.Show();
            profitCalculatorWindow.Focus();
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
            var skinDetailsWindow = NewSkin.Instance;
            skinDetailsWindow.Show();
            skinDetailsWindow.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
