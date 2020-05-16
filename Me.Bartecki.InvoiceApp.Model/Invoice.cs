using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Me.Bartecki.InvoiceApp.Model
{
    public class Invoice        
    {
        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        /// <summary>
        /// This property would be in a separate DTO class
        /// </summary>
        [NotMapped]
        public string ClientName => Client.FullName;
        public virtual Client Client { get; set; }
        public DateTime SellDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int Paytime { get; set; }
        public PaymentType PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public virtual IEnumerable<InvoiceRow> InvoiceRows { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrossTotal { get; set; }
    }
}
