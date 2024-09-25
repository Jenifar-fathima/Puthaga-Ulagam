using System;

namespace PuthagaUlagam.Common
{
    public class BookDTO
    {
        public int ID { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}