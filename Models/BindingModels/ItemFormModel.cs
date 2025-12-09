using System.ComponentModel.DataAnnotations;

namespace Final.Models.BindingModels
{
    public class ItemFormModel
    {
        [Required, StringLength(100)]
        public string? Title { get; set; }

        [Required, StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
    }
}

