using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buddy.Entities.InteractionsandRecords;

namespace buddy.Entities.Ratings
{
    public class RecordRating : Rating
    {
        public int RecordId { get; set; } // FK to Record

        public virtual Record Record { get; set; }
    }
}
