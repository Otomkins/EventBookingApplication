# Event Booking Application

**By Oliver Tomkins**

**Table of contents:**

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

**Sprint Goal:**

Create basic WPF app with unit tests allowing a user to use CRUD functionality with event database.

- [ ] Create and populate new database with test information.

- [ ] Create CRUD methods for database.

- [ ] Create passing unit tests for these methods.

- [ ] Create basic WPF front layer.

- [ ] Implement CRUD functionality inside app.

  

#### `Project Board`

<img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201104145619962.png" alt="image-20201104145619962" style="zoom: 67%;" />

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



#### `ERD Diagram`

<img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201104194736197.png" alt="image-20201104194736197" style="zoom:50%;" />



#### `Database Creation`

<img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201104194933861.png" alt="image-20201104194933861" style="zoom: 40%;" /><img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201104195821956.png" alt="image-20201104195821956" style="zoom: 53%;" />

<b>SPRINT GOAL:</b>
Create basic WPF app that allows the user to use CRUD functionality with event DB. Unit Tests made to test this layer functionality.

- [x] Create new DBContext. Migrate this to DB.




#### `User Story: Read Functionality`

<img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201105095640550.png" alt="image-20201105095640550" style="zoom:50%;" />

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

<img src="C:\Users\otomk\AppData\Roaming\Typora\typora-user-images\image-20201105102005536.png" alt="image-20201105102005536" style="zoom:50%;" />