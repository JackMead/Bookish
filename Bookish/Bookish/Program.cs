﻿using System;
using Bookish.DataAccess;


namespace Bookish
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        public void Run()
        {
            var isbn = 1447146034;
            var book1 = new DataAccess.BookDetailsAccessor().GetBookFromISBN(isbn);
            Console.WriteLine(book1.Title);

            /*
            var dbAccess = new DataAccess.DataAccessor();
            var books = dbAccess.GetListOfAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }

            Console.ReadLine();

            PrintJacksBooks(dbAccess);
            Console.ReadLine();

            Console.WriteLine("Now Jack takes out the second book on the original list");
            dbAccess.TakeOutBook(books[1], "jack");
            PrintJacksBooks(dbAccess);
            Console.WriteLine("This book is due back"+dbAccess.GetBooksForUser("jack")[0].DueDate);
            Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Now Jack returns that book");
            dbAccess.ReturnBook(books[1]);
            PrintJacksBooks(dbAccess);
            Console.ReadLine();
            */
        }

        public void PrintJacksBooks(DataAccessor dbAccess)
        {
            var jackBooks = dbAccess.GetBooksForUser("jack");
            Console.WriteLine("Printing Jack's Books:");
            foreach (var book in jackBooks)
            {
                Console.WriteLine("Now Jack has the book:" + book.Title);
            }
        }
    }
}
