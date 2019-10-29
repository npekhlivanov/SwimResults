namespace SwimResults.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public string Place { get; set; }

        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000)]
        public string Note { get; set; }
    }
}
