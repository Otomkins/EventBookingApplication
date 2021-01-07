using System;
using System.Collections.Generic;
using System.Linq;

namespace Concert_Booking_Project.Services
{
    public class EventService : IEventService
    {
        private readonly BookingContext _context;

        public EventService(BookingContext context)
        {
            _context = context;
        }

        public List<Event> RetrieveAllEvents(int venueId)
        {
            return _context.Events.Where(v => v.Venue.VenueId == venueId).ToList();
        }

        public void AddEvent(int venueId, string eventName, string mainAct, string genre, string description, DateTime date,
            string startTime, string endTime, string supportingAct)
        {
            var q = _context.Venues.Where(e => e.VenueId == venueId);
            foreach (var item in q)
            {
                var newEvent = new Event
                {
                    Venue = item,
                    Event_Name = eventName,
                    Main_Act = mainAct,
                    Supporting_Act = supportingAct,
                    Genre = genre,
                    Description = description,
                    Date = date,
                    Start_Time = startTime,
                    End_Time = endTime
                };
                _context.Events.Add(newEvent);
            }
            _context.SaveChanges();
        }

        public void RemoveEvent(int eventId)
        {
            var q2 = _context.Tickets.Where(t => t.Event.EventId == eventId);
            foreach (var item in q2)
                _context.Remove(item);
            _context.SaveChanges();

            var q = _context.Events.Where(e => e.EventId == eventId);
            foreach (var item in q)
                _context.Remove(item);
            _context.SaveChanges();
        }

        public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
        {
            var q2 = _context.Tickets.Where(t => t.Event.Date < deleteUpTo);
            foreach (var item in q2)
                _context.Remove(item);
            _context.SaveChanges();

            var q = _context.Events.Where(e => e.Date < deleteUpTo);
            foreach (var item in q)
                _context.Remove(item);
            _context.SaveChanges();
        }

        public void UpdateEventData(int eventId, string eventName, string mainAct, string supportingAct, string genre, string description, DateTime date,
            string startTime, string endTime)
        {
            var q = _context.Events.Where(v => v.EventId == eventId).FirstOrDefault();
            q.Event_Name = eventName;
            q.Main_Act = mainAct;
            q.Supporting_Act = supportingAct;
            q.Genre = genre;
            q.Description = description;
            q.Date = date;
            q.Start_Time = startTime;
            q.End_Time = endTime;
            _context.SaveChanges();
        }

        public List<Event> FilterEventsByName(int venueId, string filterText)
        {
            return _context.Events.Where(e => e.Event_Name.Contains(filterText) && e.Venue.VenueId == venueId).ToList();
        }

        public List<Event> FilterEventsByDate(DateTime startDate, DateTime endDate)
        {
            return _context.Events.Where(e => e.Date > startDate && e.Date < endDate).ToList();
        }
    }
}
