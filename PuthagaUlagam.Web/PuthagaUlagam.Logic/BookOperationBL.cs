using PuthagaUlagam.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PuthagaUlagam.Logic
{
    public class BookOperationBL
    {
        private readonly ApiResponse<bool> apiResponse = new ApiResponse<bool>();

        public ApiResponse<bool> AddBook(BookDTO bookDto)
        {
            return AddOrUpdateBook(OperationType.Add, bookDto);
        }

        public ApiResponse<bool> UpdateBook(BookDTO bookDto)
        {
            return AddOrUpdateBook(OperationType.Update, bookDto);
        }

        private ApiResponse<bool> AddOrUpdateBook(OperationType operationType, BookDTO bookDto)
        {
            string query = operationType == OperationType.Add
                ? "INSERT INTO Book (BookISBN, BookName, BookAuthor ,DateOfPublication, BookPrice, BookCount) VALUES (@BookISBN, @BookName, @BookAuthor ,@DateOfPublication, @BookPrice, @BookCount)"
                : "UPDATE Book SET BookName = @BookName, BookAuthor = @BookAuthor, BookPrice = @BookPrice, DateOfPublication = @DateOfPublication, BookCount = @BookCount WHERE BookISBN = @BookISBN";

            using (SqlConnection con = new SqlConnection("Server=MS-NB0009; Database=book; User Id=sa; Password=password-123;"))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@BookISBN", bookDto.ISBN);
                cmd.Parameters.AddWithValue("@BookName", bookDto.Title);
                cmd.Parameters.AddWithValue("@BookAuthor", bookDto.Author);
                cmd.Parameters.AddWithValue("@DateOfPublication", bookDto.Date);
                cmd.Parameters.AddWithValue("@BookPrice", bookDto.Price);
                cmd.Parameters.AddWithValue("@BookCount", bookDto.Count);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = Messages.BookAddSuccess;
                }
                else
                {
                    apiResponse.Message = Messages.BookAddFail;
                }
            }
            return apiResponse;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            string query = "SELECT * FROM Book";
            using (SqlConnection con = new SqlConnection("Server=MS-NB0009; Database=book; User Id=sa; Password=password-123;"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = Convert.ToInt32(reader["BookID"]),
                        ISBN = Convert.ToInt32(reader["BookISBN"]),
                        Title = reader["BookName"].ToString(),
                        Author = reader["BookAuthor"].ToString(),
                        Date = Convert.ToDateTime(reader["DateOfPublication"]),
                        Price = Convert.ToDecimal(reader["BookPrice"]),
                        Count = Convert.ToInt32(reader["BookCount"])
                    });
                }
            }
            return books;
        }

        public void DeleteBook(int bookIsbn)
        {
            string query = "DELETE FROM Book WHERE BookISBN = @ISBN";
            using (SqlConnection con = new SqlConnection("Server=MS-NB0009; Database=book; User Id=sa; Password=password-123;"))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                var books = GetBooks();
                var bookToDelete = books.FirstOrDefault(b => b.ISBN == bookIsbn);

                if (bookToDelete == null)
                {
                    throw new InvalidOperationException("Book not found");
                }

                cmd.Parameters.AddWithValue("@ISBN", bookToDelete.ISBN);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Book GetBookByIsbn(int bookIsbn)
        {
            Book book = null;

            using (SqlConnection connection = new SqlConnection("Server=MS-NB0009; Database=book; User Id=sa; Password=password-123;"))
            {
                string query = "SELECT * FROM Book WHERE BookISBN = @ISBN";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ISBN", bookIsbn);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    book = new Book
                    {
                        ISBN = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        Date = reader.GetDateTime(3),
                        Price = reader.GetDecimal(4),
                        Count = reader.GetInt32(5)
                    };
                }
            }
            return book;
        }

        public ApiResponse<bool> UniqueIsbnValidation(int bookIsbn)
        {
            using (SqlConnection connection = new SqlConnection("Server=MS-NB0009; Database=book; User Id=sa; Password=password-123;"))
            {
                string query = "SELECT COUNT(*) FROM Book WHERE BookISBN = @ISBN";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ISBN", bookIsbn);
                connection.Open();
                int nCount = (int)cmd.ExecuteScalar();
                if (nCount == 0)
                {
                    apiResponse.IsSuccess = true;
                    return apiResponse;
                }
                apiResponse.Message = Messages.ISBNAlreadyExist;
                return apiResponse;
            }
        }
    }
}