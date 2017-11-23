using System;
using System.Collections.Generic;

namespace Bookish
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Int64 Isbn { get; set; }
        public int CopyNumber { get; set; }
        public string UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
