using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class AppointmentService
    {
        private readonly AppointmentContext _db;

        public AppointmentService(AppointmentContext db)
        {
            _db = db;
        }

        public async Task<bool> AskForAppointment(Guid offerId, Guid studentId, DateTime date)
        {
            var offer = await _db.Offers.SingleOrDefaultAsync(x => x.Id == offerId);
            var student = await _db.Students.SingleOrDefaultAsync(t => t.Id == studentId);
            if (offer is null)
                return false;
            
            if (offer.From > date || date > offer.To)
            {
                return false;
            }
            
            var appointment = new Appointment()
            {
                Date = date,
                Offer = offer,
                State = AppointmentState.AskedFor,
                Student = student,
            };

            _db.Appointments.Add(appointment);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ConfirmAppointment(Guid appointmentId)
        {
            var appointment = await _db.Appointments.SingleOrDefaultAsync(t => t.Id == appointmentId);
            if (appointment is null)
                return false;

            if (appointment.State is 
                AppointmentState.Cancelled or 
                AppointmentState.TookPlace or 
                AppointmentState.Confirmed)
            {
                return false;
            }

            appointment.State = AppointmentState.Confirmed;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelAppointment(Guid appointmentId, Guid studentId)
        {
            var appointment = await _db.Appointments.SingleOrDefaultAsync(t => t.Id == appointmentId);
            var student = await _db.Students.SingleOrDefaultAsync(t => t.Id == studentId);

            if (student.IsCoach())
            {
                if (appointment!.State is AppointmentState.AskedFor or AppointmentState.Confirmed)
                {
                    appointment.State = AppointmentState.Cancelled;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (appointment!.State is AppointmentState.AskedFor)
                {
                    appointment.State = AppointmentState.Cancelled;
                }
                else
                {
                    return false;
                }
            }
            
            try
            {
                await _db.SaveChangesAsync();
            }
            catch 
            {
                return false;
            }
            
            return true;
        }
    }
}
