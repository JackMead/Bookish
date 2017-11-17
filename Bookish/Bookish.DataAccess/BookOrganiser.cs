using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class BookOrganiser
    {
        public List<BookSummary> GetSummaryOfBooks(List<Book> books)
        {
            var bookSummary = new Dictionary<string, BookSummary>();
            foreach(Book book in books)
            {
                if (!bookSummary.ContainsKey(book.Title))
                {
                    bookSummary[book.Title] = new BookSummary
                    {
                        Title = book.Title,
                        Author = book.Author,
                        ISBN = book.Isbn,
                        NumberCopies = 0,
                        CopyAvailable = false,
                        FirstCopyNumberAvailable = 0,
                        DateFirstCopyComesAvailable = DateTime.MaxValue
                    };
                }
                bookSummary[book.Title].NumberCopies += 1;
                if (book.UserId == null)
                {
                    bookSummary[book.Title].CopyAvailable = true;
                    bookSummary[book.Title].FirstCopyNumberAvailable =
                        Math.Max(bookSummary[book.Title].FirstCopyNumberAvailable, book.CopyNumber);
                }
                if (book.UserId != null)
                {
                    bookSummary[book.Title].DateFirstCopyComesAvailable = new DateTime[] {bookSummary[book.Title].DateFirstCopyComesAvailable, book.DueDate}.OrderBy(p => p.Date).First();
                }

            }

            return bookSummary;
        }
    }

    public class BookSummary
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int ISBN { get; set; }
        public int NumberCopies { get; set; }
        public bool CopyAvailable { get; set; }
        public int FirstCopyNumberAvailable { get; set; }
        public DateTime DateFirstCopyComesAvailable { get; set; }
    }
}
