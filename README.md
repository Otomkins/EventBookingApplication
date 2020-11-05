# Event Booking Application

**By Oliver Tomkins**

[TOC]

#### `PROJECT GOAL`

> Complete event booking app with WPF front with test-driven development. Allows user to create, update and manage their events and display this information to users. Development cycle is tracked through project board and documentation is updated regularly.

#### `PROJECT DEFINITION OF DONE`

- The event data is presented in a WPF app that splits them up into venue, events, performers etc.
- Entity framework and CRUD functionality are used to manage the relationship with the DB.
- Tests are used to make sure we are communicating with the database as we expect (adding, removing, updating).  These tests pass and more are implemented with added functionality.
- Bugs are recorded and prioritised accordingly.
- Documentation is updated with each Sprint and effectively shows the uses of the app and the project lifecycle.
- Project and documentation delivered on time: 10/22/20 14:00.

#### `USER STORY DEFINITION OF DONE`

> User can create, remove, update and present event data through the app. Relevant data is shown for each event and venue and is organised appropriately. For example, by selecting a venue, its events are shown and this data is able to be manipulated and the events booked ticket information is shown.

## Project Lifecycle

### `SPRINT 1`

*See 'Sprint 1 - ProjectBoard Start.png'  in 'Otomkins/EventBookingApplication'.*

**Sprint Goal:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [ ] Create and populate new database with test information.

- [ ] Create CRUD methods for database.

- [ ] Create passing unit tests for these methods.

- [ ] Create basic WPF front layer.

- [ ] Implement CRUD functionality inside app.


<b>USER EPIC</b>

> As an organiser, I need an app to present my concert data so that I can populate and manage these for consumer use.



#### `User Stories`

<b>1. Read Functionality:</b>
As an organiser, I need to be able to display all current info so that consumers can see them.
<b>Acceptance Criteria:</b>

- [ ] Relevant data is retrieved based on the selected object.

<b>2. Add Functionality:</b>
As an organiser, I need to be able to add events so that consumers can see what's new.
<b>Acceptance Criteria:</b>

- [ ] User can add data to the DB.
- [ ] Data is returned and displayed.

<b>3. Remove Functionality:</b>
As an organiser, I need to be able to delete events so that only correct information is shown.
<b>Acceptance Criteria:</b>

- [ ] User can delete data from the DB.
- [ ] Deleted data is no longer displayed.

<b>4. Update Functionality:</b>
As an organiser, I need to be able to update events so that they are correct.
<b>Acceptance Criteria:</b>

- [ ] User can update data in the DB.
- [ ] Changes are reflected and displayed.

#### `Database Creation`

*See 'Event Booking ERD.png' and 'Event Booking Class Diagram.png' in 'Otomkins/EventBookingApplication'.*

```c#
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
```
<b>SPRINT GOAL:</b>
Create basic WPF app that allows the user to use CRUD functionality with event DB. Unit Tests made to test this layer functionality.

- [x] Create new DBContext. Migrate this to DB.

#### `User Story: Read Functionality`

```c#
    // Display relevant data based on what is selected
    public Venue SelectedVenue { get; set; }
    public Event SelectedEvent { get; set; }
    public Ticket SelectedTicket { get; set; }

    // Read functionality
    public List<Venue> RetrieveAllVenues()
    {
        using var bc = new BookingContext();
        return bc.Venues.ToList();
    }
    public void SetSelectedVenue(object selectedVenue)
    {
        SelectedVenue = (Venue)selectedVenue;
    }

    public List<Event> RetrieveAllEvents()
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(v => v.Venue.VenueId == SelectedVenue.VenueId);
        return q.ToList();
    }
    public void SetSelectedEvent(object selectedEvent)
    {
        SelectedEvent = (Event)selectedEvent;
    }

    public List<Ticket> RetrieveAllTickets()
    {
        using var bc = new BookingContext();
        var q = bc.Tickets.Where(e => e.Event.EventId == SelectedEvent.EventId);
        return q.ToList();
    }
    public void SetSelectedTickets(object selectedTicket)
    {
        SelectedTicket = (Ticket)selectedTicket;
    }
```
<b>1. Read Functionality:</b>
As an organiser, I need to be able to display all current info so that consumers can see them.
<b>Acceptance Criteria:</b>

- [x] Relevant data is retrieved based on the selected object.

#### `User Story: Add Functionality`

