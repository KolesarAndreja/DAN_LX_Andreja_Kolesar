using DAN_XLII_Andreja_Kolesar.Service;
using System.Windows;

namespace DAN_XLII_Andreja_Kolesar.View
{
    /// <summary>
    /// Interaction logic for AddAndEditEmployeeWindow.xaml
    /// </summary>
    public partial class AddAndEditEmployeeWindow : Window
    {
        //edit
        public AddAndEditEmployeeWindow(vwEmployee user)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AddWindowViewModel(this, user);
        }

        //add
        public AddAndEditEmployeeWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.AddWindowViewModel(this);
        }
    }
}
