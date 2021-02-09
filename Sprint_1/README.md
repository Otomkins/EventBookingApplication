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

> A User story is complete when the user's specifications have been met. This has been implemented within the business layer of the app and unit testing has been used to test its functionality. Acceptance criteria have been ticked off and any alterations or changes are documented and brought into the next sprint when incomplete.

## **Project Lifecycle**

### SPRINT 1
![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_1/Sprint%201%20Start%20-%20Project%20Board.png)
> *See 'Sprint 1 Start - ProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_1'.*

**SPRINT GOAL:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [ ] Create and populate new database with test information.

- [ ] Create CRUD methods for database.

- [ ] Create passing unit tests for these methods.

- [ ] Create basic WPF front layer.

- [ ] Implement CRUD functionality inside app.

<b>USER EPIC 1:</b>

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
![](https://github.com/Otomkins/EventBookingApplication/blob/main/Event%20Booking%20Class%20Diagram.png)
> *See 'Event Booking ERD.png' and 'Event Booking Class Diagram.png' in 'Otomkins/EventBookingApplication'.*

After planning out my User Epics and Story's, I started setting up the foundation of the project. Using my *ERD Diagram*, I created my BloggingContext representing my desired relations and migrated the data into my database. 

This achieved the first Sprint goal.

<b>SPRINT GOAL:</b>
Create basic WPF app that allows the user to use CRUD functionality with event DB. Unit Tests made to test this layer functionality.

- [x] Create new DBContext. Migrate this to DB.

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
### CRUD Functionality

The next goal was to create CRUD functionality that I would use throughout the app. At this stage, I kept in mind their testability and this drove their development.

#### User Story: Read Functionality

I created Read methods where I could retrieve data into a List to use elsewhere in the application.

This achieved the 1st User Story criteria.

<b>1. Read Functionality:</b>
As an organiser, I need to be able to display all current info so that consumers can see them.
<b>Acceptance Criteria:</b>

- [x] Relevant data is retrieved based on the selected object.

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
#### User Story: Add Functionality

I created Add methods that would create a new object in the database with the specified information. Events would have a designated Venue object property and Tickets would have an Event object property. This was added automatically with my 'Selected' properties that would link this data together. There is no Add method for tickets as organisers cannot create new booked tickets. 

This achieved a criteria for the 2nd User Story.

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

I created Remove methods that would take the Primary Key of the 'Selected' object, search the database for it and remove it. This was applied to all 3 tables. This would also remove any of the associated objects under them that share the same Primary Key. This is because you cannot have an Event without a Venue and cannot have a Ticket without an Event.

Each remove method would remove the furthest connection first (Tickets) and then work backwards so that it avoids any database errors. I also added an Event Remove method that always a date to be given. All events and tickets registered before that date with be bulk deleted.

This achieved a criteria for the 3nd User Story.

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

I created Update methods that reference each object through my 'Selected' properties. This updates the object data with the information given. This does not effect its object relations and they are still identified by their Primary Keys.

This achieved a criteria for the 4th User Story and for the 2nd Sprint goal.

<b>4. Update Functionality:</b>
As an organiser, I need to be able to update events so that they are correct.
<b>Acceptance Criteria:</b>

- [x] User can update data in the DB.
- [ ] Changes are reflected and displayed.

<b>SPRINT GOAL:</b>
Create basic WPF app that allows the user to use CRUD functionality with event DB. Unit Tests made to test this layer functionality.

- [x] Create and populate new database with test information.

- [x] Create CRUD methods for database.

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

### Unit Testing

The next step was to thoroughly test the CRUD methods. I used the [SetUP] and [TearDown] to make sure that any test data was dealt with before and after the tests. I structured these in a way so that i could capture test data with populated fields and test data that held references to other test data.

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
```
A quick reference document to all the CRUD Tests is available.

> *See 'Sprint 1 Unit Tests Quick Reference.md' in 'Otomkins/EventBookingApplication/Sprint_1'.*

I created Unit Tests to make sure that I was communicating with the database in the way I expected to. Each test would check the status of the entity before and after the desired change to make sure that it was performed. The business layer methods were used to perform the CRUD functions and the test criteria queried the database for the change, ensuring that the methods functioned appropriately.

Add functions would create new entities, Remove functions would delete the entry, Update functions would change the same entity and the Read functions would return all data. The Remove methods were also tested to make sure that they also deleted all related subcategories of data in an appropriate way.

This achieved the 2nd criteria for each of the remaining User Stories and the 3rd Sprint goal.

<b>2. Add Functionality:</b>
As an organiser, I need to be able to add events so that consumers can see what's new.
<b>Acceptance Criteria:</b>

- [x] User can add data to the DB.
- [x] Data is returned and displayed.

<b>3. Remove Functionality:</b>
As an organiser, I need to be able to delete events so that only correct information is shown.
<b>Acceptance Criteria:</b>

- [x] User can delete data from the DB.
- [x] Deleted data is no longer displayed.

<b>4. Update Functionality:</b>
As an organiser, I need to be able to update events so that they are correct.
<b>Acceptance Criteria:</b>

- [x] User can update data in the DB.
- [x] Changes are reflected and displayed.

**SPRINT GOAL:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [x] Create and populate new database with test information.

- [x] Create CRUD methods for database.

- [x] Create passing unit tests for these methods.

### WPF Application

The next step was to create a basic WPF front end application to visually show the data and the functionality. From my planning stage, I had a good idea of how I wanted to structure the app. I then set up the basic design with labels, list boxes and text boxes.

This achieved the the 4th Sprint goal.

**SPRINT GOAL:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [x] Create and populate new database with test information.
- [x] Create CRUD methods for database.
- [x] Create passing unit tests for these methods.
- [x] Create basic WPF front layer.
- [ ] Implement CRUD functionality inside app.

The last goal included implementing the functionality within the application. This was nearing the end of Sprint 1 and so I had only implemented some of the features properly by the end of it.

![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_1/Sprint%201%20End%20-%20Application.png)
> *See 'Sprint 1 End - Application.png'  in 'Otomkins/EventBookingApplication/Sprint_1'.*

### Output Of Sprint Review

And the end of Sprint 1, all of my User Stories had their criteria met and where staged for review. They passed review and were placed in the 'Done' column of my project board. The last Sprint 1 goal was then passed on to Sprint 2's goals to complete before continuing.

### Sprint Retrospective

Overall, I felt that this Sprint was very successful. I created the functionality that would be used heavily within the application and tested them thoroughly. This met all the criteria for my User Stories which put me in a good position for the next Sprint.

The last Sprint goal was unfortunately missed. However, this was a small addition that I was practiced in implementing from my recent mini project ventures. This meant that I could confidently move this into the next Sprint without it effecting its flow too much.

The reason for this was because I had slightly underestimated how long the current goals would take to achieve. I spent longer than expected thoroughly testing the functionality and organising the documentation and my GitHub in a way that compliments my working style. The extra attention to these areas meant that I became more comfortable and knowledgeable and I believe this will help me greatly in future Sprints. This solid foundation will allow me to be more efficient with my time in the future.

![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_1/Sprint%201%20End%20-%20Project%20Board.png)
> *See 'Sprint 1 End - ProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_1'.*
