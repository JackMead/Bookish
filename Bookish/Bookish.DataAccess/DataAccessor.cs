using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Dapper;

namespace Bookish
{
    public class DataAccessor
    {
        public List<Book> GetListOfAllBooks()
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString);
            string SqlString = "SELECT * FROM [Books]";
            var books = (List<Book>)db.Query<Book>(SqlString);
            return books;
        }

        public List<Book> GetBooksForUser(string UserId)
        {
            IDbConnection db = GetDBConnection();
            string SqlString = "SELECT Author,Title,ISBN,CopyNumber,DueDate FROM Books WHERE UserId='" + UserId + "'";
            var books = (List<Book>)db.Query<Book>(SqlString);
            return books;
        }

        public List<Book> GetBooksByTitle(string title)
        {
            var books = new List<Book>();
            using (var connection = (SqlConnection)GetDBConnection())
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT Author,Title FROM Books WHERE Title = @title";
                SqlParameter authorParam = new SqlParameter("@title", SqlDbType.NVarChar, 100) { Value = title };
                command.Parameters.Add(authorParam);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        books.Add(new Book { Author = reader["Author"].ToString(), Title = reader["Title"].ToString() });
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return books;
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            //TODO Accept subset eg. second name?
            // Create and prepare an SQL statement.
            var books = new List<Book>();
            using (var connection = (SqlConnection)GetDBConnection())
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT Author,Title FROM Books WHERE Author = @author";
                SqlParameter authorParam = new SqlParameter("@author", SqlDbType.NVarChar, 100) { Value = author };
                command.Parameters.Add(authorParam);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        books.Add(new Book{Author = reader["Author"].ToString(), Title = reader["Title"].ToString()});
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return books;
        }

        public void AddNewBook(Book book)
        {
            var db = GetDBConnection();
            //TODO Change SQL int type for isbn to take 13 digit ints. String better?
            var isbn = book.Isbn > int.MaxValue ? int.MaxValue : book.Isbn;
            string SqlString = "INSERT INTO Books(Author, Title, ISBN, CopyNumber) VALUES('" + book.Author + "','" + book.Title + "'," + isbn + "," + book.CopyNumber + ")";
            var books = (List<Book>)db.Query<Book>(SqlString);
        }

        public void TakeOutBook(Book book, string userId)
        {
            var db = GetDBConnection();
            string sqlFormattedDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss.fff");
            string SqlString = "UPDATE Books SET UserId='" + userId + "', DueDate='" + sqlFormattedDate + "' WHERE CopyNumber=" + book.CopyNumber + " AND ISBN=" + book.Isbn;
            var books = (List<Book>)db.Query<Book>(SqlString);
        }

        public void ReturnBook(Book book)
        {
            //Execute command rather than query
            var db = GetDBConnection();
            string SqlString = "UPDATE Books SET UserId=NULL, DueDate=NULL WHERE CopyNumber=" + book.CopyNumber + " AND ISBN=" + book.Isbn;
            db.Query(SqlString);
        }

        public List<Book> SearchBooks(string searchText)
        {
            var books = new List<Book>();
            using (var connection = (SqlConnection)GetDBConnection())
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT * FROM Books WHERE Author LIKE '%'+@searchText+'%' OR Title LIKE '%'+@searchText+'%'";
                SqlParameter searchParam = new SqlParameter("@searchText", SqlDbType.NVarChar) { Value = searchText };
                command.Parameters.Add(searchParam);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        {
                            Author = reader["Author"].ToString(),
                            Title = reader["Title"].ToString(),
                            CopyNumber = int.Parse(reader["CopyNumber"].ToString()),
                            Isbn = int.Parse(reader["Isbn"].ToString())
                            
                        };
                        if (string.IsNullOrEmpty(reader["UserId"].ToString()))
                        {
                            book.UserId = reader["UserId"].ToString();
                        }
                        if (DateTime.TryParse(reader["DueDate"].ToString(), out DateTime date))
                        {
                            book.DueDate = date;
                        }

                        books.Add(book);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return books;
        }

        public IDbConnection GetDBConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString);
        }
    }
}
