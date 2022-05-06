using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Model
{
    public class NewOfferDto
    {
        public Guid TeacherId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
