using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Concert_Booking_Project
{
    public class BookingContext : DbContext
    {
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Event_Booking;");
    }
    public class Venue
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
    }
    public class Event
    {
        public int EventId { get; set; }
        public Venue Venue { get; set; }
        public string Event_Name { get; set; }
        public string Main_Act { get; set; }
        public string Supporting_Act { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public int Capacity { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
    public class Ticket
    {
        public int TicketId { get; set; }
        public Event Event { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
