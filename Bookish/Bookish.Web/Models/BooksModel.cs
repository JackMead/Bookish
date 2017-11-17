using System.Collections.Generic;

namespace Bookish.Web.Models
{
    public class BooksModel
    {
        public List<Book> books { get; set; }
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string InputText { get; set; }
    }
    
}