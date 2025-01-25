using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using GestionCSkin.ViewModels;

namespace GestionCSkin.Views
{
    public partial class NewSkin : Window
    {
        private static NewSkin instance;
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

        // Largeur “unitaire” d’une zone de couleur
        private const double ZoneWidth = 40;
        private const double ArrowWidth = 20;

        public NewSkin()
        {
            InitializeComponent();
            DataContext = new NewSkinViewModel();

            IsClosed = false;
            this.Closed += NewSkin_Closed;
        }

        private void NewSkin_Closed(object sender, EventArgs e)
        {
            IsClosed = true;
        }

        private void ProfitButton_Click(object sender, RoutedEventArgs e)
        {
            var profitCalculatorWindow = ProfitCalculator.Instance;
            profitCalculatorWindow.Show();
            profitCalculatorWindow.Focus();
        }

        // Gestion du clic sur "UploadImage"
        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                var vm = DataContext as NewSkinViewModel;
                vm.ImagePath = openFileDialog.FileName;
                
                BitmapImage userImage = new BitmapImage(new Uri(openFileDialog.FileName));
                LeftImage.Source = userImage;
                
                ClickLabel.Visibility = Visibility.Collapsed;
            }
        }


        // Quand la valeur du slider change, on met à jour la flèche + label float
        private void FloatSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateFloatDisplay(e.NewValue);
        }

        private void UpdateFloatDisplay(double sliderValue)
        {
            if (arrow == null || LeftFloatValue == null) return;

            arrow.Visibility = Visibility.Visible;

            // Calculer la largeur totale disponible
            double totalWidth = 5 * ZoneWidth;
            double arrowPosition;

            if (sliderValue <= 0.07)
            {
                arrowPosition = (sliderValue / 0.07) * ZoneWidth + 10;
            }
            else if (sliderValue <= 0.15)
            {
                arrowPosition = ((sliderValue - 0.07) / (0.15 - 0.07)) * ZoneWidth + (ZoneWidth + 10);
            }
            else if (sliderValue <= 0.38)
            {
                arrowPosition = ((sliderValue - 0.15) / (0.38 - 0.15)) * ZoneWidth + (2 * ZoneWidth + 10);
            }
            else if (sliderValue <= 0.45)
            {
                arrowPosition = ((sliderValue - 0.38) / (0.45 - 0.38)) * ZoneWidth + (3 * ZoneWidth + 10);
            }
            else
            {
                // Nouvelle approche pour la dernière section
                double lastZoneStart = 4 * ZoneWidth + 10;
                double remainingSpace = totalWidth - (4 * ZoneWidth);
                double normalizedValue = (sliderValue - 0.45) / (1.0 - 0.45);
                
                // Ajustement linéaire pour la dernière zone
                arrowPosition = lastZoneStart + (normalizedValue * remainingSpace);
                
                // Si on est très proche de 0.999, forcer la position maximale
                if (sliderValue > 0.998)
                {
                    arrowPosition = totalWidth + 10;
                }
            }

            // Assurer les limites
            double minPosition = 10;
            double maxPosition = totalWidth + 10;
            arrowPosition = Math.Max(minPosition, Math.Min(arrowPosition, maxPosition));

            // Ajuster pour le centre de la flèche
            Canvas.SetLeft(arrow, arrowPosition - (ArrowWidth / 2));
            Canvas.SetTop(arrow, 24);

            // Mettre à jour le texte
            LeftFloatValue.Text = sliderValue.ToString("F3");
            LeftFloatValue.Foreground = new SolidColorBrush(GetColorForFloat(sliderValue));

            // Positionner le texte
            LeftFloatValue.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            double textWidth = LeftFloatValue.DesiredSize.Width;
            double centerX = arrowPosition - (textWidth / 2);
            Canvas.SetLeft(LeftFloatValue, centerX);
            Canvas.SetTop(LeftFloatValue, 45);
        }
        
        // Détermine une couleur selon la “float”
        private Color GetColorForFloat(double sliderValue)
        {
            if (sliderValue <= 0.07)
                return Colors.DarkGreen;   // FN
            else if (sliderValue <= 0.15)
                return Colors.LightGreen;  // MW
            else if (sliderValue <= 0.38)
                return Colors.Yellow;      // FT
            else if (sliderValue <= 0.45)
                return Colors.Orange;      // WW
            else
                return Colors.Red;         // BS
        }
    }
}
