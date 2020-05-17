using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Me.Bartecki.InvoiceApp.Model
{
    public class InvoiceRow
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public string ItemName { get; set; }       
        public string UnitName { get; set; }       
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        /// <summary>
        /// Takes in values from 0-100
        /// </summary>
        public int VatRatePercent { get; set; }
    }
}