using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_1.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="Id je obavezan")]
        public int CategoryId { get; set; }
        [DisplayName("Naziv kategorije")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Name { get; set; } = "";
        [DisplayName("Sifra kategorije")]
        [Required(ErrorMessage = "Sifra je obavezna")]
        public string Code { get; set; } = "";
        [DisplayName("Aktivna")]
        public bool Active { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
