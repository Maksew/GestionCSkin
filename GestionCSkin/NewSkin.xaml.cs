using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace GestionCSkin
{

    public class FloatToCanvasLeftConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sliderValue = (double)value;
            // Suppose that the Canvas width is 200
            return sliderValue * 200; // Adjust this formula as needed
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class NewSkin : Window
    {
        private static NewSkin instance;

        private const double CanvasWidth = 400;
        private const double ArrowWidth = 20;

        public static NewSkin Instance
        {
            get
            {
                if (instance == null || instance.IsClosed)
                {
                    instance = new NewSkin();
                }
                return instance;
            }
        }

        public bool IsClosed { get; private set; }

        public NewSkin()
        {
            InitializeComponent();
            IsClosed = false;
            this.Closed += NewSkin_Closed;
            FloatSlider.ValueChanged += FloatSlider_ValueChanged; // S'assurer que le gestionnaire d'événements est attaché
        }

        #region NavButtons
        private void ProfitButton_Click(object sender, RoutedEventArgs e)
        {
            var profitCalculatorWindow = ProfitCalculator.Instance;
            profitCalculatorWindow.Show();
            profitCalculatorWindow.Focus();
        }
        #endregion



        private void NewSkin_Closed(object sender, EventArgs e)
        {
            IsClosed = true;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Mettez à jour la partie gauche avec les valeurs entrées par l'utilisateur
            LeftSkinName.Text = SkinNameInput.Text;
            LeftPrice.Text = $"Prix d'achat : {PriceInput.Text} €";
            LeftType.Text = $"Type : {TypeInput.Text}";
            FloatValueTextBlock.Text = FloatSlider.Value.ToString("0.000");

            UpdateProfitDisplay(FloatSlider.Value);
        }

        private void FloatSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateProfitDisplay(FloatSlider.Value);
        }

        private void UpdateProfitDisplay(double sliderValue)
        {

            arrow.Visibility = Visibility.Visible;
            LeftFloatValue.Visibility = Visibility.Visible;

            double arrowPosition = CalculateArrowPosition(sliderValue);
            Canvas.SetLeft(arrow, arrowPosition);
            Canvas.SetTop(arrow, 25);

            LeftFloatValue.Text = sliderValue.ToString("F3") + " (valeur)";
            Color zoneColor = GetColorForSliderValue(sliderValue);
            LeftFloatValue.Foreground = new SolidColorBrush(zoneColor);
        }

        private double CalculateArrowPosition(double profit)
        {
            var zones = new[]
            {
                (start: 0.0, end: 80.0, minProfit: 50.0, maxProfit: 200.0), // Dark Green zone
                (start: 80.0, end: 160.0, minProfit: 30.0, maxProfit: 49.99), // Light Green zone
                (start: 160.0, end: 240.0, minProfit: 15.0, maxProfit: 29.99), // Yellow zone
                (start: 240.0, end: 320.0, minProfit: 5.0, maxProfit: 14.99), // Orange zone
                (start: 320.0, end: CanvasWidth, minProfit: -50.0, maxProfit: 4.99) // Red zone
            };

            var currentZone = zones.FirstOrDefault(zone => profit >= zone.minProfit && profit <= zone.maxProfit);

            if (currentZone == default((double, double, double, double)))
            {
                if (profit < zones[0].minProfit)
                {
                    return CanvasWidth - ArrowWidth;
                }
                else
                {
                    return 0.0;
                }
            }

            double normalizedProfit = (currentZone.maxProfit - profit) / (currentZone.maxProfit - currentZone.minProfit);
            double positionWithinZone = normalizedProfit * (currentZone.end - currentZone.start);
            double arrowPosition = currentZone.start + positionWithinZone;

            arrowPosition = Math.Max(currentZone.start, Math.Min(arrowPosition, currentZone.end - ArrowWidth));

            return arrowPosition;
        }

        private Color GetColorForSliderValue(double sliderValue)
        {
            if (sliderValue < 0.2) return Colors.Red;
            else if (sliderValue < 0.4) return Colors.Orange;
            else if (sliderValue < 0.6) return Colors.Yellow;
            else if (sliderValue < 0.8) return Colors.LightGreen;
            else return Colors.DarkGreen;
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage userImage = new BitmapImage(new Uri(openFileDialog.FileName));

                LeftImage.Source = userImage;
                LeftImage.Stretch = Stretch.UniformToFill; 

                TextBlock textBlock = FindVisualChild<TextBlock>(sender as Button);
                if (textBlock != null)
                {
                    textBlock.Visibility = Visibility.Collapsed;
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindVisualChild<T>(child);

                    if (foundChild != null)
                        break;
                }
            }
            return foundChild;
        }





    }


}
