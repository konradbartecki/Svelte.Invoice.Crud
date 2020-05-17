using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Me.Bartecki.InvoiceApp.Model;

namespace Me.Bartecki.InvoiceApp.Svelte.Data
{
    public class MeBarteckiInvoiceAppWebContext : DbContext
    {
        public MeBarteckiInvoiceAppWebContext (DbContextOptions<MeBarteckiInvoiceAppWebContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasMany(x => x.InvoiceRows)
                .WithOne(x => x.Invoice);
        }

        public DbSet<Me.Bartecki.InvoiceApp.Model.Invoice> Invoice { get; set; }

        public DbSet<Me.Bartecki.InvoiceApp.Model.InvoiceRow> InvoiceRow { get; set; }

        public DbSet<Me.Bartecki.InvoiceApp.Model.Client> Client { get; set; }
    }
}
