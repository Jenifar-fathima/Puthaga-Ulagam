using PuthagaUlagam.Common;
using System;

namespace PuthagaUlagam.Data
{
    public class Book
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public Book(BookDTO bookDTO)
        {
            Title = bookDTO.Title;
            Author = bookDTO.Author;
            ISBN = bookDTO.ISBN;
            Price = bookDTO.Price;
            Date = bookDTO.Date;
            Count = bookDTO.Count;
        }
    }
}