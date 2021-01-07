using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Concert_Booking_Project;

namespace Concert_Booking_Project_Tests
{
    public partial class BookingEventCRUDTests
    {
        [Test]
        public void RetrieveTicketsTest()
        {
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            _context.Venues.Add(newVenue);
            _context.SaveChanges();

            // Add Event
            var newEvent = new Event()
            {
                Venue = newVenue,
                Event_Name = "Sample1234",
                Main_Act = "Act",
                Genre = "Gnr",
                Description = "Desc",
                Date = new DateTime(2020, 11, 05),
                Start_Time = "16:00",
                End_Time = "23:00"
            };
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            _venueCRUD.SelectedVenue = newVenue;
            _eventCRUD.SelectedEvent = newEvent;

            // Checks size of DB before adding Events
            var ticketList = _ticketCRUD.RetrieveAllTickets(_eventCRUD.SelectedEvent.EventId).ToList();
            _context.SaveChanges();

            // Add Ticket
            var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            _ticketCRUD.SelectedTicket = newTicket;

            // Retrieve all Events
            var ticketList2 = _ticketCRUD.RetrieveAllTickets(_eventCRUD.SelectedEvent.EventId).ToList();

            // Check size of list on return against created Events
            Assert.AreEqual(ticketList2.Count == ticketList.Count + 1, true);
        }

        [Test]
        public void RemovingTicketsTest()
        {
            // Add Ticket
            var newTicket = new Ticket() { First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            _ticketCRUD.SelectedTicket = newTicket;

            // Remove Ticket
            _ticketCRUD.RemoveTicket(_ticketCRUD.SelectedTicket.TicketId);
            var ticket = _context.Tickets.Where(t => t.First_Name == "Sample1234").FirstOrDefault();
            // Test if Ticket has been removed
            Assert.AreEqual(ticket is null, true);
        }

        [Test]
        public void UpdateTicketTest()
        {
            // Add Ticket
            var newTicket = new Ticket() { First_Name = "test-test", Last_Name = "test", Email = "test", Phone = "test" };
            _context.Tickets.Add(newTicket);
            _context.SaveChanges();
            _ticketCRUD.SelectedTicket = newTicket;

            // Ticket data to change to
            _ticketCRUD.UpdateTicketData(_ticketCRUD.SelectedTicket.TicketId, "Sample1234", "Lnm", "Phn", "Eml");
            _context.SaveChanges();

            // Test to compare DB with Ticket changes
            var tAfter = _context.Tickets.Where(t => t.TicketId == _ticketCRUD.SelectedTicket.TicketId).FirstOrDefault();
            Assert.AreEqual(tAfter.First_Name == "Sample1234", true);
        }

    }
}
