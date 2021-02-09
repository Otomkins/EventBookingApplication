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

### SPRINT 3

![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_3/Sprint%203%20Start%20-%20Project%20Board.png)
> *See 'Sprint 3 Start - ProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_3'.*

<b>SPRINT GOAL:</b>
Continue with Sprint 2 goals that were pushed back. Add ticket information within the app and implement its CRUD functions. Add filtering options to the app for Venues, Events and Tickets.

- [ ] Implement the Ticket data display into the app.
- [ ] Add Read/Remove/Update functions for Ticket data.
- [ ] Create additional exceptions functionality within the app and within Methods when needed.
- [ ] Test exceptions within app.
- [ ] Create filtering functionality and implement and test them within the app.

<b>USER EPIC 2:</b>

> As an organiser, I need to be able to view all booked event ticket data and filter all data as needed so that the app is easier to use.

#### `USER STORIES`

<b>1. Further Read Functionality:</b>
As an organiser, I need to be able to see ticket and customer information relevant to my selection so that I can monitor each event.
<b>Acceptance Criteria:</b>

- [ ] User can now see ticket and customer data.
- [ ]  Relevant data is retrieved based on the selected object.

<b>2. Ticket Filter Functionality:</b>
As an organiser, I need filtering functionality for the ticket info so that I can gather only relevant data.
<b>Acceptance Criteria:</b>

- [ ] User can filter and display ticket data as needed.
- [ ] Data returned reflects filter and is displayed.
- [ ] Filters conditions can be changed and removed.

<b>3. Event Filter Functionality:</b>
As an organiser, I need filtering functionality for the events list so that I can gather only relevant data.
<b>Acceptance Criteria:</b>

- [ ] User can filter and display event data as needed.
- [ ] Data returned reflects filter and is displayed.
- [ ] Filters conditions can be changed and removed.

### Initial Planning

After the last Sprint, I had moved some of the project goals into this Sprint. I started by planning out what i will be able to achieve by the end. From this, I decided to leave my 3rd User Epic in the Project Backlog. This included creating Login functionality, but I would not have enough time. However, the rest of my goals were achievable and I adjusted them slightly for this Sprint. 

The way I designed the app meant that it would show error messages linked to what the user was doing to avoid the application breaking. I decided to use this as my main method of testing for the filtering functionality. This should allow me more time to test my application and handle any errors that occur.

Soon into the Sprint, I noticed that my calendars on the GUI were not displaying data as I planned. I added this as an issue and placed it into my Sprint Backlog.

### Ticket Read Functionality and GUI Elements

I started the Sprint by implementing the Ticked Read functionality into the app. Doing so allowed me to visualise the app as a whole now that most of the GUI elements are present. The next step was to add filtering functionality and so I moved onto setting out all the GUI elements that I will be using for this and for the CRUD Ticket functionality.

Whilst doing so, I noticed a small error. Where I would retrieve all Ticket data, it would be in relation to the VenueId. This meant that the tickets would not display based off of the selected Event. I fixed this small error so that the functionality would work.

```c#
    public List<Ticket> RetrieveAllTickets()
    {
        using var bc = new BookingContext();
        var q = bc.Tickets.Where(t => t.Event.EventId == SelectedEvent.EventId);
        return q.ToList();
    }
```

It became clear that the sections of the app were not distinct enough and all the elements gave the application a cluttered look. With this time focused on the GUI, I added some section divides and changes the design slightly so that its flow and use is more intuitive.

This achieved the criteria for the 1st User Story and for the 1st Sprint Goal.

<b>1. Further Read Functionality:</b>
As an organiser, I need to be able to see ticket and customer information relevant to my selection so that I can monitor each event.
<b>Acceptance Criteria:</b>

- [x] User can now see ticket and customer data.
- [x] Relevant data is retrieved based on the selected object.

<b>SPRINT GOAL:</b>
Continue with Sprint 2 goals that were pushed back. Add ticket information within the app and implement its CRUD functions. Add filtering options to the app for Venues, Events and Tickets.

- [x] Implement the Ticket data display into the app.

### Ticket CRUD Functionality

With the GUI setup, I could now implement my previously tested Ticket CRUD functionality into the application. I have now implemented all CRUD functionality into the app. From the Users perspective, they cannot generate new tickets. For the purpose of demonstration, I added a Add Ticket method to be used when needed.

```c#
    public void AddTicket(string firstName, string lastName, string email, string phone) // Only used to create data for demonstration
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(e => e.EventId == SelectedEvent.EventId);
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
            bc.Tickets.Add(newTicket);
        }
        bc.SaveChanges();
    }
```
This achieved the criteria for the 2nd Sprint Goal.

<b>SPRINT GOAL:</b>
Continue with Sprint 2 goals that were pushed back. Add ticket information within the app and implement its CRUD functions. Add filtering options to the app for Venues, Events and Tickets.

- [x] Implement the Ticket data display into the app.
- [x] Add Read/Remove/Update functions for Ticket data.

### Filtering Functionality

