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

## **Project Lifecycle**

### SPRINT 1

> *See 'Sprint 1 - ProjectBoard Start.png'  in 'Otomkins/EventBookingApplication'.*

**SPRINT GOAL:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [ ] Create and populate new database with test information.

- [ ] Create CRUD methods for database.

- [ ] Create passing unit tests for these methods.

- [ ] Create basic WPF front layer.

- [ ] Implement CRUD functionality inside app.


<b>USER EPIC</b>

> As an organiser, I need an app to present my concert data so that I can populate and manage these for consumer use.

#### `USER STORIES`

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

### Database Creation

> *See 'Event Booking ERD.png' and 'Event Booking Class Diagram.png' in 'Otomkins/EventBookingApplication'.*

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

### CRUD Functionality

#### User Story: Read Functionality

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

#### User Story: Add Functionality

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
#### User Story: Remove Functionality

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
        var q3 = bc.Tickets.Where(t => t.Event.Venue.VenueId == SelectedVenue.VenueId);
        foreach (var item in q3)
            bc.Remove(item);
        bc.SaveChanges();

        var q2 = bc.Events.Where(e => e.Venue.VenueId == SelectedVenue.VenueId);
        foreach (var item in q2)
            bc.Remove(item);
        bc.SaveChanges();

        var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId);
        foreach (var item in q)
            bc.Remove(item);
        bc.SaveChanges();
    }

    public void RemoveEvent() // Removes related Ticket data
    {
        using var bc = new BookingContext();
        var q2 = bc.Tickets.Where(t => t.Event.Venue.VenueId == SelectedVenue.VenueId);
        foreach (var item in q2)
            bc.Remove(item);
        bc.SaveChanges();

        var q = bc.Events.Where(e => e.Venue.VenueId == SelectedVenue.VenueId);
        foreach (var item in q)
            bc.Remove(item);
        bc.SaveChanges();
    }

    public void RemoveTicket()
    {
        using var bc = new BookingContext();
        var q = bc.Tickets.Where(t => t.TicketId == SelectedTicket.TicketId);
        foreach (var item in q)
            bc.Remove(item);
        bc.SaveChanges();
    }

    // Added functionality to specify date where data is removed up to
    public void RemoveOutOfDateEventsAndTickets(DateTime deleteUpTo)
    {
        using var bc = new BookingContext();
        var q2 = bc.Tickets.Where(t => t.Event.Date < deleteUpTo);
        foreach (var item in q2)
            bc.Remove(item);
        bc.SaveChanges();

        var q = bc.Events.Where(e => e.Date < deleteUpTo);
        foreach (var item in q)
            bc.Remove(item);
        bc.SaveChanges();
    }
```






#### User Story: Update Functionality

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
<b>SPRINT GOAL:</b>
Create basic WPF app that allows the user to use CRUD functionality with event DB. Unit Tests made to test this layer functionality.

- [x] Create and populate new database with test information.

- [x] Create CRUD methods for database.


### Unit Testing

#### [SetUp] & [TearDown]

```c#
    BookingCodeLayer _bookingCodeLayer = new BookingCodeLayer();

    [SetUp]
    public void Setup()
    {
        using var bc = new BookingContext();
        var selectedTicket = bc.Tickets.Where(t => t.First_Name == "Sample1234");
        bc.Tickets.RemoveRange(selectedTicket);
        bc.SaveChanges();

        var selectedEvent = bc.Events.Where(e => e.Event_Name == "Sample1234");
        bc.Events.RemoveRange(selectedEvent);
        var selectedEvent2 = bc.Events.Where(e => e.Venue.Name == "Sample1234");
        bc.Events.RemoveRange(selectedEvent2);
        bc.SaveChanges();

        var selectedVenue = bc.Venues.Where(v => v.Name == "Sample1234");
        bc.Venues.RemoveRange(selectedVenue);
        bc.SaveChanges();

        _bookingCodeLayer.SelectedVenue = null;
        _bookingCodeLayer.SelectedEvent = null;
        _bookingCodeLayer.SelectedTicket = null;
    }

    [TearDown]
    public void TearDown()
    {
        using var bc = new BookingContext();
        var selectedTicket = bc.Tickets.Where(t => t.First_Name == "Sample1234");
        bc.Tickets.RemoveRange(selectedTicket);
        bc.SaveChanges();

        var selectedEvent = bc.Events.Where(e => e.Event_Name == "Sample1234");
        bc.Events.RemoveRange(selectedEvent);
        var selectedEvent2 = bc.Events.Where(e => e.Venue.Name == "Sample1234");
        bc.Events.RemoveRange(selectedEvent2);
        bc.SaveChanges();

        var selectedVenue = bc.Venues.Where(v => v.Name == "Sample1234");
        bc.Venues.RemoveRange(selectedVenue);
        bc.SaveChanges();

        _bookingCodeLayer.SelectedVenue = null;
        _bookingCodeLayer.SelectedEvent = null;
        _bookingCodeLayer.SelectedTicket = null;
    }
