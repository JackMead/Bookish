using System;


namespace Bookish
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbAccess = new DataAccess();
            var books = dbAccess.GetListOfAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }

            Console.ReadLine();
        }
    }
}
