using System;
using System.ComponentModel.DataAnnotations;

namespace Final.Models.BindingModels
{
    public class EventFormModel
    {
        [Required, StringLength(100)]
        public string? Title { get; set; }

        [Required, StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(100)]
        public string? Location { get; set; }

        [Range(1, 10000)]
        public int Capacity { get; set; }
    }
}