<b>2. Add Functionality:</b>
As an organiser, I need to be able to add events so that consumers can see what's new.
<b>Acceptance Criteria:</b>

- [x] User can add data to the DB.
- [ ] Data is returned and displayed.

```c#
    // Add functionality
    public void AddVenue(string name, string city, string email, string phone)
    {
        using var bc = new BookingContext();
        var newVenue = new Venue { Name = name, City = city, Email = email, Phone = phone };
        bc.Venues.Add(newVenue);
        bc.SaveChanges();
    }

    public void AddEvent(string eventName, string mainAct, string genre, string description, DateTime date,
        string startTime, string endTime, int capacity, string supportingAct = "N/A")
    {
        using var bc = new BookingContext();
        var q = bc.Venues.Where(e => e.VenueId == SelectedVenue.VenueId);
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
                End_Time = endTime,
                Capacity = capacity
            };
            bc.Events.Add(newEvent);
        }
        bc.SaveChanges();
    }
```
#### `User Story: Remove Functionality`

<b>3. Remove Functionality:</b>
As an organiser, I need to be able to delete events so that only correct information is shown.
<b>Acceptance Criteria:</b>

- [x] User can delete data from the DB.
- [ ] Deleted data is no longer displayed.

```c#
    // Remove functionality
    public void RemoveVenue() // Removes related Event and Ticket data
    {
        using var bc = new BookingContext();
        var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId);
        RemoveQueryFromDB(q);
        var q2 = bc.Events.Where(e => e.Venue.VenueId == SelectedVenue.VenueId);
        RemoveQueryFromDB(q2);
        var q3 = bc.Tickets.Where(t => t.Event.Venue.VenueId == SelectedVenue.VenueId);
        RemoveQueryFromDB(q3);
    }

    public void RemoveEvent() // Removes related Ticket data
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(e => e.EventId == SelectedEvent.EventId);
        RemoveQueryFromDB(q);
        var q2 = bc.Tickets.Where(t => t.Event.EventId == SelectedEvent.EventId);
        RemoveQueryFromDB(q2);
    }

    public void RemoveTicket()
    {
        using var bc = new BookingContext();
        var q = bc.Tickets.Where(t => t.TicketId == SelectedTicket.TicketId);
        RemoveQueryFromDB(q);
    }

    // Added functionality to specify date where data is removed up to
    public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(e => e.Date < deleteUpTo);
        RemoveQueryFromDB(q);
        var q2 = bc.Tickets.Where(t => t.Event.Date < deleteUpTo);
        RemoveQueryFromDB(q2);
    }

    // Compact method to remove a specified query from DB
    private void RemoveQueryFromDB(IQueryable q)
    {
        using var bc = new BookingContext();
        foreach (var item in q)
        {
            bc.Remove(item);
        }
        bc.SaveChanges();
    }
```
#### `User Story: Update Functionality`

<b>4. Update Functionality:</b>
As an organiser, I need to be able to update events so that they are correct.
<b>Acceptance Criteria:</b>

- [x] User can update data in the DB.
- [ ] Changes are reflected and displayed.

```c#
    // Update functionality
    public void UpdateVenueData(string name, string city, string email, string phone)
    {
        using var bc = new BookingContext();
        SelectedVenue.Name = name;
        SelectedVenue.City = city;
        SelectedVenue.Email = email;
        SelectedVenue.Phone = phone;
        bc.SaveChanges();
    }

    public void UpdateEventData(string eventName, string mainAct, string supportingAct, string genre, string description, DateTime date,
        string startTime, string endTime, int capacity)
    {
        using var bc = new BookingContext();
        SelectedEvent.Event_Name = eventName;
        SelectedEvent.Main_Act = mainAct;
        SelectedEvent.Supporting_Act = supportingAct;
        SelectedEvent.Genre = genre;
        SelectedEvent.Description = description;
        SelectedEvent.Date = date;
        SelectedEvent.Start_Time = startTime;
        SelectedEvent.End_Time = endTime;
        SelectedEvent.Capacity = capacity;
        bc.SaveChanges();
    }

    public void UpdateTicketData(string firstName, string lastName, string email, string phone)
    {
        using var bc = new BookingContext();
        SelectedTicket.First_Name = firstName;
        SelectedTicket.Last_Name = lastName;
        SelectedTicket.Email = email;
        SelectedTicket.Phone = phone;
        bc.SaveChanges();
    }
```