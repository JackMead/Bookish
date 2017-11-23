using System;
using System.Collections.Generic;
using RestSharp;

namespace Bookish.DataAccess
{
    public class BookDetailsAccessor
    {
        //TODO Need to reduce to first author most of the time, and remove padding round titles.
        public Book GetBookFromISBN(Int64 isbn)
        {
            var client = "https://www.googleapis.com/books/v1";
            var request = "volumes?q=isbn:" + isbn;
            var response = new RestClient(client).Execute<BookInfo>(new RestRequest(request,Method.GET)).Data;
            var book = new Book();
            if (response.TotalItems != 0)
            {
                var bookDetails = response.Items[0];
                book.Author = bookDetails.volumeInfo.title;
                book.Title = bookDetails.volumeInfo.authors;
                book.Isbn = isbn;
            }

            //TODO should really check copy number etc. here
            
            return book;
        }
    }

    public class BookInfo
    {
        public List<BookData> Items { get; set; }
        public int TotalItems { get; set; }
    }

    public class BookData
    {
        public VolInfo volumeInfo { get; set; }
    }

    public class VolInfo
    {
        public string title { get; set; }
        public string authors { get; set; }
    }



}
