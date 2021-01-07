using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Concert_Booking_Project.Services;

namespace Concert_Booking_Project
{
    public class TicketCRUDCode
    {
        private readonly TicketService _ticketService;
        public TicketCRUDCode(BookingContext context)
        {
            _ticketService = new TicketService(context);
        }
        public TicketCRUDCode()
        {
            _ticketService = new TicketService(new BookingContext());
        }

        // Display relevant data based on what is selected
        public Ticket SelectedTicket { get; set; }

        // Read functionality
        public List<Ticket> RetrieveAllTickets(int eventId)
        {
            return _ticketService.RetrieveAllTickets(eventId);
        }
        public void SetSelectedTicket(object selectedTicket)
        {
            SelectedTicket = (Ticket)selectedTicket;
        }

        // Add functionality
        public void AddTicket(int eventId, string firstName, string lastName, string email, string phone) // Only used to create data for demonstration
        {
            _ticketService.AddTicket(eventId, firstName, lastName, email, phone);
        }

        // Remove functionality
        public void RemoveTicket(int ticketId)
        {
            _ticketService.RemoveTicket(ticketId);
        }

        // Update functionality
        public void UpdateTicketData(int ticketId, string firstName, string lastName, string email, string phone)
        {
            using var bc = new BookingContext();
            SelectedTicket.First_Name = firstName;
            SelectedTicket.Last_Name = lastName;
            SelectedTicket.Email = email;
            SelectedTicket.Phone = phone;
            bc.SaveChanges();
            _ticketService.UpdateTicketData(ticketId, firstName, lastName, email, phone);
        }
        
        // Filter Functionality
        public List<Ticket> FilterTicketsById(int ticketId)
        {
            return _ticketService.FilterTicketsById(ticketId);
        }
    }
}
