using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string? Title { get; set; }

        [Required, StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(100)]
        public string? Location { get; set; }

        public int Capacity { get; set; }

        // Track the user who created the event
        public string CreatedBy { get; set; } = "";
        
        // Ek alanlar eklenebilir (ör: kayıtlı kullanıcılar vs.)
        
    }
}