```
#### Read Unit Tests

```c#
    [Test]
    public void RetrieveVenuesTest()
    {
        using var bc = new BookingContext();
        // Checks size of DB before adding Venues
        var venueList = _bookingCodeLayer.RetrieveAllVenues();

        // Add Venues
        var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
        var newVenue2 = new Venue() { Name = "Sample1234", City = "Cty2", Email = "Eml2", Phone = "Phn2" };
        bc.Venues.Add(newVenue);
        bc.Venues.Add(newVenue2);
        bc.SaveChanges();

        // Retrieve all Venues
        var venueList2 = _bookingCodeLayer.RetrieveAllVenues();

        // Check size of list on return against created Venues
        Assert.AreEqual(venueList2.Count == venueList.Count + 2, true);
    }

    [Test]
    public void RetrieveEventsTest()
    {
        using var bc = new BookingContext();
        // Add Venue
        var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
        bc.Venues.Add(newVenue);
        _bookingCodeLayer.SelectedVenue = newVenue;

        // Checks size of DB before adding Events
        var eventList = _bookingCodeLayer.RetrieveAllEvents();

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
            End_Time = "23:00",
            Capacity = 50
        };
        bc.Events.Add(newEvent);
        bc.SaveChanges();

        // Retrieve all Events
        var eventList2 = _bookingCodeLayer.RetrieveAllEvents();

        // Check size of list on return against created Events
        Assert.AreEqual(eventList2.Count == eventList.Count + 1, true);
    }

    [Test]
    public void RetrieveTicketsTest()
    {
        using var bc = new BookingContext();
        // Add Venue
        var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
        bc.Venues.Add(newVenue);

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
            End_Time = "23:00",
            Capacity = 50
        };
        bc.Events.Add(newEvent);
        _bookingCodeLayer.SelectedEvent = newEvent;

        // Checks size of DB before adding Events
        var ticketList = _bookingCodeLayer.RetrieveAllTickets();

        // Add Ticket
        var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", 	Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();

        // Retrieve all Events
        var ticketList2 = _bookingCodeLayer.RetrieveAllTickets();

        // Check size of list on return against created Events
        Assert.AreEqual(ticketList2.Count == ticketList.Count + 1, true);
    }
```
#### Add Unit Tests

```c#
    [Test]
    public void AddingVenueTest()
    {
        using var bc = new BookingContext();
        // Add Venue
        _bookingCodeLayer.AddVenue("Sample1234", "Cty", "Eml", "Phn");

        // Test if Venue has been added as expected
        var newVenue = bc.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(newVenue.City, "Cty");
        Assert.AreEqual(newVenue.Phone, "Phn");
    }

    [Test]
    public void AddingEventTest()
    {
        using var bc = new BookingContext();
        // Add Venue - Cannot have Event without Venue variable
        var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
        bc.Venues.Add(newVenue);
        bc.SaveChanges();

        // Add Event
        _bookingCodeLayer.SelectedVenue = newVenue;
        _bookingCodeLayer.AddEvent("Nme", "Act", "Gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00", 50);

        // Test if Event has been added as expected
        var newEvent = bc.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(newEvent.Main_Act, "Act");
        Assert.AreEqual(newEvent.Date, new DateTime(2020, 11, 05));
        Assert.AreEqual(newEvent.Venue.Name, "Sample1234");
    }
