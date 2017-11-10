using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish
{
    public class DataAccess
    {
        public List<Book> GetListOfAllBooks()
        {
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["LibraryConnection"].ConnectionString);
            string SqlString = "SELECT TOP 100 [Author],[Title],[ISBN],[CopyNumber] FROM [Books]";
            var books = (List<Book>)db.Query<Book>(SqlString);
            return books;
        }
    }
}
