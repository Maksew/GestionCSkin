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

        public NewSkinViewModel()
        {
            AddSkinCommand = new RelayCommand(_ => AddSkin());
        }

        private void AddSkin()
        {
            // Exemple : création du Skin et logique d’ajout dans une collection, DB, etc.
            var newSkin = new Skin(SkinName, BuyPrice, SellPrice, FloatValue, SelectedSkinType, ImagePath);

            // Ici, vous pouvez l’ajouter dans une ObservableCollection, l’envoyer en base, etc.
            // Par exemple :
            // SkinRepository.Instance.Add(newSkin);
        }
    }
}
