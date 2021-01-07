using System;
using System.Collections.Generic;
using System.Linq;

namespace Concert_Booking_Project.Services
{
    public class TicketService : ITicketService
    {
        private readonly BookingContext _context;

        public TicketService(BookingContext context)
        {
            _context = context;
        }

        public List<Ticket> RetrieveAllTickets(int eventId)
        {
            return _context.Tickets.Where(t => t.Event.EventId == eventId).ToList();
        }

        public void AddTicket(int eventId, string firstName, string lastName, string email, string phone)
        {
            var q = _context.Events.Where(e => e.EventId == eventId);
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
                _context.Tickets.Add(newTicket);
            }
            _context.SaveChanges();
        }

        public void RemoveTicket(int ticketId)
        {
            var q = _context.Tickets.Where(t => t.TicketId == ticketId);
            foreach (var item in q)
                _context.Remove(item);
            _context.SaveChanges();
        }

        public void UpdateTicketData(int ticketId, string firstName, string lastName, string email, string phone)
        {
            var q = _context.Tickets.Where(t => t.TicketId == ticketId).FirstOrDefault();
            q.First_Name = firstName;
            q.Last_Name = lastName;
            q.Email = email;
            q.Phone = phone;
            _context.SaveChanges();
        }

        public List<Ticket> FilterTicketsById(int ticketId)
        {
            var q = _context.Tickets.Where(t => t.TicketId == ticketId);
            return q.ToList();
        }

    }
}
