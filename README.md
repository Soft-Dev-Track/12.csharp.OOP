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

n C#, composition and aggregation define how objects relate to one another, and both are examples of "has-a" relationships. Let’s explore each concept in detail to understand when and why to use them.

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
     - `Title` (string)
     - `Author` (string)
     - `IsBorrowed` (boolean, indicating if the book is borrowed)
   - Add a constructor to initialize the title and author of a book, setting `IsBorrowed` to `false` by default.
   - Add a method `Borrow()` that checks if the book is already borrowed. If it is not, set `IsBorrowed` to `true` and indicate that the book has been borrowed. If the book is already borrowed, display an appropriate message.

2. **Library Class**
   - Create a class called `Library` with a list of books (`List<Book> Books`).
   - Add a method `AddBook(Book book)` to add a book to the list.
   - Add a method `ListBooks()` to display all books in the library, showing their status (borrowed or available).
   - Add a method `BorrowBook(string title)` to search for a book by title. If the book is found and is not already borrowed, it is borrowed by calling its `Borrow()` method.

3. **Main Class**
   - In the `Main` method, create an instance of `Library`.
   - Add a few books to the library.
   - Display the list of books.
   - Try to borrow a book.
   - Re-display the list to verify the book's borrowed status.

### 2.Student Management System

#### Context
You will create a simple Student Management System where you can add, display, and manage students' grades in different subjects. 

#### Instructions

1. **Student Class**
   - Create a class called `Student` with the following properties:
     - `Name` (string) – the name of the student.
     - `Age` (int) – the student’s age.
     - `Grades` (Dictionary<string, int>) – a dictionary where each key is a subject (string) and each value is a grade (int).
   - Add a constructor to initialize the student’s name and age. Initialize `Grades` as an empty dictionary.
   - Add a method `AddGrade(string subject, int grade)` to add a grade for a specific subject. If the subject already exists in the dictionary, update the grade.
   - Add a method `GetAverageGrade()` to calculate and return the average grade across all subjects.

2. **School Class**
   - Create a class called `School` with a list of students (`List<Student> Students`).
   - Add a method `AddStudent(Student student)` to add a new student to the list.
   - Add a method `ListStudents()` that displays all students’ names, ages, and average grades.
   - Add a method `FindStudent(string name)` that searches for a student by name. If found, display their grades in each subject and their average grade.

3. **Main Class**
   - In the `Main` method, create an instance of `School`.
   - Add a few students to the school and add grades for multiple subjects.
   - Display the list of all students with their average grades.
   - Search for a specific student by name and display their detailed grade report.

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

2. **Class Task**
   - Create a class called `Task` with the following properties:
     - `Title` (string) – the title of the task.
     - `Description` (string) – a brief description of the task.
     - `AssignedTo` (string) – the name of the person assigned to the task.
     - `DueDate` (DateTime) – the deadline for the task.
     - `Priority` (PriorityLevel) – the priority of the task.
     - `IsCompleted` (bool) – a boolean indicating whether the task is completed.
   - Add a constructor to initialize all properties except `IsCompleted`, which should default to `false`.
   - Add a method `CompleteTask()` to set `IsCompleted` to `true`.
   - Add a method `IsOverdue()` that returns `true` if the current date is past the task’s `DueDate` and the task is not completed; otherwise, it returns `false`.

3. **Class Project**
   - Create a class called `Project` with the following properties:
     - `Name` (string) – the name of the project.
     - `Tasks` (List<Task>) – a list of tasks within the project.
   - Add a method `AddTask(Task task)` to add a task to the project.
   - Add a method `GetOverdueTasks()` that returns a list of all overdue tasks in the project.
   - Add a method `ListTasksByPriority(PriorityLevel priority)` to display all tasks that match a specified priority level.
   - Add a method `CompleteTask(string title)` that marks a task with a specific title as completed.

4. **Class TaskManager**
   - Create a class called `TaskManager` to manage multiple projects with the following properties:
     - `Projects` (List<Project>) – a list of projects.
   - Add a method `AddProject(Project project)` to add a new project.
   - Add a method `GetTasksByAssignedUser(string user)` to return a list of all tasks assigned to a specific user across all projects.
   - Add a method `ListAllOverdueTasks()` to display all overdue tasks across all projects.
   - Add a method `ListAllTasksByPriority(PriorityLevel priority)` to display all tasks across all projects that match a given priority.

5. **Main Class**
   - In the `Main` method:
     - Create an instance of `TaskManager`.
     - Add a few projects and tasks with varying priorities, deadlines, and assigned users.
     - Display the list of all overdue tasks.
     - List all tasks by a specific priority level.
     - Find and list all tasks assigned to a particular user.