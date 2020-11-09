using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        // Conditional remove bools
        bool _venueRemoveConfirm = false;
        bool _eventRemoveConfirm = false;
        bool _ticketRemoveConfirm = false;

        bool _venueUpdateConfirm = false;
        bool _eventUpdateConfirm = false;

        List<string> genres = new List<string>()
        {
            "",
            "Alternative",
            "Avant-garde",
            "Black Metal",
            "Blues",
            "Choir",
            "Classical",
            "Comedy",
            "Country",
            "Death Metal",
            "Drum & Bass",
            "Dubstep",
            "Easy Listening",
            "EDM",
            "Electronic",
            "Folk",
            "Grime",
            "Grunge",
            "Goth/Gothic Rock",
            "Hardcore",
            "Heavy Metal",
            "House",
            "Indie",
            "Flamenco",
            "Folk",
            "Hip Hop",
            "Jazz",
            "Latin",
            "New Wave",
            "Orchestral",
            "Pop",
            "Punk Rock",
            "R&B & Soul",
            "Rock",
            "Ska",
            "Techno",
        };
        List<string> time = new List<string>()
        {
            "",
            "00:00",
            "00:30",
            "01:00",
            "01:30",
            "02:00",
            "02:30",
            "03:00",
            "03:30",
            "04:00",
            "04:30",
            "05:00",
            "05:30",
            "06:00",
            "06:30",
            "07:00",
            "07:00",
            "07:30",
            "08:00",
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00",
            "21:30",
            "22:00",
            "22:30",
            "23:00",
            "23:30"
        };


        public MainWindow()
        {
            InitializeComponent();
            PopulateVenueFields();

            EventGenreComboBoxA.ItemsSource = genres;
            EventStartTimeComboBoxA.ItemsSource = time;
            EventEndTimeComboBoxA.ItemsSource = time;
            EventGenreComboBoxB.ItemsSource = genres;
            EventStartTimeComboBoxB.ItemsSource = time;
            EventEndTimeComboBoxB.ItemsSource = time;
        }

        private void RestrictWhenRemoving()
        {
            // Restric list boxes
            VenueListBox.IsEnabled = false;
            EventListBox.IsEnabled = false;

            // Restrict writable text boxes
            VenueNameTextBoxA.IsReadOnly = true;
            VenueCityTextBoxA.IsReadOnly = true;
            VenueEmailTextBoxA.IsReadOnly = true;
            VenuePhoneTextBoxA.IsReadOnly = true;
            VenueNameTextBoxB.IsReadOnly = true;
            VenueCityTextBoxB.IsReadOnly = true;
            VenueEmailTextBoxB.IsReadOnly = true;
            VenuePhoneTextBoxB.IsReadOnly = true;

            // Restrict Buttons
            VenueAddButton.IsEnabled = false;
            VenueClearButton.IsEnabled = false;
        }
        private void UnrestrictAfterRemoveDecision()
        {
            // Unestric list boxes
            VenueListBox.IsEnabled = true;
            EventListBox.IsEnabled = true;

            // Unrestrict writable text boxes
            VenueNameTextBoxA.IsReadOnly = false;
            VenueCityTextBoxA.IsReadOnly = false;
            VenueEmailTextBoxA.IsReadOnly = false;
            VenuePhoneTextBoxA.IsReadOnly = false;
            VenueNameTextBoxB.IsReadOnly = false;
            VenueCityTextBoxB.IsReadOnly = false;
            VenueEmailTextBoxB.IsReadOnly = false;
            VenuePhoneTextBoxB.IsReadOnly = false;

            // Unrestrict Buttons
            VenueAddButton.IsEnabled = true;
            VenueClearButton.IsEnabled = true;
        }

        private void PopulateVenueFields()
        {
            VenueListBox.ItemsSource = _bookingCodeLayer.RetrieveAllVenues();
            // Figure out how to display more variables when DB is filled
            VenueListBox.DisplayMemberPath = "Name";
        }

        private void PopulateEventFields()
        {
            EventListBox.ItemsSource = _bookingCodeLayer.RetrieveAllEvents();
            EventListBox.DisplayMemberPath = "Event_Name";
        }

        private void PopulateTicketFields()
        {
            TicketListBox.ItemsSource = _bookingCodeLayer.RetrieveAllTickets();
            TicketListBox.DisplayMemberPath = "TicketId";
        }

        private void VenueListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VenueListBox != null)
            {
                _bookingCodeLayer.SetSelectedVenue(VenueListBox.SelectedItem);
                PopulateEventFields();

                if (_bookingCodeLayer.SelectedVenue != null)
                {
                    // Retrieves selected Venue's data
                    VenueNameTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Name;
                    VenueCityTextBoxA.Text = _bookingCodeLayer.SelectedVenue.City;
                    VenueEmailTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Email;
                    VenuePhoneTextBoxA.Text = _bookingCodeLayer.SelectedVenue.Phone;
                }

                // Clear error box when selecting
                VenueErrorTextboxA.Text = "";
                VenueErrorTextboxB.Text = "";

                // Clears Event fileds when changed
                EventNameTextBoxA.Text = "";
                EventGenreComboBoxA.SelectedItem = "";
                EventMainActTextBoxA.Text = "";
                EventSupportingActTextBoxA.Text = "";
                EventDescriptionTextBoxA.Text = "";
                EventDateCalendarA.SelectedDate = DateTime.Now;
                EventStartTimeComboBoxA.SelectedItem = "";
                EventEndTimeComboBoxA.SelectedItem = "";
                EventErrorTextboxA.Text = "";
                EventErrorTextboxB.Text = "";
            }
            //else
            //    _bookingCodeLayer.SelectedVenue = null;
        }

        private void EventListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventListBox != null)
            {
                _bookingCodeLayer.SetSelectedEvent(EventListBox.SelectedItem);
                PopulateTicketFields();

                if (_bookingCodeLayer.SelectedEvent != null)
                {
                    // Retrieves selected Venue's data
                    EventNameTextBoxA.Text = _bookingCodeLayer.SelectedEvent.Event_Name;
                    EventGenreComboBoxA.SelectedItem = _bookingCodeLayer.SelectedEvent.Genre;
                    EventMainActTextBoxA.Text = _bookingCodeLayer.SelectedEvent.Main_Act;
                    EventSupportingActTextBoxA.Text = _bookingCodeLayer.SelectedEvent.Supporting_Act;
                    EventDescriptionTextBoxA.Text = _bookingCodeLayer.SelectedEvent.Description;
                    EventDateCalendarA.SelectedDate = _bookingCodeLayer.SelectedEvent.Date;
                    EventStartTimeComboBoxA.SelectedItem = _bookingCodeLayer.SelectedEvent.Start_Time;
                    EventEndTimeComboBoxA.SelectedItem = _bookingCodeLayer.SelectedEvent.End_Time;
                }
                // Clear error box when selecting
                EventErrorTextboxA.Text = "";
                EventErrorTextboxB.Text = "";
            }
            //else
                //_bookingCodeLayer.SelectedEvent = null;
        }

        private void TicketListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TicketListBox != null)
            {
                _bookingCodeLayer.SetSelectedTicket(TicketListBox.SelectedItem);

                if (_bookingCodeLayer.SelectedTicket != null)
                {
                    // Retrieves selected Ticket's data
                    TicketFirstNameTextBox.Text = _bookingCodeLayer.SelectedTicket.First_Name;
                    TicketLastNameTextBox.Text = _bookingCodeLayer.SelectedTicket.Last_Name;
                    TicketEmailTextBox.Text = _bookingCodeLayer.SelectedTicket.Email;
                    TicketPhoneTextBox.Text = _bookingCodeLayer.SelectedTicket.Phone;
                }
                // Clear error box when selecting
                TicketErrorTextbox.Text = "";

            }
            //else
            //    _bookingCodeLayer.SelectedTicket = null;
        }

        // VENUE METHODS
        private void VenueUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(VenueUpdateButton.Content == "Yes")
            {
                if(VenueErrorTextboxA.Text.Contains("Remove"))
                {
                    // Comfirmation Remove User Button functions
                    VenueUpdateButton.Content = "Update";
                    VenueRemoveButton.Content = "Remove";
                    VenueNameTextBoxA.Text = "";
                    VenueCityTextBoxA.Text = "";
                    VenueEmailTextBoxA.Text = "";
                    VenuePhoneTextBoxA.Text = "";
                    _bookingCodeLayer.RemoveVenue();

                    _bookingCodeLayer.SelectedVenue = null;
                    VenueListBox.SelectedItem = null;

                    // Resume acces to other buttons after confirmation
                    _venueRemoveConfirm = false;

                    UnrestrictAfterRemoveDecision();
                    PopulateVenueFields();
                    EventListBox.ItemsSource = null;
                }

                if (VenueErrorTextboxA.Text.Contains("Update"))
                {
                    var name = VenueNameTextBoxA.Text;
                    var city = VenueCityTextBoxA.Text;
                    var email = VenueEmailTextBoxA.Text;
                    var phone = VenuePhoneTextBoxA.Text;

                    _bookingCodeLayer.UpdateVenueData(name, city, email, phone);

                    _bookingCodeLayer.SelectedVenue = null;

                    VenueUpdateButton.Content = "Update";
                    VenueRemoveButton.Content = "Remove";
                    VenueNameTextBoxA.Text = "";
                    VenueCityTextBoxA.Text = "";
                    VenueEmailTextBoxA.Text = "";
                    VenuePhoneTextBoxA.Text = "";

                    _bookingCodeLayer.SelectedVenue = null;
                    VenueListBox.SelectedItem = null;

                    UnrestrictAfterRemoveDecision();
                    PopulateVenueFields();
                }
            }
            else
            {
                if (_bookingCodeLayer.SelectedVenue == null)
                    VenueErrorTextboxA.Text = "Please select a venue to update";
                else
                {
                    VenueErrorTextboxA.Text = $"Update {_bookingCodeLayer.SelectedVenue.Name}?";
                    _venueUpdateConfirm = true;
                }

                // Changes buttons and functions for update confirmation
                if (_venueUpdateConfirm == true)
                {
                    VenueUpdateButton.Content = "Yes";
                    VenueRemoveButton.Content = "No";

                    // Restrictions when confirming removal
                    RestrictWhenRemoving();
                }
            }
        }

        private void VenueRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (VenueRemoveButton.Content == "No")
            {
                // Second Button functions - Cancelling removal
                VenueErrorTextboxA.Text = "";
                VenueUpdateButton.Content = "Update";
                VenueRemoveButton.Content = "Remove";

                // Resume acces to other buttons after confirmation
                _venueRemoveConfirm = false;
                UnrestrictAfterRemoveDecision();
            }
            else
            {
                // Normal Remove Venue Button functions
                if (_bookingCodeLayer.SelectedVenue == null)
                    VenueErrorTextboxA.Text = "Please select a venue to remove";
                else
                {
                    VenueErrorTextboxA.Text = $"Remove {_bookingCodeLayer.SelectedVenue.Name}?";
                    _venueRemoveConfirm = true;
                }

                // Changes buttons and functions for removal confirmation
                if (_venueRemoveConfirm == true)
                {
                    VenueUpdateButton.Content = "Yes";
                    VenueRemoveButton.Content = "No";

                    // Restrictions when confirming removal
                    RestrictWhenRemoving();
                }
            }
        }

        private void VenueAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (VenueNameTextBoxB.Text == "")
                VenueErrorTextboxB.Text = "The Venue must have a name";
            else
            {
                var name = VenueNameTextBoxB.Text;
                var city = VenueCityTextBoxB.Text;
                var email = VenueEmailTextBoxB.Text;
                var phone = VenuePhoneTextBoxB.Text;

                _bookingCodeLayer.AddVenue(name, city, email, phone);
                PopulateVenueFields();

                VenueNameTextBoxB.Text = "";
                VenueCityTextBoxB.Text = "";
                VenueEmailTextBoxB.Text = "";
                VenuePhoneTextBoxB.Text = "";
                VenueErrorTextboxB.Text = "";

            }
        }

        private void VenueClearButton_Click(object sender, RoutedEventArgs e)
        {
            VenueNameTextBoxB.Text = "";
            VenueCityTextBoxB.Text = "";
            VenueEmailTextBoxB.Text = "";
            VenuePhoneTextBoxB.Text = "";
            VenueErrorTextboxB.Text = "";
        }

        // EVENT METHODS
        private void EventUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventUpdateButton.Content == "Yes")
            {
                if (EventErrorTextboxA.Text.Contains("Remove"))
                {
                    // Comfirmation Remove User Button functions
                    EventUpdateButton.Content = "Update";
                    EventRemoveButton.Content = "Remove";

                    // Clears Event fileds when removed
                    EventNameTextBoxA.Text = "";
                    EventGenreComboBoxA.SelectedItem = "";
                    EventMainActTextBoxA.Text = "";
                    EventSupportingActTextBoxA.Text = "";
                    EventDescriptionTextBoxA.Text = "";
                    EventDateCalendarA.SelectedDate = DateTime.Now;
                    EventStartTimeComboBoxA.SelectedItem = "";
                    EventEndTimeComboBoxA.SelectedItem = "";
                    EventErrorTextboxA.Text = "";
                    _bookingCodeLayer.RemoveEvent();

                    _bookingCodeLayer.SelectedEvent = null;
                    EventListBox.SelectedItem = null;

                    // Resume acces to other buttons after confirmation
                    _eventRemoveConfirm = false;

                    UnrestrictAfterRemoveDecision();
                    PopulateEventFields();
                }

                if (EventErrorTextboxA.Text.Contains("Update"))
                {
                    var name = EventNameTextBoxA.Text;
                    var genre = EventGenreComboBoxA.SelectedItem.ToString();
                    var mainAct = EventMainActTextBoxA.Text;
                    var supportingAct = EventSupportingActTextBoxA.Text;
                    var description = EventDescriptionTextBoxA.Text;
                    var date = (DateTime)EventDateCalendarA.SelectedDate;
                    var startTime = EventStartTimeComboBoxA.SelectedItem.ToString();
                    var endTime = EventEndTimeComboBoxA.SelectedItem.ToString();

                    _bookingCodeLayer.UpdateEventData(name, mainAct, supportingAct, genre, description, date, startTime, endTime, 0);

                    _bookingCodeLayer.SelectedEvent = null;

                    EventUpdateButton.Content = "Update";
                    EventRemoveButton.Content = "Remove";

                    // Clears Event fileds when removed
                    EventNameTextBoxA.Text = "";
                    EventGenreComboBoxA.SelectedItem = "";
                    EventMainActTextBoxA.Text = "";
                    EventSupportingActTextBoxA.Text = "";
                    EventDescriptionTextBoxA.Text = "";
                    EventDateCalendarA.SelectedDate = DateTime.Now;
                    EventStartTimeComboBoxA.SelectedItem = "";
                    EventEndTimeComboBoxA.SelectedItem = "";
                    EventErrorTextboxA.Text = "";

                    _bookingCodeLayer.SelectedEvent = null;
                    EventListBox.SelectedItem = null;

                    UnrestrictAfterRemoveDecision();
                    PopulateEventFields();
                }
            }
            else
            {
                if (_bookingCodeLayer.SelectedEvent == null)
                    EventErrorTextboxA.Text = "Please select an event to update";
                else
                {
                    EventErrorTextboxA.Text = $"Update {_bookingCodeLayer.SelectedEvent.Event_Name}?";
                    _eventUpdateConfirm = true;
                }

                // Changes buttons and functions for update confirmation
                if (_eventUpdateConfirm == true)
                {
                    EventUpdateButton.Content = "Yes";
                    EventRemoveButton.Content = "No";

                    // Restrictions when confirming removal
                    RestrictWhenRemoving();
                }
            }
        }

        private void EventRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventRemoveButton.Content == "No")
            {
                // Second Button functions - Cancelling removal
                EventErrorTextboxA.Text = "";
                EventUpdateButton.Content = "Update";
                EventRemoveButton.Content = "Remove";

                // Resume acces to other buttons after confirmation
                _eventRemoveConfirm = false;
                UnrestrictAfterRemoveDecision();
            }
            else
            {
                // Normal Remove Event Button functions
                if (_bookingCodeLayer.SelectedEvent == null)
                    EventErrorTextboxA.Text = "Please select an Event to remove";
                else
                {
                    EventErrorTextboxA.Text = $"Remove {_bookingCodeLayer.SelectedEvent.Event_Name}?";
                    _eventRemoveConfirm = true;
                }

                // Changes buttons and functions for removal confirmation
                if (_eventRemoveConfirm == true)
                {
                    EventUpdateButton.Content = "Yes";
                    EventRemoveButton.Content = "No";

                    // Restrictions when confirming removal
                    RestrictWhenRemoving();
                }
            }
        }

        private void EventAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventNameTextBoxB.Text == "")
                EventErrorTextboxB.Text = "The Event must have a name";
            else
            {
                var name = EventNameTextBoxB.Text;
                var genre = EventGenreComboBoxB.SelectedItem.ToString();
                var mainAct = EventMainActTextBoxB.Text;
                var supportingAct = EventSupportingActTextBoxB.Text;
                var description = EventDescriptionTextBoxB.Text;
                var date = (DateTime)EventDateCalendarB.SelectedDate;
                var startTime = EventStartTimeComboBoxB.SelectedItem.ToString();
                var endTime = EventEndTimeComboBoxB.SelectedItem.ToString();

                _bookingCodeLayer.AddEvent(name, mainAct, genre, description, date, startTime, endTime, 0, supportingAct);
                PopulateEventFields();

                EventNameTextBoxB.Text = "";
                EventGenreComboBoxB.SelectedItem = "";
                EventMainActTextBoxB.Text = "";
                EventSupportingActTextBoxB.Text = "";
                EventDescriptionTextBoxB.Text = "";
                EventDateCalendarB.SelectedDate = DateTime.Now;
                EventStartTimeComboBoxB.SelectedItem = "";
                EventEndTimeComboBoxB.SelectedItem = "";
                EventErrorTextboxB.Text = "";
            }
        }

        private void EventClearButton_Click(object sender, RoutedEventArgs e)
        {
            EventNameTextBoxB.Text = "";
            EventGenreComboBoxB.SelectedItem = "";
            EventMainActTextBoxB.Text = "";
            EventSupportingActTextBoxB.Text = "";
            EventDescriptionTextBoxB.Text = "";
            EventDateCalendarB.SelectedDate = DateTime.Now;
            EventStartTimeComboBoxB.SelectedItem = "";
            EventEndTimeComboBoxB.SelectedItem = "";
            EventErrorTextboxB.Text = "";
        }

        // Ticket Methods
        private void TicketUpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TicketRemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
