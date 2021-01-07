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
        VenueCRUDCode _venueCRUD;
        EventCRUDCode _eventCRUD;
        TicketCRUDCode _ticketCRUD;

        BookingContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<BookingContext>().UseInMemoryDatabase(databaseName: "BookingVenueDB").Options;
            _context = new BookingContext(options);
            _venueCRUD = new VenueCRUDCode(_context);
            _eventCRUD = new EventCRUDCode(_context);
            _ticketCRUD = new TicketCRUDCode(_context);
        }

        [SetUp]
        public void Setup()
        {
            _context.Tickets.RemoveRange(_context.Tickets.Where(t => t.First_Name == "Sample1234"));
            _context.Events.RemoveRange(_context.Events.Where(e => e.Event_Name == "Sample1234"));
            _context.Events.RemoveRange(_context.Events.Where(e => e.Venue.Name == "Sample1234"));
            _context.Venues.RemoveRange(_context.Venues.Where(v => v.Name == "Sample1234"));
            _context.SaveChanges();

            _venueCRUD.SelectedVenue = null;
            _eventCRUD.SelectedEvent = null;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Tickets.RemoveRange(_context.Tickets.Where(t => t.First_Name == "Sample1234"));
            _context.Events.RemoveRange(_context.Events.Where(e => e.Event_Name == "Sample1234"));
            _context.Events.RemoveRange(_context.Events.Where(e => e.Venue.Name == "Sample1234"));
            _context.Venues.RemoveRange(_context.Venues.Where(v => v.Name == "Sample1234"));
            _context.SaveChanges();

            _venueCRUD.SelectedVenue = null;
            _eventCRUD.SelectedEvent = null;
        }

        [Test]
        public void RetrieveVenuesTest()
        {
            // Checks size of DB before adding Venues
            var venueList = _venueCRUD.RetrieveAllVenues();

            // Add Venues
            _context.Venues.Add(new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" });
            _context.Venues.Add(new Venue() { Name = "Sample1234", City = "Cty2", Email = "Eml2", Phone = "Phn2" });
            _context.SaveChanges();

            // Retrieve all Venues
            var venueList2 = _venueCRUD.RetrieveAllVenues();

            // Check size of list on return against created Venues
            Assert.AreEqual(venueList2.Count == venueList.Count + 2, true);
        }

        [Test]
        public void AddingVenueTest()
        {
            // Add Venue
            _venueCRUD.AddVenue("Sample1234", "Cty", "Eml", "Phn");

            // Test if Venue has been added as expected
            var newVenue = _context.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(newVenue.City, "Cty");
            Assert.AreEqual(newVenue.Phone, "Phn");
        }

        [Test]
        public void RemovingVenueAndRelatedEventsTest()
        {
            // Add Venue
            var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
            _context.Venues.Add(newVenue);
            _venueCRUD.SelectedVenue = newVenue;

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

            // Add Ticket
            _context.Tickets.Add(new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" });
            _context.SaveChanges();

            // Remove Event
            _venueCRUD.RemoveVenue(_venueCRUD.SelectedVenue.VenueId);

            // Test to see if removing Venue also removed related Events and Tickets
            var delVenue = _context.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
            var delEvent = _context.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
            var delTicket = _context.Tickets.Where(t => t.Event.Venue.Name == "Sample1234").FirstOrDefault();
            Assert.AreEqual(delVenue is null, true);
            Assert.AreEqual(delEvent is null, true);
            Assert.AreEqual(delTicket is null, true);
        }

        [Test]
        public void UpdateVenueTest()
        {
            // Add Venue
            var newVenue = new Venue() { Name = "test-test", City = "test", Email = "test", Phone = "test" };
            _context.Venues.Add(newVenue);
            _context.SaveChanges();
            _venueCRUD.SelectedVenue = newVenue;

            // Venue data to change to
            _venueCRUD.UpdateVenueData(_venueCRUD.SelectedVenue.VenueId, "Sample1234", "Cty", "Phn", "Eml");
            _context.SaveChanges();

            // Test to compare DB with Venue changes
            var vAfter = _context.Venues.Where(v => v.VenueId == newVenue.VenueId).FirstOrDefault();
            Assert.AreEqual(vAfter.Name == "Sample1234", true);
        }
    }
}