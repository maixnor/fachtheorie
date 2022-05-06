namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Article
    {
        public int Id { get; set; } = 0;
        public string ArticleNumber { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }        
    }
}
