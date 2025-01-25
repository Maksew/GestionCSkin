using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using GestionCSkin.Model;

namespace GestionCSkin.ViewModels
{
    public class NewSkinViewModel : BaseViewModel
    {
        private string _skinName;
        public string SkinName
        {
            get => _skinName;
            set
            {
                _skinName = value;
                OnPropertyChanged();
            }
        }

        private decimal _buyPrice;
        public decimal BuyPrice
        {
            get => _buyPrice;
            set
            {
                _buyPrice = value;
                OnPropertyChanged();
            }
        }

        private decimal _sellPrice;
        public decimal SellPrice
        {
            get => _sellPrice;
            set
            {
                _sellPrice = value;
                OnPropertyChanged();
            }
        }

        private double _floatValue = 0.000; 
        public double FloatValue
        {
            get => _floatValue;
            set
            {
                if (value < 0.0)
                    _floatValue = 0.0;
                else if (value > 0.999)
                    _floatValue = 0.999;
                else
                    _floatValue = value;

                OnPropertyChanged(); 
            }
        }
        
        private SkinType _selectedSkinType;
        public SkinType SelectedSkinType
        {
            get => _selectedSkinType;
            set
            {
                _selectedSkinType = value;
                OnPropertyChanged();
            }
        }


        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddSkinCommand { get; }
        
        private readonly SkinService _skinService = new SkinService();

        public NewSkinViewModel()
        {
            AddSkinCommand = new RelayCommand(_ => AddSkin());
            SelectedSkinType = SkinType.Rifle;
            FloatValue = 0.000;
            BuyPrice = 0;
            SellPrice = 0;
        }

        private void AddSkin()
{
    try
    {
        // Validation des champs obligatoires
        if (string.IsNullOrWhiteSpace(SkinName) || BuyPrice <= 0)
        {
            MessageBox.Show("Le nom et le prix d'achat sont obligatoires");
            return;
        }

        // Gestion de l'image
        string relativeImagePath = null;
        if (!string.IsNullOrWhiteSpace(ImagePath))
        {
            relativeImagePath = CopyImageToAppFolder(ImagePath);
            if (relativeImagePath == null)
            {
                MessageBox.Show("Échec du téléchargement de l'image");
                return;
            }
        }

        // Création du skin
        var newSkin = new Skin(
            SkinName.Trim(),
            BuyPrice,
            SellPrice,
            FloatValue,
            SelectedSkinType,
            relativeImagePath
        );

        // Sauvegarde dans le JSON
        var skins = _skinService.LoadSkins();
        skins.Add(newSkin);
        _skinService.SaveSkins(skins); // Sauvegarde d'abord

        // Mise à jour de l'UI
        if (Application.Current.MainWindow.DataContext is MainWindowViewModel mainVm)
        {
            mainVm.Skins.Add(newSkin); 
        }
        
        ResetForm();
        
        MessageBox.Show("Skin ajouté avec succès !", "Succès",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }
    catch (IOException ioEx)
    {
        MessageBox.Show($"Erreur de fichier : {ioEx.Message}", "Erreur",
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
    catch (UnauthorizedAccessException authEx)
    {
        MessageBox.Show($"Permission refusée : {authEx.Message}", "Erreur", 
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur inattendue : {ex.Message}", "Erreur",
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

        private string CopyImageToAppFolder(string sourcePath)
        {
            try
            {
                var imgDirectory = Path.Combine(Directory.GetCurrentDirectory(), "img");
                Directory.CreateDirectory(imgDirectory);
        
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(sourcePath)}"; // Nom unique
                var destPath = Path.Combine(imgDirectory, fileName);
        
                File.Copy(sourcePath, destPath, overwrite: true);
                return Path.Combine("img", fileName);
            }
            catch
            {
                return null; // Échec silencieux (géré dans AddSkin)
            }
        }

        private void RefreshMainWindow()
        {
            if (Application.Current.MainWindow?.DataContext is MainWindowViewModel mainVm)
            {
                mainVm.LoadSkins();
            }
        }

        private void ResetForm()
        {
            SkinName = string.Empty;
            BuyPrice = 0;
            SellPrice = 0;
            FloatValue = 0.000;
            SelectedSkinType = SkinType.Rifle;
            ImagePath = string.Empty;
            
            OnPropertyChanged(nameof(SkinName));
            OnPropertyChanged(nameof(BuyPrice));
            OnPropertyChanged(nameof(SellPrice));
            OnPropertyChanged(nameof(FloatValue));
            OnPropertyChanged(nameof(SelectedSkinType));
            OnPropertyChanged(nameof(ImagePath));
        }
    }
}
