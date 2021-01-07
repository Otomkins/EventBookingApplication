using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concert_Booking_Project.Services
{
    public class VenueService : IVenueService
    {
        private readonly BookingContext _context;

        public VenueService(BookingContext context)
        {
            _context = context;
        }

        public List<Venue> RetrieveAllVenues()
        {
            return _context.Venues.ToList();
        }

        public void AddVenue(string name, string city, string email, string phone)
        {
            //using var bc = new BookingContext();
            var newVenue = new Venue { Name = name, City = city, Email = email, Phone = phone };
            _context.Venues.Add(newVenue);
            _context.SaveChanges();
        }

        public void RemoveVenue(int venueId)
        {
            //using var bc = new BookingContext();
            var q3 = _context.Tickets.Where(t => t.Event.Venue.VenueId == venueId);
            foreach (var item in q3)
                _context.Remove(item);
            _context.SaveChanges();

            var q2 = _context.Events.Where(e => e.Venue.VenueId == venueId);
            foreach (var item in q2)
                _context.Remove(item);
            _context.SaveChanges();

            var q = _context.Venues.Where(v => v.VenueId == venueId);
            foreach (var item in q)
                _context.Remove(item);
            _context.SaveChanges();
        }

        public void UpdateVenueData(int venueId, string name, string city, string email, string phone)
        {
            var q = _context.Venues.Where(v => v.VenueId == venueId).FirstOrDefault();
            q.Name = name;
            q.City = city;
            q.Email = email;
            q.Phone = phone;
            _context.SaveChanges();
        }
    }
}
