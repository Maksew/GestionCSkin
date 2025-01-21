using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GestionCSkin.ViewModels;

    namespace GestionCSkin.Views
{
    public partial class ProfitCalculator : Window
    {
        private static ProfitCalculator instance;

        public static ProfitCalculator Instance
        {
            get
            {
                if (instance == null || instance.IsClosed)
                {
                    instance = new ProfitCalculator();
                }
                return instance;
            }
        }

        public bool IsClosed { get; private set; }

        private const double CanvasWidth = 400; 
        private const double ArrowWidth = 20;

        public ProfitCalculator()
        {
            InitializeComponent();
            IsClosed = false;
            this.Closed += (sender, e) => IsClosed = true;

            // Si vous n’avez pas défini le DataContext dans le XAML,
            // faites-le ici :
            // DataContext = new ProfitCalculatorViewModel();

            // On s’abonne au PropertyChanged du VM, pour réagir
            var vm = DataContext as ProfitCalculatorViewModel;
            if (vm != null)
            {
                vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        #region NavButtons
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var skinDetailsWindow = NewSkin.Instance;
            skinDetailsWindow.Show();
            skinDetailsWindow.Focus();
        }
        #endregion

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProfitCalculatorViewModel.Profit))
            {
                // Dès que la propriété "Profit" du VM change,
                // on met à jour l'affichage visuel
                var vm = (ProfitCalculatorViewModel)sender;
                double profit = vm.Profit;

                UpdateProfitDisplay(profit);
            }
        }

        private void UpdateProfitDisplay(double profit)
        {
            arrow.Visibility = Visibility.Visible;
            txtProfitResult.Visibility = Visibility.Visible;

            double arrowPosition = CalculateArrowPosition(profit);
            Canvas.SetLeft(arrow, arrowPosition);
            Canvas.SetTop(arrow, 25);

            txtProfitResult.Text = profit.ToString("F2") + " €";
            Color zoneColor = GetColorForProfit(profit);
            txtProfitResult.Foreground = new SolidColorBrush(zoneColor);

            // On calcule la position idéale du texte
            txtProfitResult.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            double arrowCenter = arrowPosition + (ArrowWidth / 2);
            double textCenter = txtProfitResult.DesiredSize.Width / 2;
            Canvas.SetLeft(txtProfitResult, arrowCenter - textCenter);
            Canvas.SetTop(txtProfitResult, Canvas.GetTop(arrow) + 20); 
        }

        private double CalculateArrowPosition(double profit)
        {
            // Votre logique de répartition en zones
            // (Exemple existant, gardé tel quel)
            var zones = new[]
            {
                (start: 0.0,   end: 80.0,  minProfit: 50.0,   maxProfit: 200.0),
                (start: 80.0,  end: 160.0, minProfit: 30.0,   maxProfit: 49.99),
                (start: 160.0, end: 240.0, minProfit: 15.0,   maxProfit: 29.99),
                (start: 240.0, end: 320.0, minProfit: 5.0,    maxProfit: 14.99),
                (start: 320.0, end: 400.0, minProfit: -50.0,  maxProfit: 4.99)
            };

            var currentZone = zones.FirstOrDefault(zone => 
                profit >= zone.minProfit && profit <= zone.maxProfit);

            // Si pas trouvé => on sort de la plage
            if (currentZone == default)
            {
                // Si profit < zone min => tout à droite ou tout à gauche selon votre logique
                if (profit < zones.Last().minProfit)
                    return CanvasWidth - ArrowWidth;
                else
                    return 0.0;
            }

            // Normalisation
            double normalizedProfit = (currentZone.maxProfit - profit) 
                                      / (currentZone.maxProfit - currentZone.minProfit);
            double positionWithinZone = normalizedProfit * (currentZone.end - currentZone.start);
            double arrowPosition = currentZone.start + positionWithinZone;
            arrowPosition = Math.Max(currentZone.start, 
                             Math.Min(arrowPosition, currentZone.end - ArrowWidth));

            return arrowPosition;
        }

        private Color GetColorForProfit(double profit)
        {
            if (profit >= 50)
                return Colors.DarkGreen;
            else if (profit >= 30)
                return Colors.LightGreen;
            else if (profit >= 15)
                return Colors.Yellow;
            else if (profit >= 5)
                return Colors.Orange;
            else
                return Colors.Red;
        }
    }
}
