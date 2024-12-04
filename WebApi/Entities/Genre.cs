using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
