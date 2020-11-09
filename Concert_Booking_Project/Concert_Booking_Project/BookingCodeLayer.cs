using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Venue SelectedVenueUpdate { get; set; }

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
            var q = bc.Tickets.Where(e => e.Event.Venue.VenueId == SelectedVenue.VenueId);
            return q.ToList();
        }
        public void SetSelectedTicket(object selectedTicket)
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
            string startTime, string endTime, int capacity = 0, string supportingAct = "N/A")
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
            var q3 = bc.Tickets.Where(t => t.Event.Venue.VenueId == SelectedVenue.VenueId);
            foreach (var item in q3)
                bc.Remove(item);
            bc.SaveChanges();

            var q2 = bc.Events.Where(e => e.Venue.VenueId == SelectedVenue.VenueId);
            foreach (var item in q2)
                bc.Remove(item);
            bc.SaveChanges();

            var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId);
            foreach (var item in q)
                bc.Remove(item);
            bc.SaveChanges();
        }

        public void RemoveEvent() // Removes related Ticket data
        {
            using var bc = new BookingContext();
            var q2 = bc.Tickets.Where(t => t.Event.EventId == SelectedEvent.EventId);
            foreach (var item in q2)
                bc.Remove(item);
            bc.SaveChanges();

            var q = bc.Events.Where(e => e.EventId == SelectedEvent.EventId);
            foreach (var item in q)
                bc.Remove(item);
            bc.SaveChanges();
        }

        public void RemoveTicket()
        {
            using var bc = new BookingContext();
            var q = bc.Tickets.Where(t => t.TicketId == SelectedTicket.TicketId);
            foreach (var item in q)
                bc.Remove(item);
            bc.SaveChanges();
        }

        // Added functionality to specify date where data is removed up to
        public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
        {
            using var bc = new BookingContext();
            var q2 = bc.Tickets.Where(t => t.Event.Date < deleteUpTo);
            foreach (var item in q2)
                bc.Remove(item);
            bc.SaveChanges();

            var q = bc.Events.Where(e => e.Date < deleteUpTo);
            foreach (var item in q)
                bc.Remove(item);
            bc.SaveChanges();
        }

        // Update functionality
        public void UpdateVenueData(string name, string city, string email, string phone)
        {
            using var bc = new BookingContext();
            SelectedVenue.Name = name;
            SelectedVenue.City = city;
            SelectedVenue.Email = email;
            SelectedVenue.Phone = phone;
            bc.SaveChanges();

            var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId).FirstOrDefault();
            q.Name = name;
            q.City = city;
            q.Email = email;
            q.Phone = phone;
            bc.SaveChanges();
        }

        public void UpdateEventData(string eventName, string mainAct, string supportingAct, string genre, string description, DateTime date,
            string startTime, string endTime, int capacity)
        {
            using var bc = new BookingContext();
            SelectedEvent.Event_Name = eventName;
            SelectedEvent.Main_Act = mainAct;
            SelectedEvent.Supporting_Act = supportingAct;
            SelectedEvent.Genre = genre;
            SelectedEvent.Description = description;
            SelectedEvent.Date = date;
            SelectedEvent.Start_Time = startTime;
            SelectedEvent.End_Time = endTime;
            SelectedEvent.Capacity = capacity;
            bc.SaveChanges();

            //var q = bc.Events.Where(v => v.Venue.VenueId == SelectedVenue.VenueId).FirstOrDefault();
            var q = bc.Events.Where(v => v.EventId == SelectedEvent.EventId).FirstOrDefault();
            q.Event_Name = eventName;
            q.Main_Act = mainAct;
            q.Supporting_Act = supportingAct;
            q.Genre = genre;
            q.Description = description;
            q.Date = date;
            q.Start_Time = startTime;
            q.End_Time = endTime;
            q.Capacity = capacity;
            bc.SaveChanges();
        }

        public void UpdateTicketData(string firstName, string lastName, string email, string phone)
        {
            using var bc = new BookingContext();
            SelectedTicket.First_Name = firstName;
            SelectedTicket.Last_Name = lastName;
            SelectedTicket.Email = email;
            SelectedTicket.Phone = phone;
            bc.SaveChanges();
        }




        public void AddTicket(string firstName, string lastName, string email, string phone)
        {
            using var bc = new BookingContext();
            var q = bc.Events.Where(e => e.EventId == SelectedEvent.EventId);
            foreach (var item in q)
            {
                var newTicket = new Ticket
                {
                    Event = item,
                    First_Name = firstName,
                    Last_Name = lastName,
                    Email = email,
                    Phone = phone
                   
                };
                bc.Tickets.Add(newTicket);
            }
            bc.SaveChanges();
        }
    }
}
