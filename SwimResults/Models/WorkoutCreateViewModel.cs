namespace SwimResults.Models
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class WorkoutCreateViewModel
    {
        [Required]
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Place { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Note { get; set; }
    }
}
