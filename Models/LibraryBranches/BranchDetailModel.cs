using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.LibraryBranches
{
    public class BranchDetailModel
    { 
       public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Description { get; set; }
        public string OpenDate { get; set; }
        public bool IsOpen { get; set; }
        public int NumberOfUser { get; set; }
        public int TotalLibraryAssetValue { get; set; }
        public int TotalLibraryAssetCount { get; set; }
        public IEnumerable<string> HoursOpen { get; set; }
        public string ImageUrl { get; set; }

    }
}
