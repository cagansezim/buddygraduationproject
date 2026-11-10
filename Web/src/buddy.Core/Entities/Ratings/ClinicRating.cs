using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buddy.Entities.Facilities;

namespace buddy.Entities.Ratings
{
    public class ClinicRating : Rating
    {
        public int ClinicId { get; set; } // FK to Clinic

        public virtual Clinic Clinic { get; set; }
    }
}
