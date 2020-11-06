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
using Concert_Booking_Project;

namespace Concert_Booking_WPF_App
{
    public partial class MainWindow : Window
    {
        private BookingCodeLayer _bookingCodeLayer = new BookingCodeLayer();
        public MainWindow()
        {
            InitializeComponent();
            PopulateVenueFields();
        }
        private void PopulateVenueFields()
        {
            VenueListBox.ItemsSource = _bookingCodeLayer.RetrieveAllVenues();
            // Figure out how to display more variables when DB is filled
            VenueListBox.DisplayMemberPath = "Name";
        }
        private void VenueListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VenueListBox != null)
            {
                _bookingCodeLayer.SetSelectedVenue(VenueListBox.SelectedItem);
                //PopulateEventFields();

                VenueNameTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Name;
                VenueCityTextBoxA.Text = _bookingCodeLayer.SelectedVenue.City;
                VenueEmailTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Email;
                VenuePhoneTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Phone;
            }
            else
                _bookingCodeLayer.SelectedVenue = null;
        }


        private void VenueAddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = VenueNameTextBoxB.Text;
            var city = VenueCityTextBoxB.Text;
            var email = VenueEmailTextBoxB.Text;
            var phone = VenuePhoneTextBoxB.Text;

            _bookingCodeLayer.AddVenue(name, city, email, phone);
            PopulateVenueFields();
        }

    }
}
