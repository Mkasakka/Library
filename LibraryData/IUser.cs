using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface IUser
    {
        User Get(int Id);
        IEnumerable<User> GetAll();
        void Add(User newUser);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int userId);
        IEnumerable<Hold> GetHolds(int UserId);
        IEnumerable<aCheckout> GetCheckouts(int UserId);
    }
}
