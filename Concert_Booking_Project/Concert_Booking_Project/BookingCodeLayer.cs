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

        // Remove functionality
        public void RemoveVenue() // Removes related Event and Ticket data
        {
            using var bc = new BookingContext();
            var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId);
            RemoveQueryFromDB(q);
            var q2 = bc.Events.Where(e => e.Venue.VenueId == SelectedVenue.VenueId);
            RemoveQueryFromDB(q2);
            var q3 = bc.Tickets.Where(t => t.Event.Venue.VenueId == SelectedVenue.VenueId);
            RemoveQueryFromDB(q3);
        }

        public void RemoveEvent() // Removes related Ticket data
        {
            using var bc = new BookingContext();
            var q = bc.Events.Where(e => e.EventId == SelectedEvent.EventId);
            RemoveQueryFromDB(q);
            var q2 = bc.Tickets.Where(t => t.Event.EventId == SelectedEvent.EventId);
            RemoveQueryFromDB(q2);
        }

        public void RemoveTicket()
        {
            using var bc = new BookingContext();
            var q = bc.Tickets.Where(t => t.TicketId == SelectedTicket.TicketId);
            RemoveQueryFromDB(q);
        }

        // Added functionality to specify date where data is removed up to
        public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
        {
            using var bc = new BookingContext();
            var q = bc.Events.Where(e => e.Date < deleteUpTo);
            RemoveQueryFromDB(q);
            var q2 = bc.Tickets.Where(t => t.Event.Date < deleteUpTo);
            RemoveQueryFromDB(q2);
        }

        // Compact method to remove a specified query from DB
        private void RemoveQueryFromDB(IQueryable q)
        {
            using var bc = new BookingContext();
            foreach (var item in q)
            {
                bc.Remove(item);
            }
            bc.SaveChanges();
        }


    }
}
