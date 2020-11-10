using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Concert_Booking_Project;

namespace Concert_Booking_Project_Tests
{
    public class Tests
    {
        BookingCodeLayer _bookingCodeLayer = new BookingCodeLayer();

        [SetUp]
        public void Setup()
        {
            using var bc = new BookingContext();
            var selectedTicket = bc.Tickets.Where(t => t.First_Name == "Sample1234");
            bc.Tickets.RemoveRange(selectedTicket);
            //var selectedTicket2 = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234");
            //bc.Tickets.RemoveRange(selectedTicket2);
            //var selectedTicket3 = bc.Tickets.Where(t => t.Event.Venue.Name == "Sample1234");
            //bc.Tickets.RemoveRange(selectedTicket3);
            bc.SaveChanges();

            var selectedEvent = bc.Events.Where(e => e.Event_Name == "Sample1234");
            bc.Events.RemoveRange(selectedEvent);
            var selectedEvent2 = bc.Events.Where(e => e.Venue.Name == "Sample1234");
            bc.Events.RemoveRange(selectedEvent2);
            bc.SaveChanges();

            var selectedVenue = bc.Venues.Where(v => v.Name == "Sample1234");
            bc.Venues.RemoveRange(selectedVenue);
            bc.SaveChanges();

            _bookingCodeLayer.SelectedVenue = null;
            _bookingCodeLayer.SelectedEvent = null;
            _bookingCodeLayer.SelectedTicket = null;
        }

        [TearDown]
        public void TearDown()
        {
            using var bc = new BookingContext();
            var selectedTicket = bc.Tickets.Where(t => t.First_Name == "Sample1234");
            bc.Tickets.RemoveRange(selectedTicket);
            //var selectedTicket2 = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234");
            //bc.Tickets.RemoveRange(selectedTicket2);
            //var selectedTicket3 = bc.Tickets.Where(t => t.Event.Venue.Name == "Sample1234");
            //bc.Tickets.RemoveRange(selectedTicket3);
            bc.SaveChanges();

            var selectedEvent = bc.Events.Where(e => e.Event_Name == "Sample1234");
            bc.Events.RemoveRange(selectedEvent);
            var selectedEvent2 = bc.Events.Where(e => e.Venue.Name == "Sample1234");
            bc.Events.RemoveRange(selectedEvent2);
            bc.SaveChanges();

            var selectedVenue = bc.Venues.Where(v => v.Name == "Sample1234");
            bc.Venues.RemoveRange(selectedVenue);
            bc.SaveChanges();

            _bookingCodeLayer.SelectedVenue = null;
            _bookingCodeLayer.SelectedEvent = null;
            _bookingCodeLayer.SelectedTicket = null;
        }

        [Test]
        public void RetrieveVenuesTest()
        {
            using var bc = new BookingContext();
            // Checks size of DB before adding Venues
            var venueList = _bookingCodeLayer.RetrieveAllVenues();

            // Add Venues
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            var newVenue2 = new Venue() { Name = "Sample1234", City = "Cty2", Email = "Eml2", Phone = "Phn2" };
            bc.Venues.Add(newVenue);
            bc.Venues.Add(newVenue2);
            bc.SaveChanges();

            // Retrieve all Venues
            var venueList2 = _bookingCodeLayer.RetrieveAllVenues();

            // Check size of list on return against created Venues
            Assert.AreEqual(venueList2.Count == venueList.Count + 2, true);
        }

        [Test]
        public void RetrieveEventsTest()
        {
            using var bc = new BookingContext();
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            bc.Venues.Add(newVenue);
            _bookingCodeLayer.SelectedVenue = newVenue;

            // Checks size of DB before adding Events
            var eventList = _bookingCodeLayer.RetrieveAllEvents();

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
                End_Time = "23:00",
                Capacity = 50
            };
            bc.Events.Add(newEvent);
            bc.SaveChanges();

            // Retrieve all Events
            var eventList2 = _bookingCodeLayer.RetrieveAllEvents();

            // Check size of list on return against created Events
            Assert.AreEqual(eventList2.Count == eventList.Count + 1, true);
        }

        [Test]
        public void RetrieveTicketsTest()
        {
            using var bc = new BookingContext();
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            bc.Venues.Add(newVenue);

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
                End_Time = "23:00",
                Capacity = 50
            };
            bc.Events.Add(newEvent);
            _bookingCodeLayer.SelectedVenue = newVenue;
            _bookingCodeLayer.SelectedEvent = newEvent;

            // Checks size of DB before adding Events
            var ticketList = _bookingCodeLayer.RetrieveAllTickets();
            bc.SaveChanges();

            // Add Ticket
            var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();

            // Retrieve all Events
            var ticketList2 = _bookingCodeLayer.RetrieveAllTickets();

