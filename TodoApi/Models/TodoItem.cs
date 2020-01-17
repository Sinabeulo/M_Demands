using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        ////[Range(minimum: 0.01, maximum: (double) decimal.MaxValue)]
        //public bool IsComplete { get; set; }
    }
}