```
#### Remove Unit Tests

```c#
    [Test]
    public void RemovingVenueAndRelatedEventsTest()
    {
        using var bc = new BookingContext();
        // Add Venue
        var newVenue = new Venue() { Name = "Sample1234", City = "Cty", Email = "Eml", Phone = "Phn" };
        bc.Venues.Add(newVenue);
        _bookingCodeLayer.SelectedVenue = newVenue;

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
            End_Time = "23:00",
            Capacity = 50
        };
        bc.Events.Add(newEvent);

        // Add Ticket
        var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();

        // Remove Event
        _bookingCodeLayer.RemoveVenue();

        // Test to see if removing Venue also removed related Events and Tickets
        var delVenue = bc.Venues.Where(v => v.Name == "Sample1234").FirstOrDefault();
        var delEvent = bc.Events.Where(e => e.Venue.Name == "Sample1234").FirstOrDefault();
        var delTicket = bc.Tickets.Where(t => t.Event.Venue.Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(delVenue is null, true);
        Assert.AreEqual(delEvent is null, true);
        Assert.AreEqual(delTicket is null, true);
    }

    [Test]
    public void RemovingEventsAndRelatedTicketsTest()
    {
        using var bc = new BookingContext();
        // Add Event
        var newEvent = new Event()
        {
            Event_Name = "Sample1234",
            Main_Act = "Act",
            Genre = "Gnr",
            Description = "Desc",
            Date = new DateTime(2020, 11, 05),
            Start_Time = "16:00",
            End_Time = "23:00",
            Capacity = 50
        };
        bc.Events.Add(newEvent);
        _bookingCodeLayer.SelectedEvent = newEvent;

        // Add Ticket
        var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();

        // Remove Event
        _bookingCodeLayer.RemoveEvent();

        // Test to see if removing Event also removed related Tickets
        var delEvent = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
        var delTicket = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(delEvent is null, true);
        Assert.AreEqual(delTicket is null, true);
    }

    [Test]
    public void RemovingTicketsTest()
    {
        // Add Ticket
        using var bc = new BookingContext();
        var newTicket = new Ticket() { First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();
        _bookingCodeLayer.SelectedTicket = newTicket;

        // Remove Ticket
        _bookingCodeLayer.RemoveTicket();
        var ticket = bc.Tickets.Where(t => t.First_Name == "Sample1234").FirstOrDefault();
        // Test if Ticket has been removed
        Assert.AreEqual(ticket is null, true);
    }

    [Test]
    public void RemovingOutOfDateEventsAndRelatedTicketsTest()
    {
        using var bc = new BookingContext();
        // Add Event
        var newEvent = new Event()
        {
            Event_Name = "Sample1234",
            Main_Act = "Act",
            Genre = "Gnr",
            Description = "Desc",
            Date = new DateTime(1999, 11, 05),
            Start_Time = "16:00",
            End_Time = "23:00",
            Capacity = 50
        };
        bc.Events.Add(newEvent);

        // Add Ticket
        var newTicket = new Ticket() { Event = newEvent, First_Name = "Sample1234", Last_Name = "Lnm", Phone = "Phn", Email = "Eml" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();

        // Remove Events before given date
        _bookingCodeLayer.RemoveOutOfDateEventsAndTickets(new DateTime(1999, 01, 01));
        // Will not delete Event as its later than given date
        var delEvent = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(delEvent is null, false);

        _bookingCodeLayer.RemoveOutOfDateEventsAndTickets(new DateTime(2000, 01, 01));

        // Test to see if removing Event also removed related Tickets
        var delEvent2 = bc.Events.Where(e => e.Event_Name == "Sample1234").FirstOrDefault();
        var delTicket = bc.Tickets.Where(t => t.Event.Event_Name == "Sample1234").FirstOrDefault();
        Assert.AreEqual(delEvent2 is null, true);
        Assert.AreEqual(delTicket is null, true);
    }
```
#### Update Unit Tests

```c#
    [Test]
    public void UpdateVenueTest()
    {
        using var bc = new BookingContext();
        // Add Venue
        var newVenue = new Venue() { Name = "test-test", City = "test", Email = "test", Phone = "test" };
        bc.Venues.Add(newVenue);
        bc.SaveChanges();
        _bookingCodeLayer.SelectedVenue = newVenue;

        // Venue data to change to
        _bookingCodeLayer.UpdateVenueData("Sample1234", "Cty", "Phn", "Eml");
        bc.SaveChanges();

        // Test to compare DB with Venue changes
        var vAfter = bc.Venues.Where(v => v.VenueId == _bookingCodeLayer.SelectedVenue.VenueId).FirstOrDefault();
        Assert.AreEqual(vAfter.Name == "Sample1234", true);
    }

    [Test]
    public void UpdateEventsTest()
    {
        using var bc = new BookingContext();
        // Add Event
        var newEvent = new Event()
        {
            Event_Name = "test-test",
            Main_Act = "test",
            Genre = "test",
            Description = "test",
            Date = new DateTime(1010, 01, 01),
            Start_Time = "test",
            End_Time = "test",
            Capacity = 00
        };
        bc.Events.Add(newEvent);
        bc.SaveChanges();
        _bookingCodeLayer.SelectedEvent = newEvent;

        // Venue data to change to
        _bookingCodeLayer.UpdateEventData("Sample1234", "Act", "SpAct", "gnr", "Desc", new DateTime(2020, 11, 05), "16:00", "23:00", 50);
        bc.SaveChanges();

        // Test to compare DB with Event changes
        var eAfter = bc.Events.Where(e => e.EventId == _bookingCodeLayer.SelectedEvent.EventId).FirstOrDefault();
        Assert.AreEqual(eAfter.Event_Name == "Sample1234", true);
    }

    [Test]
    public void UpdateTicketTest()
    {
        using var bc = new BookingContext();
        // Add Ticket
        var newTicket = new Ticket() { First_Name = "test-test", Last_Name = "test", Email = "test", Phone = "test" };
        bc.Tickets.Add(newTicket);
        bc.SaveChanges();
        _bookingCodeLayer.SelectedTicket = newTicket;

        // Ticket data to change to
        _bookingCodeLayer.UpdateTicketData("Sample1234", "Lnm", "Phn", "Eml");
        bc.SaveChanges();

        // Test to compare DB with Ticket changes
        var tAfter = bc.Tickets.Where(t => t.TicketId == _bookingCodeLayer.SelectedTicket.TicketId).FirstOrDefault();
        Assert.AreEqual(tAfter.First_Name == "Sample1234", true);
    }
}
```
**SPRINT GOAL:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [x] Create and populate new database with test information.

- [x] Create CRUD methods for database.

- [x] Create passing unit tests for these methods.
