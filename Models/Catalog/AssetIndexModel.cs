using Library.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AssetIndexModel
    {
        public IEnumerable<AssetIndexListengModel> Asset { get; set; }
    }
}
