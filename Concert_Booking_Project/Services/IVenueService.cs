using System;
using System.Collections.Generic;
using System.Text;

namespace Concert_Booking_Project.Services
{
    public interface IVenueService
    {
        public List<Venue> RetrieveAllVenues();
        public void AddVenue(string name, string city, string email, string phone);
        public void RemoveVenue(int venueId);
        public void UpdateVenueData(int venueId, string name, string city, string email, string phone);
    }
}
