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

- Type Safety: `List<T>` is generic, meaning it holds elements of a specific type (`T`), ensuring type safety. For example, `List<int>` can only contain integers, while `List<string>` can only contain strings.

## 3. Composition vs agregation

## 4. Overloading Constructors

