namespace GestionCSkin.Model
{
    public static class ProfitCalculatorService
    {
        /// <summary>
        /// Calcule le profit net en fonction du prix d’achat et de vente, 
        /// en appliquant un pourcentage de frais.
        /// </summary>
        public static double CalculateNetProfit(double buyPrice, double sellPrice)
        {
            double feePercentage = sellPrice > 1000 ? 0.06 : 0.12;
            double fees = sellPrice * feePercentage;
            return sellPrice - buyPrice - fees;
        }
    }
}