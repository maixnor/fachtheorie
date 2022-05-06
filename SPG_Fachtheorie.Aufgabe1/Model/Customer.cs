using Microsoft.Extensions.Primitives;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Customer
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Salutation { get; set; }        
        public string Address { get; set; }
        public string Street { get; set; }
        public int CustomerNumber { get; set; } // TODO autoinc

        public List<Invoice> Invoices { get; set; }
    }
}
