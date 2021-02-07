using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface ICheckOut
    {
        IEnumerable<aCheckout> GetAll();
        aCheckout GetById(int checkoutId);
        void Add(aCheckout newCheckout);
        void CheckInItem(int assetId);
        void CheckOutItem(int assetId, int LibraryCardId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int Id);
        aCheckout GetLatestCheckout(int assetId);

        void PlaceHold(int assetId, int LibraryCardId);
        string GetCurrentCheckOutUserName(int Id);
        bool IsCheckedOut(int Id);
        DateTime GetCurrentHoldPlaced(int Id);
        IEnumerable<Hold> GetCurrentHolds(int Id);

        void MarkLost(int assetId);
        void MarkFound(int assetId);
        string GetCurrentHolduserName(int id);
    }
}
