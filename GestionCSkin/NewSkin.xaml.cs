using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionCSkin
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

        public NewSkin()
        {
            InitializeComponent();
            IsClosed = false;
            this.Closed += NewSkin_Closed;
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

    }


}
