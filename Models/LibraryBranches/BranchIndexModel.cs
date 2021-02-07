using System.Collections.Generic;
using System.ComponentModel;

namespace Library.Models.LibraryBranches
{
    public class BranchIndexModel
    {
        public IEnumerable<BranchDetailModel> Branches { get; set; }
    }
}
