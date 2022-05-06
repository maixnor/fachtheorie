using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class DeleteOfferDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public bool CanDelete { get; set; }
    }
}
