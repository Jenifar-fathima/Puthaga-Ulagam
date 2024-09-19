using PuthagaUlagam.Common;
using PuthagaUlagam.Data;
using System.Collections.Generic;
using System.Linq;

namespace PuthagaUlagam.Logic
{
    public class BookOperationBL
    {
        ApiResponse<bool> apiResponse = new ApiResponse<bool>();
        public ApiResponse<bool> AddBook(BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                return apiResponse;
            }
            apiResponse.IsSuccess = true;
            var bookDetails = new Book(bookDTO);
            DataContext.Books.Add(bookDetails);
            return apiResponse;
        }

        public List<Book> GetBooks()
        {
            return DataContext.Books;
        }

        public void DeleteBook(int nRowIndex)
        {
            DataContext.Books.RemoveAt(nRowIndex);
        }

        public ApiResponse<bool> UpdateBook(BookDTO bookDto)
        {
            var bookToUpdate = DataContext.Books
                .FirstOrDefault(b => b.ISBN == bookDto.ISBN);

            if (bookToUpdate == null)
            {
                apiResponse.Message = Messages.ISBNAlreadyExist;
                return apiResponse;
            }

            apiResponse.IsSuccess = true;
            bookToUpdate.Title = bookDto.Title;
            bookToUpdate.Author = bookDto.Author;
            bookToUpdate.Price = bookDto.Price;
            bookToUpdate.Date = bookDto.Date;
            bookToUpdate.Count = bookDto.Count;
            return apiResponse;
        }

        public Book GetBookById(int bookIndex)
        {
            return DataContext.Books[bookIndex];
        }
    }
}