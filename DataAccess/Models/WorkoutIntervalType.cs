namespace DataAccess.Models
{
   using System.ComponentModel.DataAnnotations;

   public class WorkoutIntervalType
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
