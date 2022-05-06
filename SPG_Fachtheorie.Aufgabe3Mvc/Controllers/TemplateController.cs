using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers;

public class TemplateController : Controller
{
    private readonly AppointmentContext _db;

    public TemplateController(AppointmentContext db)
    {
        _db = db;
    }

    public IActionResult Dropdown()
    {
        ViewBag.ValuesToSelectFrom = new List<SelectListItem>
        {
            // inspect html: option fields
            new("first Value", "1"),
            new("second Value", "2"),
            new("third Value", "third"),
            new("fourth Value", "numero-quattro")
        };
        ViewBag.ValuesFromEntityFramework = new SelectList(_db.Students, "Id", "Email");
        
        return View();
    }

    public record TemplateDropdownDto(string Value, string EFValue);
    
    [HttpPost]
    public IActionResult Dropdown(TemplateDropdownDto dto)
    {
        // debug here to see Value and EFValue passed down within DTO,
        // not visible in browser
        ViewBag.ValuesToSelectFrom = new List<SelectListItem>
        {
            new SelectListItem("firstValue", "1"),
            new SelectListItem("secondValue", "2"),
            new SelectListItem("thirdValue", "third"),
            new SelectListItem("fourthValue", "numero-quattro"),
        };
        ViewBag.ValuesFromEntityFramework = new SelectList(_db.Subjects, "Id", "Name");

        return View();
    }
    
}