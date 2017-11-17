using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class BooksModel
    {
        public Dictionary<string, BookSummary> BookSummaries { get; set; }
        public List<Book> books { get; set; }
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string InputText { get; set; }
    }
    
}