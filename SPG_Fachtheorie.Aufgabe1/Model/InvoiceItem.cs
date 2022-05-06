namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class InvoiceItem
    {
        public int Id { get; set; } = 0;
        public decimal Price { get; set; }

        public Article Article { get; set; }
        public Invoice Invoice { get; set; }
    }
}
