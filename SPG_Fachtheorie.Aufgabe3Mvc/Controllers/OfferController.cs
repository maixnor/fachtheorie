using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3Mvc.Controllers
{
    public class OfferController : Controller
    {
        private readonly AppointmentContext _db;

        public OfferController(AppointmentContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Offer> model = _db
                .Offers
                .Include(o => o.Subject)
                .Include(o => o.Appointments)
                .ToList();
            return View(model);
        }

        public IActionResult Detail(Guid id)
        {
            Offer model = _db
                .Offers
                .Include(o => o.Appointments)
                .ThenInclude(a => a.Student)
                .SingleOrDefault(o => o.Id == id);
            return View(model);
        }

        [HttpGet()]
        public IActionResult Add()
        {
            ViewData["subjects"] = new SelectList(_db.Subjects, "Id", "Name");
            ViewData["teachers"] = new SelectList(_db.Students.Where(s => s is Coach), "Id", "Lastname");
            return View();
        }
        [HttpPost()]
        public IActionResult Add(NewOfferDto model)
        {
            // Init
            ViewData["subjects"] = new SelectList(_db.Subjects, "Id", "Name");
            ViewData["teachers"] = new SelectList(_db.Students.Where(s => s is Coach), "Id", "Lastname");
            Subject subject = _db
                .Subjects
                .SingleOrDefault(s => s.Id == model.SubjectId);
            if (subject is null)
            {
                ModelState.AddModelError("", "Subject not found!");
            }
            Student coach = _db.Students.SingleOrDefault(s => s.Id == model.TeacherId);
            if (subject is null)
            {
                ModelState.AddModelError("", "Teacher not found!");
            }

            if (ModelState.IsValid)
            {
                // Validation
                if (model.From <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Von-Datum darf nicht in der Vergangenheit liegen!");
                    return View(model);
                }

                // Act
                Offer newOffer = new Offer() 
                { 
                    Id = Guid.NewGuid(), 
                    From = model.From, 
                    To = model.To, 
                    SubjectId = subject.Id, 
                    TeacherId = coach.Id
                };
                _db.Offers.Add(newOffer);

                // Save
                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(500);
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500);
                }
                return RedirectToAction("Index", "Offer");
            }
            return View(model);
        }

        [HttpGet()]
        public IActionResult Edit(Guid id)
        {
            Offer offer = _db
                .Offers
                .SingleOrDefault(s => s.Id == id);
            if (offer is null)
            {
                ModelState.AddModelError("", "Offer not found!");
            }
            EditOfferDto editOfferDto = new EditOfferDto() 
            {
                Id = id, 
                To = offer.To 
            };
            return View(editOfferDto);
        }
        [HttpPost()]
        public IActionResult Edit(Guid id, EditOfferDto model)
        {
            // Init
            Offer offer = _db
                .Offers
                .SingleOrDefault(s => s.Id == id);
            if (offer is null)
            {
                ModelState.AddModelError("", "Offer not found!");
            }

            if (ModelState.IsValid)
            {
                // Validation
                if (model.To <= offer.To)
                {
                    ModelState.AddModelError("", "Bis-Datum kann nur verlängert werden!");
                    return View(model);
                }

                // Act
                offer.To = model.To;
                _db.Offers.Update(offer);

                // Save
                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(500);
                }
                catch (DbUpdateException)
                {
                    return StatusCode(500);
                }
                return RedirectToAction("Index", "Offer");
            }
            return View(model);
        }

        [HttpGet()]
        public IActionResult Delete(Guid id)
        {
            bool canDelete = true;
             string message = $"Really wanna delete offer {id}?";
            Offer offer = _db
                .Offers
                .Include(o=>o.Appointments)
                .SingleOrDefault(s => s.Id == id);
            if (offer is null)
            {
                ModelState.AddModelError("", "Offer not found!");
            }
            if (offer.Appointments.Any())
            {
                message = "Offer with Appintments cannot be deleted!";
                canDelete = false;
            }
            return View(new DeleteOfferDto()
            {
                Id = id,
                Message = message,
                CanDelete = canDelete
            });
        }
        [HttpGet()]
        public IActionResult ConfirmDelete(Guid id)
        {
            // Init
            Offer offer = _db.Offers.SingleOrDefault(s => s.Id == id);
            if (offer is null)
            {
                ModelState.AddModelError("", "Offer not found!");
            }

            // Validate
            // ...

            // Act
            _db.Offers.Remove(offer);

            // Save
            try
            {
                _db.SaveChanges();
                return RedirectToAction("Index", "Offer");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }
        }
    }
}
