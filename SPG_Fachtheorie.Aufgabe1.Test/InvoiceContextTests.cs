using Xunit;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Test;

/// <summary>
/// Unittests f�r den DBContext.
/// Die Datenbank wird im Ordner SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe1.Test\bin\Debug\net6.0\Invoice.db
/// erzeugt und kann mit SQLite Management Studio oder DBeaver betrachtet werden
/// </summary>
public class InvoiceContextTests
{
    /// <summary>
    /// Pr�ft, ob die Datenbank mit dem Model im InvoiceContext angelegt werden kann.
    /// </summary>
    [Fact]
    public void CreateDatabaseTest()
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=Invoice.db")
            .Options;

        using var db = new InvoiceContext(options);
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var article = new Article()
        {
            Price = 0.3m,
            ArticleNumber = "69",
            DisplayName = "Drechsler"
        };
        db.Articles.Add(article);
        db.SaveChanges();
    }
}