using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string Seller { get; set; } = "";
        public DateTime PostedAt { get; set; }
        public string? PhotoPath { get; set; } // <-- Fotoğraf yolu eklendi
    }
}