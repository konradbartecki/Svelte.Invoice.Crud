using System.ComponentModel.DataAnnotations;

namespace Me.Bartecki.InvoiceApp.Model
{
    public class InvoiceRow
    {
        [Key]
        public int Id { get; set; }
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