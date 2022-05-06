using Bogus.DataSets;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Invoice
    {
        public int Id { get; set; } = 0;
        public int Discount { get; set; }
        public DateTime IssueDate { get; set; }
        public int InvoiceNumber { get; set; } // TODO autoinc

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
