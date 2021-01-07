using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Concert_Booking_Project.Services;

namespace Concert_Booking_Project
{
    public class EventCRUDCode
    {
        private readonly IEventService _eventService;
        public EventCRUDCode(BookingContext context)
        {
            _eventService = new EventService(context);
        }
        public EventCRUDCode()
        {
            _eventService = new EventService(new BookingContext());
        }

        // Display relevant data based on what is selected
        public Event SelectedEvent { get; set; }

        // Read functionality
        public List<Event> RetrieveAllEvents(int venueId)
        {
            return _eventService.RetrieveAllEvents(venueId);
        }

        public void SetSelectedEvent(object selectedEvent)
        {
            SelectedEvent = (Event)selectedEvent;
        }

        // Add functionality
        public void AddEvent(int venueId, string eventName, string mainAct, string genre, string description, DateTime date,
            string startTime, string endTime, string supportingAct = "N/A")
        {
            _eventService.AddEvent(venueId, eventName, mainAct, genre, description, date,
            startTime, endTime, supportingAct);
        }

        // Remove functionality
        public void RemoveEvent(int eventId) // Removes related Ticket data
        {
            _eventService.RemoveEvent(eventId);
        }

        // Added functionality to specify date where data is removed up to
        public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
        {
            _eventService.RemoveOutOfDateEventsAndTickets(deleteUpTo);
        }

        // Update functionality
        public void UpdateEventData(int eventId, string eventName, string mainAct, string supportingAct, string genre, string description, DateTime date,
            string startTime, string endTime)
        {
            SelectedEvent.Event_Name = eventName;
            SelectedEvent.Main_Act = mainAct;
            SelectedEvent.Supporting_Act = supportingAct;
            SelectedEvent.Genre = genre;
            SelectedEvent.Description = description;
            SelectedEvent.Date = date;
            SelectedEvent.Start_Time = startTime;
            SelectedEvent.End_Time = endTime;
            _eventService.UpdateEventData(eventId, eventName, mainAct, supportingAct, genre, description, date,
            startTime, endTime);
        }

        // Filter Functionality
        public List<Event> FilterEventsByName(int venueId, string filterText)
        {
            return _eventService.FilterEventsByName(venueId, filterText);
        }

        public List<Event> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            return _eventService.FilterEventsByDate(startDate, endDate);
        }
    }
}
