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
        public void RetrieveEventsTest()
        {
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            _context.Venues.Add(newVenue);
            _context.SaveChanges();
            _venueCRUD.SelectedVenue = newVenue;

            // Checks size of DB before adding Events
            var eventList = _eventCRUD.RetrieveAllEvents(_venueCRUD.SelectedVenue.VenueId);

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

            // Retrieve all Events
            var eventList2 = _eventCRUD.RetrieveAllEvents(_venueCRUD.SelectedVenue.VenueId);

            // Check size of list on return against created Events
            Assert.AreEqual(eventList2.Count == eventList.Count + 1, true);
        }

        [Test]
        public void AddingEventTest()
        {
            // Add Venue - Cannot have Event without Venue variable
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            _context.Venues.Add(newVenue);
            _context.SaveChanges();

            // Add Event
            _eventCRUD.AddEvent(newVenue.VenueId, "Nme", "Act", "Gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00");

            // Test if Event has been added as expected
            var newEvent = _context.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(newEvent.Main_Act, "Act");
            Assert.AreEqual(newEvent.Date, new DateTime(2020, 11, 05));
            Assert.AreEqual(newEvent.Venue.Name, "Sample1234");
        }

        [Test]
        public void RemovingEventsAndRelatedTicketsTest()
        {
            // Add Event
            var newEvent = new Event()
            {
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
            _eventCRUD.SelectedEvent = newEvent;

            // Add Ticket
            _context.Tickets.Add(new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" });
            _context.SaveChanges();

            // Remove Event
            _eventCRUD.RemoveEvent(_eventCRUD.SelectedEvent.EventId);

            // Test to see if removing Event also removed related Tickets
            var delEvent = _context.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            var delTicket = _context.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void RemovingOutOfDateEventsAndRelatedTicketsTest()
        {
            // Add Event
            var newEvent = new Event()
            {
                Event_Name = "Sample1234",
                Main_Act = "Act",
                Genre = "Gnr",
                Description = "Desc",
                Date = new DateTime(1999, 11, 05),
                Start_Time = "16:00",
                End_Time = "23:00"
            };
            _context.Events.Add(newEvent);

            // Add Ticket
            _context.Tickets.Add(new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" });
            _context.SaveChanges();

            // Remove Events before given date
            _eventCRUD.RemoveOutOfDateEventsAndTickets(new DateTime(1999, 01, 01));
            // Will not delete Event as its later than given date
            var delEvent = _context.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent is null, false);

            _eventCRUD.RemoveOutOfDateEventsAndTickets(new DateTime(2000, 01, 01));

            // Test to see if removing Event also removed related Tickets
            var delEvent2 = _context.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
            var delTicket = _context.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delEvent2 is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void UpdateEventsTest()
        {
            // Add Event
            var newEvent = new Event()
            {
                Event_Name = "test-test",
                Main_Act = "test",
                Genre = "test",
                Description = "test",
                Date = new DateTime(1010, 01, 01),
                Start_Time = "test",
                End_Time = "test"
            };
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            _eventCRUD.SelectedEvent = newEvent;

            // Venue data to change to
            _eventCRUD.UpdateEventData(_eventCRUD.SelectedEvent.EventId, "Sample1234", "Act", "SpAct", "gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00");
            _context.SaveChanges();

            // Test to compare DB with Event changes
            var eAfter = _context.Events.Where(e => e.EventId == _eventCRUD.SelectedEvent.EventId).FirstOrDefault();
            Assert.AreEqual(eAfter.Event_Name == "Sample1234", true);
        }
    }
}
