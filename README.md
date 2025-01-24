# 11. Object-Oriented Programming (OOP) : advanced

## 1. Static Methods

In C#, a static method is a method that belongs to the class itself, rather than to any specific instance of the class. This means that a static method can be called without creating an object of the class. Static methods are commonly used for utility or helper functions that don’t depend on any particular instance of the class but rather perform general operations.

- **Class-Level Scope**: Static methods are defined at the class level, meaning they’re accessible through the class itself rather than an instance of the class.

- **Access Without Instantiation**: Since static methods don’t rely on object instances, they can be called directly using the class name.

- **No Access to Instance Members**: Static methods cannot access non-static (instance) members directly, as they are not tied to any particular instance of the class. They can, however, interact with other static fields, properties, and methods.

Here are two very interesting examples:

```csharp
namespace Company 
{
    internal class Car 
    {
        #region Fields
        public static int TotalCars { get; private set; } = 0;
        #endregion

        #region Constructors
        public Car()
        {
            TotalCars += 1;
        }
        #endregion
    }
}

Car car1 = new();
Car car2 = new();
Car car3 = new();
Car car4 = new();

Console.WriteLine(Car.TotalCars); // 4
```

And the second example: 

```csharp
namespace F
{
    internal class AgeUtility 
    {
        // Static method to check if the age qualifies as adult age
        public static bool IsAdult(int age)
        {
            return age >= 18;
        }
    }
}

// Usage
bool isAdult = AgeUtility.IsAdult(20); // Output: true
Console.WriteLine(isAdult);
```

## 2. Short introduction to List

In C#, `List<T>` is a generic collection type that provides a flexible way to store a dynamically-sized list of elements of a specific type (`T`). Unlike arrays, which have a fixed size, lists can grow and shrink as needed, making them a powerful tool for managing collections of items in a more dynamic way.

**Key Characteristics of List<T>**

- **Dynamically Sized**: Lists automatically adjust their size as elements are added or removed, so you don’t need to specify the size ahead of time.

- **Type Safety**: `List<T>` is generic, meaning it holds elements of a specific type (`T`), ensuring type safety. For example, `List<int>` can only contain integers, while `List<string>` can only contain strings.

- **Easy Access to Elements**: Like arrays, lists allow quick access to elements by index, starting from `0` for the first element.

**Basic Operations with List<T>:**

```csharp
List<string> names = new List<string>();

//Add
names.Add("Alice");
names.Add("Bob");

// Access
Console.WriteLine(names[0]); // Output: Alice

// Remove
names.Remove("Alice");    // Removes "Alice" by value
names.RemoveAt(0);        // Removes the first element by index

// And Iterating
foreach (string name in names)
{
    Console.WriteLine(name);
}
```

It is important to know that in advance for the two next exercices to come.

## 3. Composition vs agregation

In C#, composition and aggregation define how objects relate to one another, and both are examples of "has-a" relationships. Let’s explore each concept in detail to understand when and why to use them.

### Composition

**Composition** represents a strong form of association where the containing object (the whole) owns the contained objects (the parts). If the containing object is destroyed, so are the contained objects. This indicates a strict dependency: the parts cannot exist independently of the whole.

**Characteristics of Composition**:

- **Strong Ownership**: The contained object’s lifecycle is fully managed by the containing object.
- **Dependency**: If the whole is destroyed, all parts are destroyed as well.
- **Tight Coupling**: Composition tightly couples the part to the whole, which can be beneficial when parts are inherently tied to the whole.

Example:

```csharp
namespace Rent
{
    internal class Engine
    {
        public string Type { get; set; }
        public Engine(string type) => Type = type; // Constructor
    }

    internal class Car
    {
        private Engine _engine; // Composition: Car owns Engine

        public Car()
        {
            _engine = new Engine("V8"); // Engine is created and managed by Car
        }
    }
}
```

### Aggregation

**Aggregation** represents a weaker form of association where the containing object (the whole) holds references to objects that exist independently of the whole. The contained objects can outlive the container, meaning they aren’t destroyed if the containing object is destroyed.

- **Loose Ownership**: The container (whole) holds a reference to the part, but it doesn’t control its lifecycle.
- **Independence**: The part can exist independently of the whole.
- **Loose Coupling**: Aggregation loosely couples objects, allowing more flexible relationships.

```csharp
namespace LibrarySytem
{
    public class Book
    {
        public string Title { get; init; }
        public Book(string title) => Title = title;
    }

    public class Library
    {
        private List<Book> _books; // Aggregation: Library holds a reference to Book

        public Library()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }
    }
}
```

| Feature      | Composition                     | Aggregation                        |
|--------------|---------------------------------|------------------------------------|
| **Ownership** | Strong (whole controls part)    | Weak (whole holds a reference)     |
| **Lifecycle** | Dependent on the whole          | Independent of the whole           |
| **Coupling**  | Tight                           | Loose                              |
| **Example**   | `Car` with `Engine`             | `Library` with `Book`              |


## 4. Overloading Constructors

In C#, constructor overloading allows a class to have multiple constructors, each with different parameters. This gives you the flexibility to create instances of a class in different ways, based on what information you have available at the time of instantiation. Overloading constructors can make your classes more versatile, improving code readability and usability.

