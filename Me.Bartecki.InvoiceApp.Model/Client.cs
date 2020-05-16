using System.ComponentModel.DataAnnotations;

namespace Me.Bartecki.InvoiceApp.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TaxId { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }        
        public string PostalCode { get; set; }
    }
}