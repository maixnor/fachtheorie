namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Employee
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }

        public List<Invoice> Invoices { get; set; }
    }
}
