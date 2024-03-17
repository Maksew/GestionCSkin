using System;
using System.Globalization;
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
            return sliderValue * 200; 
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
            FloatSlider.ValueChanged += FloatSlider_ValueChanged;
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
            double zoneWidth = 40;
            double arrowPosition = CalculateArrowPosition(sliderValue);
            Canvas.SetLeft(arrow, arrowPosition);

            if (sliderValue < 0.07) // Factory New
                arrowPosition = sliderValue / 0.07 * zoneWidth;
            else if (sliderValue < 0.15) // Minimal Wear
                arrowPosition = (sliderValue - 0.07) / (0.15 - 0.07) * zoneWidth + zoneWidth;
            else if (sliderValue < 0.38) // Field-Tested
                arrowPosition = (sliderValue - 0.15) / (0.38 - 0.15) * zoneWidth + zoneWidth * 2;
            else if (sliderValue < 0.45) // Well-Worn
                arrowPosition = (sliderValue - 0.38) / (0.45 - 0.38) * zoneWidth + zoneWidth * 3;
            else // Battle-Scarred
                arrowPosition = (sliderValue - 0.45) / (1 - 0.45) * zoneWidth + zoneWidth * 4;

            Canvas.SetLeft(arrow, arrowPosition);

            arrow.Visibility = Visibility.Visible;
            Color arrowColor = GetColorForSliderValue(sliderValue);

            double arrowTop = 15 + 15; 
            Canvas.SetTop(arrow, arrowTop);

            double textTop = arrowTop + 15 + 4; 
            Canvas.SetTop(LeftFloatValue, textTop);

            LeftFloatValue.Foreground = new SolidColorBrush(GetColorForSliderValue(sliderValue));
            LeftFloatValue.Text = sliderValue.ToString("F3") + "";
            Canvas.SetLeft(LeftFloatValue, arrowPosition - (LeftFloatValue.ActualWidth / 2) + 10);
        }



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sliderValue = (double)value;
            double canvasWidth = 200;
            double arrowWidth = 20; 
            return Math.Max(0, Math.Min(canvasWidth - arrowWidth, sliderValue * (canvasWidth - arrowWidth)));
        }

        private Color GetColorForSliderValue(double sliderValue)
        {
            sliderValue = Math.Min(sliderValue, 0.999);


            if (sliderValue >= 0.45) return Colors.Red; // Battle-Scarred
            else if (sliderValue >= 0.38) return Colors.Orange; // Well-Worn
            else if (sliderValue >= 0.15) return Colors.Yellow; // Field-Tested
            else if (sliderValue >= 0.07) return Colors.LightGreen; // Minimal Wear
            else return Colors.DarkGreen; // Factory New
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

        private double CalculateArrowPosition(double sliderValue)
        {
            sliderValue = Math.Min(sliderValue, 0.9999); 
            double totalWidth = 200.0; 
            double offset = ArrowWidth / 2.0;
            double positionWithinTotalWidth = sliderValue / 0.9999 * totalWidth;
            return positionWithinTotalWidth - offset;
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

        private void PriceInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PriceInput.Text))
            {
                LeftPrice.Text = PriceInput.Text + " €";
            }
            else
            {
                LeftPrice.Text = string.Empty;
            }
        }

    }


}