The next step was to add the filtering functionality. With the GUI set up, I started adding the Methods into my business layer code. This was done in the same way as my previous 'RetrieveAll' Methods but with a specified filtering parameter.

```c#
    // Filter Functionality
    public List<Event> FilterEventsByName(string filterText)
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(e => e.Event_Name.Contains(filterText) && e.Venue.VenueId == SelectedVenue.VenueId);
        return q.ToList();
    }
    public List<Event> FilterEventsByDate(DateTime startDate, DateTime endDate)
    {
        using var bc = new BookingContext();
        var q = bc.Events.Where(e => e.Date > startDate && e.Date < endDate);
        return q.ToList();
    }

    public List<Ticket> FilterTicketsById(int ticketId)
    {
        using var bc = new BookingContext();
        var q = bc.Tickets.Where(t => t.TicketId == ticketId);
        return q.ToList();
    }
```
I used the application to test these filters with different objects and tested if I could remove the filter and return the displayed data to normal. All tests were successful and I could now navigate the app with its full functionality. I implemented this functionality with the Ticket and Event data.

This achieved the criteria for both the 2nd and 3rd User Story and for the 3nd Sprint Goal.

<b>2. Ticket Filter Functionality:</b>
As an organiser, I need filtering functionality for the ticket info so that I can gather only relevant data.
<b>Acceptance Criteria:</b>

- [x] User can filter and display ticket data as needed.
- [x] Data returned reflects filter and is displayed.
- [x] Filters conditions can be changed and removed.

<b>3. Event Filter Functionality:</b>
As an organiser, I need filtering functionality for the events list so that I can gather only relevant data.
<b>Acceptance Criteria:</b>

- [x] User can filter and display event data as needed.
- [x] Data returned reflects filter and is displayed.
- [x] Filters conditions can be changed and removed.

<b>SPRINT GOAL:</b>
Continue with Sprint 2 goals that were pushed back. Add ticket information within the app and implement its CRUD functions. Add filtering options to the app for Venues, Events and Tickets.

- [x] Implement the Ticket data display into the app.
- [x] Add Read/Remove/Update functions for Ticket data.
- [x] Create filtering functionality and implement and test them within the app.

### Error Handling

The last task that I put time aside for was error handling. This is where I tested every function of my application individually through intensive use. I also tested how the application works and communicates with itself when switching between functions. I set out to find as many bugs as possible. By the end, with a few alterations to the XAML code, the application and its error text boxes handled most of the errors that I came across. Unfortunately, due to time constraints, I added this unhandled exception to my Project Board Issues. This was where the User could put any date into the Bulk Remove function. An invalid date would stop the application from running.

This achieved the criteria for both the final Sprint Goals.

<b>SPRINT GOAL:</b>
Continue with Sprint 2 goals that were pushed back. Add ticket information within the app and implement its CRUD functions. Add filtering options to the app for Venues, Events and Tickets.

- [x] Implement the Ticket data display into the app.
- [x] Add Read/Remove/Update functions for Ticket data.
- [x] Create additional exceptions functionality within the app and within Methods when needed.
- [x] Test exceptions within app.
- [x] Create filtering functionality and implement and test them within the app.

![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_3/Sprint%203%20End%20-%20Application.png)
> *See 'Sprint 3 End - Application.png' in 'Otomkins/EventBookingApplication/Sprint_3'.*

### Output Of Sprint Review

At the end of Sprint 3, the final Sprint, all of my User Stories had their criteria met and were staged for review. They passed review and were placed in the 'Done' column of my project board. Whilst there was items in the project backlog, I had decided early on that I would not include these due to time constraints.

### Sprint/Project Retrospective

Overall, the Sprint was successful. This was mainly due to the emphasis on my previous planning and my change in priority, which allowed me to pace the rest of the project lifecycle effectively. Even though I did not implement everything that I initially had planned at the start of the project, I managed to create a working application that ticket off all of my criteria and functioned exactly how I wanted it to.

The extra time I allowed myself at the end gave me chance to error check the smallest details in my application to make sure that it communicates exactly as planned in every instance and finish off the documentation appropriately. There was a couple issues that I flagged during this Sprint which I would have liked to have put work into but couldn't due to time constraints. These don't pose too much of a detriment to the application and are definitely smaller changes that can be made in future versions.

The project as a whole went well, and after a bit of a slow start, I came into my own and had a comfortable routine where I could work on my project, with the planning on my project board, and update my documentation and GitHub frequently. When I encountered issues, I had a good foundation where I could document them and solve them so that they do not interfere with the project. Closer to the start, this was more of an issue and did take up more time then I expected. However, I can confidently say that I learnt a lot of useful skills and really solidified my knowledge of all the individual components that went into this project. In the future, I will be able to be more organised quickly, which would allow for more efficient project work.

![](https://github.com/Otomkins/EventBookingApplication/blob/main/Sprint_3/Sprint%203%20End%20-%20Project%20Board.png)
> *See 'Sprint 3 End - ProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_3'.*
