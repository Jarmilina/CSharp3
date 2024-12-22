# ToDoList
The ToDoList WebApp is a task management application built using C# with ASP.NET Core. It features a Blazor and Bootstrap frontend for a responsive and interactive user interface. For database management, the app uses SQLite with Entity Framework Core (EF Core), ensuring efficient and seamless data operations.

The app allows users to create, edit, delete, and organize tasks, offering sorting capabilities by time added, name, or category.

# Installation 
**Prerequisites**

.NET SDK (6.0 or higher).

**Steps** 

Clone the repository:
git clone https://github.com/yourusername/ToDoListWebApp.git  

Navigate to the project directory:
cd ToDoListWebApp  

Restore dependencies:
dotnet restore  

Build the project:
dotnet build  

Run the application:
dotnet run  

Open your browser and navigate to:
http://localhost:5001/Dashboard

## Usage
**Add a Task**:
Navigate to the "Add Task" page.
Fill in the task details (e.g., name, category) and click "Save."

**Edit a Task**:
Select a task from the list on the main page.
Click "Edit," update the task details, and save changes.

**Delete a Task**:
Click the "Delete" button next to a task to remove it.

**Sort Tasks**:
Use the sorting dropdown menu to sort tasks by date (newest/oldest), name, or category.

## Database configuration
The application uses an SQLite database for storing tasks. The database file (tasks.db) will be created automatically in the application's directory upon first run.
