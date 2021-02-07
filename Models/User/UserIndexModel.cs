using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.User
{
    public class UserIndexModel
    {
        public IEnumerable<UserDetailModel> users { get; set; }
    }
}
