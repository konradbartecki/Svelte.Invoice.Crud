using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Me.Bartecki.InvoiceApp.Model;

namespace Me.Bartecki.InvoiceApp.Web.Data
{
    public class MeBarteckiInvoiceAppWebContext : DbContext
    {
        public MeBarteckiInvoiceAppWebContext (DbContextOptions<MeBarteckiInvoiceAppWebContext> options)
            : base(options)
        {            
        }

        public DbSet<Me.Bartecki.InvoiceApp.Model.Invoice> Invoice { get; set; }
    }
}
