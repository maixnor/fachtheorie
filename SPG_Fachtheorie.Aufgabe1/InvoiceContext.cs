using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace SPG_Fachtheorie.Aufgabe1
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions opt) : base(opt) { }

        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Füge - wenn notwendig - noch Konfigurationen hinzu.
            // TODO autoincrement fuer Properties
        }
    }
}
