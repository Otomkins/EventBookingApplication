using System;
using System.Collections.Generic;
using System.Text;

namespace Concert_Booking_Project.Services
{
    public interface IEventService
    {
        public List<Event> RetrieveAllEvents(int venueId);
        public void AddEvent(int venueId, string eventName, string mainAct, string genre, string description, DateTime date,
            string startTime, string endTime, string supportingAct);
        public void RemoveEvent(int eventId);
        public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo);
        public void UpdateEventData(int eventId, string eventName, string mainAct, string supportingAct, string genre, string description, DateTime date,
            string startTime, string endTime);
        public List<Event> FilterEventsByName(int venueId, string filterText);
        public List<Event> FilterEventsByDate(DateTime startDate, DateTime endDate);
    }
}
