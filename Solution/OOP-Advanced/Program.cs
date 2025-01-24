using OOP_Advanced.LSM;

namespace OOP_Advanced
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book_one = new("The Eternal Journey", "Amelia Rivers", "Adventure");
            Book book_two = new("Shadows of the Past", "Liam Harper", "Mystery");
            Book book_three = new("Echoes of Eternity", "Sophia Blackwell", "Fantasy");
            Book book_four = new("Through the Whispering Woods", "Eliza Montgomery", "Romance");
            Book book_five = new("The Quantum Paradox", "Dr. Victor Blake", "Science Fiction");
            Book book_six = new("Beneath Crimson Skies", "Catherine Voss", "Historical Fiction");
            Book book_seven = new("Fragments of Tomorrow", "Elliot Grayson", "Thriller");
            Book book_eight = new("The Art of Letting Go", "Julia Bennett", "Self-Help");
            Book book_nine = new("Dancing with the Wind", "Olivia Caldwell", "Poetry");
            Book book_ten = new("The Last City", "Marcus Kane", "Dystopian Fiction");

            Library library = new Library();

            library.books.Add(book_one);
            library.books.Add(book_two);
            library.books.Add(book_three);

            try
            {
                library.BorrowBook("The Eternal Journey");
            }catch(Exception ec)
            {
                Console.WriteLine(ec.Message);
            }

            foreach (Book book in library.GetBooks()) 
            {
                Console.Write($"Title: {book.Title}");
                Console.Write($" - Author: {book.Author}");
                Console.Write($" - Genre: {book.Genre}");
                Console.Write((book.IsBorrowed) ? " - No Available" : " - Available");
                Console.WriteLine("");
            }

            Console.WriteLine("Books by genre");

            try
            {
                foreach (Book book in library.ListBooksByGenre("Love"))
                {
                    Console.Write($"Title: {book.Title}");
                    Console.Write($" - Author: {book.Author}");
                    Console.Write($" - Genre: {book.Genre}");
                    Console.Write((book.IsBorrowed) ? " - No Available" : " - Available");
                    Console.WriteLine("");
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }

          

        }
    }
}
