using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_1.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="Id je obavezan")]
        public int ProductId { get; set; }
        [DisplayName("Naziv proizvoda")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Name { get; set; } = "";
        [DisplayName("Sifra proizvoda")]
        [Required(ErrorMessage = "Sifra je obavezna")]
        public string Code { get; set; } = "";
       
        [DisplayName("Cena")]
        [Required(ErrorMessage = "Cena je obavezna")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Cena ne sme da bude manja od 0")]
        public decimal Price { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; } = "";
        [DisplayName("Kategorije")]
        public Category Category { get; set; } = new Category();
        [DisplayName("Slika")]
        public string ImageName { get; set; } = "";
        [DisplayName("Aktivan")]
        public bool Active { get; set; }
        [NotMapped]
        public string ImagePath => "/images/" +ImageName ;
        [NotMapped]
        public IFormFile? NewImage { get; set; }


    }
}
