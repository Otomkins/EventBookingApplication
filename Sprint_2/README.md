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

### SPRINT 2

> *See 'Sprint 2 Start - ProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_2'.*

<b>SPRINT GOAL:</b>
Continue from Sprint 1 and finish front representation of functionality. Add multiple testable filtering options with the addition of displayed ticket information. 

- [ ] Implement CRUD functionality within app (Sprint 1).
- [ ] Add additional WPF CRUD functions with Event data and present it appropriately.
<b>Moved To Sprint 3:</b>
- [ ] Add Read/Remove/Update functions with Ticket data inside.
- [ ] Create additional exceptions functionality within Methods and unit test these.
- [ ] Implement exceptions visually with app.
- [ ] Create filtering functionality and unit test them.
- [ ] Implement filtering functionality with app.

<b>USER EPIC 1 (Continued From Sprint 1):</b>

> As an organiser, I need an app to present my concert data so that I can populate and manage these for consumer use.

<b>USER EPIC 2:</b>

> As an organiser, I need to be able to view all booked event ticket data and filter all data as needed so that the app is easier to use.

#### `USER STORIES`

<b>1. WPF Front - Venues</b>
As an organiser, I need to be able to see and manipulate the Venue data with the CRUD functionality.
<b>Acceptance Criteria:</b>

- [ ] User can now access the DB CRUD functions through the app for the Venue data.
- [ ]  Displayed data is changed with the use of the functions.

<b>2. WPF Front - Events</b>
As an organiser, I need to be able to see and manipulate the Event data with the CRUD functionality.
<b>Acceptance Criteria:</b>

- [ ] User can now access the DB CRUD functions through the app for the Events data.
- [ ]  Displayed data is changed with the use of the functions.

### Initial Planning And Priority Change

Initially, I planned to use the majority of this Sprint to add filtering functionality into my application. When I began implementing the CRUD functionality into the WPF application, it took more time then I was expecting and so I changed my Sprint priorities to reflect this. I moved some of the functionality into Sprint 3 so that I could make sure that the core application was working as planned. I then created new user stories that reflected these changes and added them.

> *See 'Sprint 2 Start - UpdatedProjectBoard.png'  in 'Otomkins/EventBookingApplication/Sprint_2'.*

### WPF Application

From the previous Sprint, I had created a basic WPF front for my application with some of the functionality within it. Continuing this, I added the rest of the CRUD functionality for the Venue column. During this time, the Update functionality wasn't interacting with the GUI as expected. Looking into the code I created a new solution to querying the database appropriately. I did this for the Venue and Event Update methods. The Ticket functionality will be updated once the GUI displays and interacts with the Ticket data. This will be achieved in Sprint 3. After applying this code, I re-ran the unit tests to make sure that they still were passing. All tests passed and this functionality was implemented successfully.

```c#
        var q = bc.Venues.Where(v => v.VenueId == SelectedVenue.VenueId).FirstOrDefault();
        q.Name = name;
        q.City = city;
        q.Email = email;
        q.Phone = phone;
        bc.SaveChanges();
```
```c#
        var q = bc.Events.Where(v => v.EventId == SelectedEvent.EventId).FirstOrDefault();
        q.Event_Name = eventName;
        q.Main_Act = mainAct;
        q.Supporting_Act = supportingAct;
        q.Genre = genre;
        q.Description = description;
        q.Date = date;
        q.Start_Time = startTime;
        q.End_Time = endTime;
        q.Capacity = capacity;
        bc.SaveChanges();
```
This achieved the criteria for the 1st User Story and for the 1st Sprint Goal.

<b>1. WPF Front - Venues</b>
As an organiser, I need to be able to see and manipulate the Venue data with the CRUD functionality.
<b>Acceptance Criteria:</b>

- [x] User can now access the DB CRUD functions through the app for the Venue data.
- [x] Displayed data is changed with the use of the functions.

<b>SPRINT GOAL:</b>
Continue from Sprint 1 and finish front representation of functionality. Add multiple testable filtering options with the addition of displayed ticket information. 

- [x] Implement CRUD functionality within app (Sprint 1).

The next step was to implement the same functionality with the Event data. I started designing the GUI in a way that it can easily present all of the data related to each event. After adding the WPF elements, I used the same functionality to communicate with the Event data. This was mostly similar to what I implemented with the Venue functionality, however, I was careful in the XAML to make sure that I kept and appropriately displayed the relations between the Events and their Venues.

This achieved the criteria for the 2nd User Story and for the 2nd Sprint Goal.

<b>SPRINT GOAL:</b>
Continue from Sprint 1 and finish front representation of functionality. Add multiple testable filtering options with the addition of displayed ticket information. 

- [x] Implement CRUD functionality within app (Sprint 1).
- [x] Add additional WPF CRUD functions with Event data and present it appropriately.

<b>2. WPF Front - Events</b>
As an organiser, I need to be able to see and manipulate the Event data with the CRUD functionality.
<b>Acceptance Criteria:</b>

- [x] User can now access the DB CRUD functions through the app for the Events data.
- [x] Displayed data is changed with the use of the functions.

*See 'Application Demonstration.mkv'  in 'Otomkins/EventBookingApplication/Sprint_2'.*

### Output Of Sprint Review

At the end of Sprint 2, all of my User Stories had their criteria met and were staged for review. They passed review and were placed in the 'Done' column of my project board. Most of the Sprint goals were removed until the next Sprint. I passed these on to Sprint 3's goals to continue and complete.

### Sprint Retrospective

Overall, this was another successful Sprint. My priorities changed during, however, I planned appropriately and finished with the application in a good place. Even though I spent more time then I expected on some of the functionality, I still have plenty of time to realise my goals for the project. This was because of the large amount of preparing I did when I was planning ahead.

Taking what I learnt from the previous Sprint, I was able to adapt and appropriately organise my goals. At the start, I planned for Sprint 3 to be where I can add extra functionality to the project. Unfortunately, I will not be able to do so before the deadline of the project. Nonetheless, it gives me space to comfortably implement everything that I had planned for an event organiser to be able to use the app.

*See 'Sprint 2 End - ProjectBoard Start.png'  in 'Otomkins/EventBookingApplication/Sprint_2'.*