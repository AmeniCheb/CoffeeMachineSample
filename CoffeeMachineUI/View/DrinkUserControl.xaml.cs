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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoffeMachineUI.ViewModel;

namespace CoffeMachineUI.View
{
    /// <summary>
    /// Logique d'interaction pour DrinkUserControl.xaml
    /// </summary>
    public partial class DrinkUserControl : UserControl
    {
        public DrinkUserControl()
        {
            InitializeComponent();
            DataContext = new DrinkViewModel();
        }
    }
}
