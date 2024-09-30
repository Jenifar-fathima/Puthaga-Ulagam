using PuthagaUlagam.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PuthagaUlagam.Logic
{
    public class BookOperationBL
    {
        private readonly ApiResponse<bool> apiResponse = new ApiResponse<bool>();

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BookDbConnection"].ConnectionString;
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
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string storedProcedureName = "AddOrUpdateBook";

                SqlCommand cmd = new SqlCommand(storedProcedureName, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OperationType", operationType == OperationType.Add ? "Add" : "Update");
                cmd.Parameters.AddWithValue("@BookISBN", bookDto.ISBN);
                cmd.Parameters.AddWithValue("@BookName", bookDto.Title);
                cmd.Parameters.AddWithValue("@BookAuthor", bookDto.Author);
                cmd.Parameters.AddWithValue("@DateOfPublication", bookDto.Date);
                cmd.Parameters.AddWithValue("@BookPrice", bookDto.Price);
                cmd.Parameters.AddWithValue("@BookCount", bookDto.Count);

                SqlDataAdapter adpt = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adpt.Fill(dt);

                apiResponse.IsSuccess = true;
                apiResponse.Message = Messages.BookAddSuccess;
            }
            return apiResponse;
        }


        public DataTable GetBooks()
        {
            DataTable booksTable = new DataTable();
            string query = "SELECT * FROM Book ORDER BY BookID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

                con.Open();

                dataAdapter.Fill(booksTable);
            }
            return booksTable;
        }

        public void DeleteBook(int bookIsbn)
        {
            string query = "DELETE FROM Book WHERE BookISBN = @ISBN";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ISBN", bookIsbn);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public DataTable GetBookByIsbn(int bookIsbn)
        {
            DataTable bookDataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Book WHERE BookISBN = @ISBN";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ISBN", bookIsbn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(bookDataTable);
            }
            return bookDataTable;
        }

        public ApiResponse<bool> UniqueIsbnValidation(int bookIsbn)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        public DataTable DataFill(DataTable bookDataTable)
        {
            return bookDataTable;
        }
    }
}