**Why Overload Constructors?**

- **Flexibility in Object Initialization**: Overloaded constructors let you initialize an object with varying amounts of data.
- **Convenience**: Users of the class can choose the constructor that fits their needs, making the class easier to work with.
- **Simpler Defaults**: By providing overloaded constructors, you can set default values for some properties while allowing customization of others.

Suppose we have a `Person` class that represents an individual. We might want to create a `Person` with just a name, a name and an age, or a full name, age, and address.

```csharp
namespace Solution
{
    internal class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        // Constructor with just a name
        public Person(string name)
        {
            Name = name;
            Age = 0; // Default age
        }

        // Constructor with a name and age
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
```

But ! DRY (don't repeat yourself) so ...

```csharp
namespace Solution
{
    internal class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        // Constructor with just a name
        public Person(string name)
        {
            Name = name;
            Age = 0; // Default age
        }
        // Constructor with a name and age
        public Person(string name, int age) : this(name)
        {
            Age = age;
        }
    }
}
```

## Exercices
### 1. Library Management System

#### Context
We want to create a small library application in C# where a user can view available books, add new ones, and borrow them.

#### Instructions

1. **Book Class**
 - Create a class called `Book` with the following properties:
    - `Title` (string): The title of the book.
    - `Author` (string): The author of the book.
    - `Genre` (string): The genre of the book.
    - `IsBorrowed` (bool): Indicates if the book is currently borrowed.
   - Add a constructor to initialize the title, author, and genre of a book, setting `IsBorrowed` to `false` by default.
   - Add a Borrow() method:
        - This method should check if the book is already borrowed:
            - If the book is not borrowed, set IsBorrowed to true and display a message (e.g., using Console.WriteLine) indicating that the book has been successfully borrowed.
            - If the book is already borrowed, display a message (using Console.WriteLine) indicating that the book is unavailable.
        - The method returns a string.

    - Add a ReturnBook() method:
        - This method should check if the book is currently borrowed:
            - If the book is borrowed, set IsBorrowed to false and display a message (e.g., using Console.WriteLine) indicating that the book has been successfully returned.
            - If the book is not borrowed, display a message indicating that there is nothing to return.
        - The method returns a string.

2. **Library Class**
   - Create a class called `Library` with a list of books (`List<Book> Books`).
   - Add methods to:
    - `AddBook(Book book)`: Add a book to the list.
    - `ListBooks()`: Display all books in the library, showing their title, author, genre, and status (borrowed or available).
    - `ListBooksByGenre(string genre)`: Display all books of a specified genre.
    - `BorrowBook(string title)`: Search for a book by title. If the book is found and not borrowed, call its `Borrow()` method. Otherwise, display a message that it’s unavailable.
    - `ReturnBook(string title)`: Search for a book by title and, if it’s borrowed, call its `ReturnBook()` method to make it available again.

3. **Patron Class**
   - Create a `Patron` class to represent a library user with the following properties:
    - `Name` (string): The name of the patron.
    - `BorrowedBooks` (List<Book>): A list of books borrowed by this patron.
   - Add methods to:
    - `BorrowBook(Book book)`: Add a book to the `BorrowedBooks` list.
    - `ReturnBook(Book book)`: Remove a book from `BorrowedBooks`.

4. **Library System Class**
   - Create a `LibrarySystem` class to manage patrons and the library with the following properties:
    - `Library` (Library): An instance of the Library class.
    - `Patrons` (List<Patron>): A list of patrons.
   - Add methods to:
    - `AddPatron(Patron patron)`: Add a patron to the system.
    - `BorrowBook(string patronName, string title)`: Search for the patron by name and borrow the book by title, if available.
    - `ReturnBook(string patronName, string title)`: Search for the patron by name and return the book by title, if it’s borrowed by the patron.
    - `ListBorrowedBooks(string patronName)`: Display all books currently borrowed by a specific patron.

5. **Main Class**
   - In the `Main` method:
    - Create an instance of `LibrarySystem`.
    - Add a few books to the library.
    - Register some patrons.
    - Borrow and return books for patrons, and display borrowed books.
    - Use `ListBooksByGenre` to display books by genre.


### 2.Student Management System

1. **Student Class**
   - Create a class called `Student` with the following properties:
     - `Name` (string): The name of the student.
     - `Age` (int): The student’s age.
     - `Grades` (List<Grade>): A list of grades, where each grade includes a subject and a score.
   - Add a constructor to initialize the student’s name and age, with an empty list for `Grades`.
   - Add methods to:
     - `AddGrade(string subject, int score)`: Add a grade for a specific subject. If a grade for the subject already exists, update the score.
     - `GetAverageGrade()`: Calculate and return the student’s average grade across all subjects.
     - `ListGrades()`: Display the student’s grades for each subject.

2. **Grade Class**
   - Create a class called `Grade` with the following properties:
     - `Subject` (string): The name of the subject.
     - `Score` (int): The grade or score the student received.
   - Use this class to represent individual grades within the `Grades` list in the `Student` class.

3. **School Class**
   - Create a class called `School` with a list of students (`List<Student>`).
   - Add methods to:
     - `AddStudent(Student student)`: Add a new student to the list.
     - `ListStudents()`: Display all students with their names, ages, and average grades.
     - `FindStudent(string name)`: Search for a student by name. If found, display their grades for each subject and their average grade.
     - `ListTopStudents(int topN)`: Display the top N students with the highest average grades.

4. **SchoolReport Class**
   - Create a class called `SchoolReport` to generate reports for students with the following methods:
     - `GenerateReportCard(Student student)`: Generate a detailed report card for a single student, listing their grades and average grade.
     - `GenerateOverallReport(School school)`: Generate a summary report of all students in the school, showing each student’s name, age, and average grade.

5. **Main Class**
   - In the `Main` method:
     - Create an instance of `School`.
     - Add a few students with grades in multiple subjects.
     - Use `ListStudents()` to display a list of all students with their average grades.
     - Use `FindStudent()` to look up a specific student by name and display their grade report.
     - Use `ListTopStudents()` to display the top N students with the highest average grades.
     - Generate a detailed report for a specific student and an overall report for the school.


### 3. Advanced Task Management System

#### Context
In this exercise, you will create a Task Management System where users can create and manage tasks, assign them to team members, set deadlines, and manage task priorities.

#### Instructions

1. **Enum for Task Priority**
   - Create an `enum` called `PriorityLevel` with the following values:
     - `Low`
     - `Medium`
     - `High`
     - `Critical`

2. **Task Class**
   - Create a class called `Task` with the following properties:
     - `Title` (string): The title of the task.
     - `Description` (string): A brief description of the task.
     - `AssignedTo` (string): The name of the team member assigned to the task.
     - `DueDate` (DateTime): The deadline for the task.
     - `Priority` (PriorityLevel): The priority level of the task.
     - `IsCompleted` (bool): Indicates whether the task is completed.
   - Initialize all properties in a constructor, with `IsCompleted` set to `false` by default.
   - Add methods to:
     - `CompleteTask()`: Mark the task as completed by setting `IsCompleted` to `true`.
     - `IsOverdue()`: Check if the current date is past `DueDate` and if `IsCompleted` is `false`. If so, return `true`; otherwise, return `false`.

3. **Project Class**
   - Create a class called `Project` with the following properties:
     - `Name` (string): The name of the project.
     - `Tasks` (List<Task>): A list of tasks associated with the project.
   - Add methods to:
     - `AddTask(Task task)`: Add a task to the `Tasks` list.
     - `GetOverdueTasks()`: Return a list of all overdue tasks in the project.
     - `ListTasksByPriority(PriorityLevel priority)`: Display all tasks that match the specified priority level.
     - `CompleteTask(string title)`: Find and mark a task with the given title as completed.
     - `ListTasksByUser(string user)`: Display all tasks in the project assigned to a specific user.
     - `ListCompletedTasks()`: Display all tasks in the project that are marked as completed.

4. **TaskManager Class**
   - Create a class called `TaskManager` to manage multiple projects with the following properties:
     - `Projects` (List<Project>): A list of projects within the task management system.
   - Add methods to:
     - `AddProject(Project project)`: Add a new project to the system.
     - `GetTasksByAssignedUser(string user)`: Return a list of all tasks assigned to a specific user across all projects.
     - `ListAllOverdueTasks()`: Display all overdue tasks across all projects.
     - `ListAllTasksByPriority(PriorityLevel priority)`: Display all tasks across all projects that match the specified priority level.
     - `ListTasksByProject(string projectName)`: Display all tasks within a specific project.
     - `GenerateReport()`: Display a summary report listing each project with its total number of tasks, completed tasks, and overdue tasks.

5. **TaskReport Class**
   - Create a class called `TaskReport` with methods to generate detailed reports for tasks:
     - `GenerateProjectReport(Project project)`: Display a detailed report of all tasks in a project, grouped by status (completed, overdue, or pending).
     - `GenerateUserReport(string user, TaskManager taskManager)`: Display all tasks assigned to a specific user across all projects, listing completed, overdue, and pending tasks.
     - `GenerateSystemReport(TaskManager taskManager)`: Generate an overview of all projects, showing the total tasks, overdue tasks, and completed tasks for each project.

6. **Main Class**
   - In the `Main` method:
     - Create an instance of `TaskManager`.
     - Add a few projects with tasks, setting a variety of priorities, deadlines, and assigned users.
     - Display a list of all overdue tasks.
     - List all tasks by priority (e.g., showing only high-priority tasks).
     - List all tasks assigned to a specific user across all projects.
     - Use `GenerateReport()` from `TaskManager` to show a summary of all projects.
     - Use `GenerateProjectReport` and `GenerateUserReport` in `TaskReport` for specific projects and users.


--- 

![](https://media0.giphy.com/media/v1.Y2lkPTc5MGI3NjExZjRjaXRpMTllb3FqYjgyNmlod3FpaXh3eGI5Zmw0YWViaThmMzF4aSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/ZdZzsgdkDB91ZPKwdW/giphy.gif)