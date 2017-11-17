using System.Web.Mvc;
using Bookish.DataAccess;
using Bookish.Web.Models;
using Microsoft.SqlServer.Server;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Jack's Library is the best library.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us and tell us how great we are.";

            return View();
        }

        public ActionResult Catalogue()
        {
            var books = new DataAccessor().GetListOfAllBooks();
            var bookSummaries = new BookOrganiser().GetSummaryOfBooks(books);

            var booksModel = new BooksModel
            {
                BookSummaries = bookSummaries
            };

            return View(booksModel);
        }

        public ActionResult Books(string userId)
        {
            var books = new BooksModel
            {
                books = new DataAccessor().GetBooksForUser(userId)
            };
            return View(books);
        }

        public ActionResult ReturnBook(int isbn, int copyNumber, string userId)
        {
            var book = new Book
            {
                Isbn = isbn,
                CopyNumber = copyNumber
            };
            new DataAccessor().ReturnBook(book);
            return RedirectToAction("Books", new { userId = userId });
        }

        public ActionResult CheckOut(int isbn, int copyNumber, string userId)
        {
            var book = new Book
            {
                Isbn = isbn,
                CopyNumber = copyNumber
            };
            new DataAccessor().TakeOutBook(book, userId);
            return RedirectToAction("Catalogue");
        }

        public ActionResult AddBook(int isbn, string author, string title)
        {
            var book = new Book
            {
                Isbn = isbn,
                Author = author,
                Title = title
            };
            new DataAccessor().AddNewBook(book);
            return RedirectToAction("Catalogue");
        }

        public ActionResult Search(string inputText)
        {
            //Add a search method to data access
            var books = new DataAccessor().GetBooksByAuthor(inputText);
            books.AddRange(new DataAccessor().GetBooksByTitle(inputText));
            var bookSummaries = new BookOrganiser().GetSummaryOfBooks(books);

            var bookModel = new BooksModel
            {
                BookSummaries = bookSummaries
            };

            return View("Catalogue", bookModel);
        }
    }
}