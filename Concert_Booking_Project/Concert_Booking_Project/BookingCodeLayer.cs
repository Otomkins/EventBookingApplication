using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concert_Booking_Project
{
    public class BookingCodeLayer
    {
        // Display relevant data based on what is selected
        public Venue SelectedVenue { get; set; }
        public Event SelectedEvent { get; set; }
        public Ticket SelectedTicket { get; set; }

        // Read functionality
        public List<Venue> RetrieveAllVenues()
        {
            using var bc = new BookingContext();
            return bc.Venues.ToList();
        }
        public void SetSelectedVenue(object selectedVenue)
        {
            SelectedVenue = (Venue)selectedVenue;
        }

        public List<Event> RetrieveAllEvents()
        {
            using var bc = new BookingContext();
            var q = bc.Events.Where(v => v.Venue.VenueId == SelectedVenue.VenueId);
            return q.ToList();
        }
        public void SetSelectedEvent(object selectedEvent)
        {
            SelectedEvent = (Event)selectedEvent;
        }

        public List<Ticket> RetrieveAllTickets()
        {
            using var bc = new BookingContext();
            var q = bc.Tickets.Where(e => e.Event.EventId == SelectedEvent.EventId);
            return q.ToList();
        }
        public void SetSelectedTickets(object selectedTicket)
        {
            SelectedTicket = (Ticket)selectedTicket;
        }

        // Add functionality
        public void AddVenue(string name, string city, string email, string phone)
        {
            using var bc = new BookingContext();
            var newVenue = new Venue { Name = name, City = city, Email = email, Phone = phone };
            bc.Venues.Add(newVenue);
            bc.SaveChanges();
        }

        public void AddEvent(string eventName, string mainAct, string genre, string description, DateTime date,
            string startTime, string endTime, int capacity, string supportingAct = "N/A")
        {
            using var bc = new BookingContext();
            var q = bc.Venues.Where(e => e.VenueId == SelectedVenue.VenueId);
            foreach (var item in q)
            {
                var newEvent = new Event
                {
                    Venue = item,
                    Event_Name = eventName,
                    Main_Act = mainAct,
                    Supporting_Act = supportingAct,
                    Genre = genre,
                    Description = description,
                    Date = date,
                    Start_Time = startTime,
                    End_Time = endTime,
                    Capacity = capacity
                };
                bc.Events.Add(newEvent);
            }
            bc.SaveChanges();
        }
    }
}
