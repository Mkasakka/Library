using LibraryData.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.User
{
    public class UserDetailModel
    {
        public  int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LibraryCardId { get; set; }
        public string Adress { get; set; }
        public DateTime MemberSince { get; set; }
        public string TelephoneNum { get; set; }
        public string HomeBrach { get; set; }
        public int Feed { get; set; }
        public IEnumerable<aCheckout> AssetsCheckedOut { get; set; }
        public IEnumerable<CheckoutHistory> CheckOutHistory { get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
 