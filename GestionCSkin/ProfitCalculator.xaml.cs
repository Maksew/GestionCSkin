using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionCSkin
{
    public partial class ProfitCalculator : Window
    {
        private const double CanvasWidth = 400; 
        private const double ArrowWidth = 20; 

        public ProfitCalculator()
        {
            InitializeComponent();
        }

        private void CalculateProfit_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtBuyPrice.Text, out double buyPrice))
            {
                MessageBox.Show("Le prix d'achat n'est pas un nombre valide.");
                return;
            }

            if (!double.TryParse(txtSellPrice.Text, out double sellPrice))
            {
                MessageBox.Show("Le prix de vente n'est pas un nombre valide.");
                return;
            }

            double profit = CalculateNetProfit(buyPrice, sellPrice);

            UpdateProfitDisplay(profit);
        }

        private void UpdateProfitDisplay(double profit)
        {
            arrow.Visibility = Visibility.Visible;
            txtProfitResult.Visibility = Visibility.Visible;

            txtProfitResult.Text = profit.ToString("F2");
            Color zoneColor = GetColorForProfit(profit);
            txtProfitResult.Foreground = new SolidColorBrush(zoneColor);

            double arrowPosition = CalculateArrowPosition(profit);
            Canvas.SetLeft(arrow, arrowPosition);
            Canvas.SetTop(arrow, 25); 

            txtProfitResult.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(txtProfitResult, arrowPosition - (txtProfitResult.DesiredSize.Width / 2));
            Canvas.SetTop(txtProfitResult, Canvas.GetTop(arrow) + 10); 
        }

        private double CalculateArrowPosition(double profit)
        {
            // Define the color zones and their corresponding profit thresholds.
            var zones = new[]
            {
                (start: 0.0, end: 80.0, minProfit: 50.0, maxProfit: double.PositiveInfinity),   // Dark Green zone
                (start: 80.0, end: 160.0, minProfit: 30.0, maxProfit: 49.99), // Light Green zone
                (start: 160.0, end: 240.0, minProfit: 15.0, maxProfit: 29.99),// Yellow zone
                (start: 240.0, end: 320.0, minProfit: 5.0, maxProfit: 14.99), // Orange zone
                (start: 320.0, end: CanvasWidth, minProfit: double.NegativeInfinity, maxProfit: 4.99) // Red zone
            };

            // Find the current zone based on the profit.
            var currentZone = zones.First(zone => profit >= zone.minProfit && profit <= zone.maxProfit);

            // Normalize the profit within the current zone's range. Reverse the normalization.
            double normalizedProfit = 1 - ((profit - currentZone.minProfit) / (currentZone.maxProfit - currentZone.minProfit));

            // Calculate the arrow's position within the zone.
            double positionWithinZone = normalizedProfit * (currentZone.end - currentZone.start);

            // Compute the arrow's absolute position on the canvas.
            double arrowPosition = currentZone.start + positionWithinZone;

            // Ensure the arrow does not go beyond the zone's boundaries.
            arrowPosition = Math.Min(Math.Max(arrowPosition, currentZone.start), currentZone.end - ArrowWidth);

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

        private double CalculateNetProfit(double buyPrice, double sellPrice)
        {
            double feePercentage = sellPrice > 1000 ? 0.06 : 0.12;
            double fees = sellPrice * feePercentage;
            return sellPrice - buyPrice - fees;
        }
    }
}
