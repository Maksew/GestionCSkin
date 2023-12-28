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

            double arrowPosition = CalculateArrowPosition(profit);
            Canvas.SetLeft(arrow, arrowPosition);
            Canvas.SetTop(arrow, 25);

            txtProfitResult.Text = profit.ToString("F2") + " €";
            Color zoneColor = GetColorForProfit(profit);
            txtProfitResult.Foreground = new SolidColorBrush(zoneColor);

            txtProfitResult.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

            double arrowCenter = arrowPosition + (ArrowWidth / 2);
            double textCenter = txtProfitResult.DesiredSize.Width / 2;
            Canvas.SetLeft(txtProfitResult, arrowCenter - textCenter);
            Canvas.SetTop(txtProfitResult, Canvas.GetTop(arrow) + 20); 
        }


        private double CalculateArrowPosition(double profit)
        {
            var zones = new[]
            {
                (start: 0.0, end: 80.0, minProfit: 50.0, maxProfit: 300.0), // Dark Green zone
                (start: 80.0, end: 160.0, minProfit: 30.0, maxProfit: 49.99), // Light Green zone
                (start: 160.0, end: 240.0, minProfit: 15.0, maxProfit: 29.99), // Yellow zone
                (start: 240.0, end: 320.0, minProfit: 5.0, maxProfit: 14.99), // Orange zone
                (start: 320.0, end: CanvasWidth, minProfit: -50.0, maxProfit: 4.99) // Red zone
            };

            // Trouvez la zone actuelle en utilisant FirstOrDefault
            var currentZone = zones.FirstOrDefault(zone => profit >= zone.minProfit && profit <= zone.maxProfit);

            // Si aucun élément n'est trouvé, c'est que le profit est hors des plages prévues
            if (currentZone == default((double, double, double, double)))
            {
                if (profit < zones[0].minProfit)
                {
                    // Profit est inférieur au min de la première zone, placer à l'extrémité droite
                    return CanvasWidth - ArrowWidth;
                }
                else
                {
                    // Profit est supérieur au max de la dernière zone, placer à l'extrémité gauche
                    return 0.0;
                }
            }

            // Inverser la direction de la flèche pour toutes les zones
            double normalizedProfit = (currentZone.maxProfit - profit) / (currentZone.maxProfit - currentZone.minProfit);
            double positionWithinZone = normalizedProfit * (currentZone.end - currentZone.start);
            double arrowPosition = currentZone.start + positionWithinZone;

            // S'assurer que la flèche reste dans les limites de la zone actuelle
            arrowPosition = Math.Max(currentZone.start, Math.Min(arrowPosition, currentZone.end - ArrowWidth));

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
