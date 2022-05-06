using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class AppointmentServiceTests
    {
        private Guid offerId = new Guid("132EFFFA-2050-28B7-C0B6-FF7A95A6039A");
        private Guid appointmentId = new Guid("015E3149-A6AD-2B93-2212-A69801E93A82");
        private Guid appointmentIdInvalidState = new Guid("0032BB90-FED0-ECCD-BE47-8D207FF56934");
        private Guid studentId = new Guid("629C1D06-8D1C-855F-12BE-03C28C94E049");
        //private Guid studentIdInvalidState = new Guid("14E2E16C-11CC-4192-3F9A-9E20005186ED");
        private DateTime date = new DateTime(2021, 10, 10);

        /// <summary>
        /// Legt die Datenbank an und befüllt sie mit Musterdaten. Die Datenbank ist
        /// nach Ausführen des Tests ServiceClassSuccessTest in
        /// SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe2.Test\bin\Debug\net6.0\Appointment.db
        /// und kann mit SQLite Manager, DBeaver, ... betrachtet werden.
        /// </summary>
        private AppointmentContext GetAppointmentContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Appointment.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }
        
        [Fact]
        public void ServiceClassSuccessTest()
        {
            using var db = GetAppointmentContext();
            Assert.True(db.Students.Count() > 0);
        }
        [Fact]
        public async Task AskForAppointmentSuccessTest()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.AskForAppointment(offerId, studentId, date);

            // Assert
            Assert.True(actual);

        }
        [Fact]
        public async Task AskForAppointmentReturnsFalseIfNoOfferExists()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.AskForAppointment(Guid.NewGuid(), studentId, date);

            // Assert
            Assert.False(actual);
        }
        [Fact]
        public async Task AskForAppointmentReturnsFalseIfOutOfDate()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.AskForAppointment(offerId, studentId, new DateTime(2020,03,05));

            // Assert
            Assert.False(actual);
        }
        [Fact]
        public async Task ConfirmAppointmentSuccessTest()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.ConfirmAppointment(appointmentId);

            // Assert
            Assert.True(actual);
        }
        [Fact]
        public async Task ConfirmAppointmentReturnsFalseIfStateIsInvalid()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.ConfirmAppointment(appointmentIdInvalidState);

            // Assert
            Assert.False(actual);
        }
        [Fact]
        public async Task CancelAppointmentStudentSuccessTest()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.CancelAppointment(appointmentId, studentId);

            // Assert
            Assert.True(actual);
        }
        [Fact]
        public async Task CancelAppointmentCoachSuccessTest()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.CancelAppointment(appointmentId, new Guid("610007DA-9AB0-3AF1-71D8-6E273282F63F"));

            // Assert
            Assert.True(actual);
        }
        [Fact]
        public async Task ConfirmAppointmentStudentReturnsFalseIfStateIsInvalid()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.ConfirmAppointment(new Guid("0032BB90-FED0-ECCD-BE47-8D207FF56934"));

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ConfirmAppointmentCoachReturnsFalseIfStateIsInvalid()
        {
            // Arrange
            using var db = GetAppointmentContext();
            AppointmentService service = new AppointmentService(db);

            // Act
            bool actual = await service.ConfirmAppointment(new Guid("0032BB90-FED0-ECCD-BE47-8D207FF56934"));

            // Assert
            Assert.False(actual);
        }
    }
}
