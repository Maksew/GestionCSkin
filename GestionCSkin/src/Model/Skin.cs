using Newtonsoft.Json;

namespace GestionCSkin.Model
{
    public class Skin
    {
        public string Name { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public double FloatValue { get; set; }
        public SkinType Type { get; set; }
        
        public string ImagePath { get; set; }
        
        [JsonIgnore] 
        public decimal Profit => SellPrice - BuyPrice;
        
        public Skin() { }
        
        public Skin(string name, decimal buyPrice, decimal sellPrice, double floatValue, 
            SkinType type, string imagePath)
        {
            Name = name;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
            FloatValue = floatValue;
            Type = type;
            ImagePath = imagePath;
        }
    }
}