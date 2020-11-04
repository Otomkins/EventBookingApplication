using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concert_Booking_Project
{
    public class BookingCodeLayer
    {
        public Venue SelectedVenue { get; set; }
        public Event SelectedEvent { get; set; }
        public Ticket SelectedTicket { get; set; }

        public List<Venue> RetrieveAllVenues()
        {
            using (var bc = new BookingContext())
            {
                return bc.Venues.ToList();
            }
        }
        public void SetSelectedVenue(object selectedVenue)
        {
            SelectedVenue = (Venue)selectedVenue;
        }

        public List<Event> RetrieveAllEvents()
        {
            using (var bc = new BookingContext())
            {
                var q = bc.Events.Where(v => v.Venue.VenueId == SelectedVenue.VenueId);
                return q.ToList();
            }
        }
        public void SetSelectedEvent(object selectedEvent)
        {
            SelectedEvent = (Event)selectedEvent;
        }

        public List<Ticket> RetrieveAllTickets()
        {
            using (var bc = new BookingContext())
            {
                var q = bc.Tickets.Where(e => e.Event.EventId == SelectedEvent.EventId);
                return q.ToList();
            }
        }
        public void SetSelectedTickets(object selectedTicket)
        {
            SelectedTicket = (Ticket)selectedTicket;
        }
    }
}
