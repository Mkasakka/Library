using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryData.Models
{
    public  class LibraryCard
    {
        public int Id { get; set; }
        public int Fees { get; set; }

        public DateTime Created { get; set; }
        public virtual IEnumerable<aCheckout> Checkouts { get; set; }
    }
}
