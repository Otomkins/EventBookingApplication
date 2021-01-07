using System;
using System.Collections.Generic;
using System.Text;

namespace Concert_Booking_Project.Services
{
    public interface ITicketService
    {
        public List<Ticket> RetrieveAllTickets(int eventId);
        public void AddTicket(int eventId, string firstName, string lastName, string email, string phone);
        public void RemoveTicket(int ticketId);
        public void UpdateTicketData(int ticketId, string firstName, string lastName, string email, string phone);
        public List<Ticket> FilterTicketsById(int ticketId);
    }
}
