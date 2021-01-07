using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Concert_Booking_Project.Services;

namespace Concert_Booking_Project
{
    public class VenueCRUDCode
    {
        private IVenueService _venueService;
        public VenueCRUDCode(BookingContext context)
        {
            _venueService = new VenueService(context);
        }
        public VenueCRUDCode()
        {
            _venueService = new VenueService(new BookingContext());
        }

        // Display relevant data based on what is selected
        public Venue SelectedVenue { get; set; }

        // Read functionality
        public List<Venue> RetrieveAllVenues()
        {
            return _venueService.RetrieveAllVenues();
        }

        public void SetSelectedVenue(object selectedVenue)
        {
            SelectedVenue = (Venue)selectedVenue;
        }

        // Add functionality
        public void AddVenue(string name, string city, string email, string phone)
        {
            _venueService.AddVenue(name, city, email, phone);
        }

        // Remove functionality
        public void RemoveVenue(int venueId) // Removes related Event and Ticket data
        {
            _venueService.RemoveVenue(venueId);
        }

        // Update functionality
        public void UpdateVenueData(int venueId, string name, string city, string email, string phone)
        {
            SelectedVenue.Name = name;
            SelectedVenue.City = city;
            SelectedVenue.Email = email;
            SelectedVenue.Phone = phone;
            _venueService.UpdateVenueData(venueId, name, city, email, phone);
        }
    }
}