            // Check size of list on return against created Events
            Assert.AreEqual(ticketList2.Count == ticketList.Count + 1, true);
        }

        [Test]
        public void AddingVenueTest()
        {
            using var bc = new BookingContext();
            // Add Venue
            _bookingCodeLayer.AddVenue("Sample1234", "Cty", "Eml", "Phn");

            // Test if Venue has been added as expected
            var newVenue = bc.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(newVenue.City, "Cty");
            Assert.AreEqual(newVenue.Phone, "Phn");
        }

        [Test]
        public void AddingEventTest()
        {
            using var bc = new BookingContext();
            // Add Venue - Cannot have Event without Venue variable
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            bc.Venues.Add(newVenue);
            bc.SaveChanges();

            // Add Event
            _bookingCodeLayer.SelectedVenue = newVenue;
            _bookingCodeLayer.AddEvent("Nme", "Act", "Gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00", 50);

            // Test if Event has been added as expected
            var newEvent = bc.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(newEvent.Main_Act, "Act");
            Assert.AreEqual(newEvent.Date, new DateTime(2020, 11, 05));
            Assert.AreEqual(newEvent.Venue.Name, "Sample1234");
        }

        [Test]
        public void RemovingVenueAndRelatedEventsTest()
        {
            using var bc = new BookingContext();
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            bc.Venues.Add(newVenue);
            _bookingCodeLayer.SelectedVenue = newVenue;

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
                End_Time = "23:00",
                Capacity = 50
            };
            bc.Events.Add(newEvent);

            // Add Ticket
            var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();

            // Remove Event
            _bookingCodeLayer.RemoveVenue();

            // Test to see if removing Venue also removed related Events and Tickets
            var delVenue = bc.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
            var delEvent = bc.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
            var delTicket = bc.Tickets.Where(t => t.Event.Venue.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delVenue is null, true);
            Assert.AreEqual(delEvent is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void RemovingEventsAndRelatedTicketsTest()
        {
            using var bc = new BookingContext();
            // Add Event
            var newEvent = new Event()
            {
                Event_Name = "Sample1234",
                Main_Act = "Act",
                Genre = "Gnr",
                Description = "Desc",
                Date = new DateTime(2020, 11, 05),
                Start_Time = "16:00",
                End_Time = "23:00",
                Capacity = 50
            };
            bc.Events.Add(newEvent);
            _bookingCodeLayer.SelectedEvent = newEvent;

            // Add Ticket
            var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();

            // Remove Event
            _bookingCodeLayer.RemoveEvent();

            // Test to see if removing Event also removed related Tickets
            var delEvent = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            var delTicket = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void RemovingTicketsTest()
        {
            // Add Ticket
            using var bc = new BookingContext();
            var newTicket = new Ticket() { First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();
            _bookingCodeLayer.SelectedTicket = newTicket;

            // Remove Ticket
            _bookingCodeLayer.RemoveTicket();
            var ticket = bc.Tickets.Where(t => t.First_Name == "Sample1234").FirstOrDefault();
            // Test if Ticket has been removed
            Assert.AreEqual(ticket is null, true);
        }

        [Test]
        public void RemovingOutOfDateEventsAndRelatedTicketsTest()
        {
            using var bc = new BookingContext();
            // Add Event
            var newEvent = new Event()
            {
                Event_Name = "Sample1234",
                Main_Act = "Act",
                Genre = "Gnr",
                Description = "Desc",
                Date = new DateTime(1999, 11, 05),
                Start_Time = "16:00",
                End_Time = "23:00",
                Capacity = 50
            };
            bc.Events.Add(newEvent);

            // Add Ticket
            var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();

            // Remove Events before given date
            _bookingCodeLayer.RemoveOutOfDateEventsAndTickets(new DateTime(1999, 01, 01));
            // Will not delete Event as its later than given date
            var delEvent = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent is null, false);

            _bookingCodeLayer.RemoveOutOfDateEventsAndTickets(new DateTime(2000, 01, 01));

            // Test to see if removing Event also removed related Tickets
            var delEvent2 = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            var delTicket = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent2 is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void UpdateVenueTest()
        {
            using var bc = new BookingContext();
            // Add Venue
            var newVenue = new Venue() { Name = "test-test", City = "test", Email = "test", Phone = "test" };
            bc.Venues.Add(newVenue);
            bc.SaveChanges();
            _bookingCodeLayer.SelectedVenue = newVenue;

            // Venue data to change to
            _bookingCodeLayer.UpdateVenueData("Sample1234", "Cty", "Phn", "Eml");
            bc.SaveChanges();

            // Test to compare DB with Venue changes
            var vAfter = bc.Venues.Where(v => v.VenueId == _bookingCodeLayer.SelectedVenue.VenueId).FirstOrDefault();
            Assert.AreEqual(vAfter.Name == "Sample1234", true);
        }

        [Test]
        public void UpdateEventsTest()
        {
            using var bc = new BookingContext();
            // Add Event
            var newEvent = new Event()
            {
                Event_Name = "test-test",
                Main_Act = "test",
                Genre = "test",
                Description = "test",
                Date = new DateTime(1010, 01, 01),
                Start_Time = "test",
                End_Time = "test",
                Capacity = 00
            };
            bc.Events.Add(newEvent);
            bc.SaveChanges();
            _bookingCodeLayer.SelectedEvent = newEvent;

            // Venue data to change to
            _bookingCodeLayer.UpdateEventData("Sample1234", "Act", "SpAct", "gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00", 50);
            bc.SaveChanges();

            // Test to compare DB with Event changes
            var eAfter = bc.Events.Where(e => e.EventId == _bookingCodeLayer.SelectedEvent.EventId).FirstOrDefault();
            Assert.AreEqual(eAfter.Event_Name == "Sample1234", true);
        }

        [Test]
        public void UpdateTicketTest()
        {
            using var bc = new BookingContext();
            // Add Ticket
            var newTicket = new Ticket() { First_Name = "test-test", Last_Name = "test", Email = "test", Phone = "test" };
            bc.Tickets.Add(newTicket);
            bc.SaveChanges();
            _bookingCodeLayer.SelectedTicket = newTicket;

            // Ticket data to change to
            _bookingCodeLayer.UpdateTicketData("Sample1234", "Lnm", "Phn", "Eml");
            bc.SaveChanges();

            // Test to compare DB with Ticket changes
            var tAfter = bc.Tickets.Where(t => t.TicketId == _bookingCodeLayer.SelectedTicket.TicketId).FirstOrDefault();
            Assert.AreEqual(tAfter.First_Name == "Sample1234", true);
        }

    }
}