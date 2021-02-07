using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace LibraryServices
{
    public class CheckOutService : ICheckOut
    {
        private LibraryContext _context;
        public CheckOutService(LibraryContext context)
            
        {
            _context = context;
        }
        public void Add(aCheckout newCheckout)
        {
            _context.Add(newCheckout);
            _context.SaveChanges();
        }

        public void CheckInItem(int assetId)
        {
            var now = DateTime.Now;
            var item= _context.LibraryAssets.FirstOrDefault(a => a.Id == assetId);
            //remove checkouts on the item
            var checkout = _context.Checkouts.FirstOrDefault(s => s.LibraryAssets.Id == assetId);
            RemoveCheckOuts(checkout.Id);
            //close existing chekcouts
             CloseCheckoutHistory(assetId, now);
            //looks for holds 
            var currentHodlds = _context.Holds
                .Include(h => h.LibraryAssets)
                .Include(h => h.LibraryCard)
                .Where(h => h.LibraryAssets.Id == assetId);
            UpdateAssetStatus(assetId, "Available");
            if (currentHodlds.Any())
            {
                CheckOutToEarliest(assetId, currentHodlds);
            }
            
            _context.SaveChanges();

            
            
            //if there are holds check item to the earlies libcard
            //else uodate status to available 
        }

        private void CheckOutToEarliest(int id, IQueryable<Hold> currentHodlds)
        {
            var earliestHold = currentHodlds.OrderBy(holds => holds.HoldPlaced).
                FirstOrDefault();

            var card = earliestHold.LibraryCard;
            _context.Remove(earliestHold);
            

            CheckOutItem(id, card.Id);

        }

        public void CheckOutItem(int assetId, int LibraryCardId)
        
        {
            var now= DateTime.Now;
            if (IsCheckedout(assetId))
            {
                return; 
            }
            var item = _context.LibraryAssets.FirstOrDefault(h => h.Id == assetId);
            UpdateAssetStatus(assetId, "Checked Out");
            var card = _context.LibraryCards
                .Include(c => c.Checkouts)
                .FirstOrDefault(ca => ca.Id == LibraryCardId);

            var checout = new aCheckout
            {
                LibraryAssets = item,
                LibraryCard = card,
                Since = now,
                Until = GetDefauldCheckoutTime(now.AddDays(30))
            };

            _context.Add(checout);
            var checkouthistory = new CheckoutHistory
            {
                CheckedOut = now,
                LibraryAssets = item,
                LibraryCard = card
            };
            _context.Add(checkouthistory);

            _context.SaveChanges();
        }

        private DateTime GetDefauldCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        private bool IsCheckedout(int assetId)
        {
             return   _context.Checkouts.Where(co => co.LibraryAssets.Id == assetId).Any();
        }

        public IEnumerable<aCheckout> GetAll()
        {
            return _context.Checkouts;
        }

        public aCheckout GetById(int checkoutId)
        {
            return _context.Checkouts.FirstOrDefault(checkout => checkout.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int Id)
        {
            return _context.CheckoutHistories
                .Include(h=>h.LibraryAssets)
                .Include(h=> h.LibraryCard)
                .Where(h => h.LibraryAssets.Id == Id);
        }

        public DateTime GetCurrentHoldPlaced(int Id)
        {
            return _context.Holds
                .Include(h=> h.LibraryAssets)
                .Include(h=>h.LibraryCard)
                .FirstOrDefault(a => a.Id == Id).HoldPlaced;
        }


        public IEnumerable<Hold> GetCurrentHolds(int Id)
        {
            return _context.Holds
                .Include(h => h.LibraryAssets)
                .Where(h=>h.LibraryAssets.Id==Id);
        }

        public string GetCurrentCheckOutUserName(int Id)
        {
            var checkout = GetCurrentCheckOutByAssetId(Id);
            if (checkout==null)
            {
                return "";
            }
            else
            {
                var cardID = checkout.LibraryCard.Id;
                var User=  _context.Users
                    .Include(p=>p.LibraryCard)
                    .FirstOrDefault(i => i.LibraryCard.Id == cardID);
                return User.FirstName + " " + User.LastName;
            }
        }

        private aCheckout GetCurrentCheckOutByAssetId(int id)
        {
             return _context.Checkouts
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAssets)
                .FirstOrDefault(co => co.LibraryAssets.Id == id);

        }

        public aCheckout GetLatestCheckout(int assetId)
        {
            return _context.Checkouts.Where(c => c.Id == assetId)
                .OrderByDescending(c=>c.Since)
                .FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {
            var now = DateTime.Now;
            UpdateAssetStatus(assetId, "Available");
            RemoveCheckOuts(assetId);
            CloseCheckoutHistory(assetId, now);

            _context.SaveChanges();
        }

        private void UpdateAssetStatus(int assetId, string v)
        {
            var item = _context.LibraryAssets
            .FirstOrDefault(a => a.Id == assetId);
            

            item.Status = _context.Statuses
            .FirstOrDefault(a => a.Name == v);
            _context.Update(item);
        }

        private void CloseCheckoutHistory(int assetId, DateTime now)
        {
            var history = _context.CheckoutHistories.FirstOrDefault(hi => hi.LibraryAssets.Id == assetId && hi.CheckedIn == null);
            if (history != null)
            {
                _context.Update(history);
                history.CheckedIn = now;
            }
            _context.SaveChanges();
        }

        private void RemoveCheckOuts( int Id)
        {

             _context.Remove(_context.Checkouts
            .FirstOrDefault(co => co.Id == Id));
         
        }

        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, "Lost");

            _context.SaveChanges();
        }

        public void PlaceHold(int assetId, int LibraryCardId)
        {
            var now = DateTime.Now;
            Hold hold = new Hold
            {
                LibraryAssets = _context.LibraryAssets
                                .Include(s => s.Status)
                                .FirstOrDefault(i => i.Id == assetId),
                LibraryCard = _context.LibraryCards
                                .FirstOrDefault(c => c.Id == LibraryCardId),
                HoldPlaced = now
            };

            _context.Holds.Add(hold);
            _context.SaveChanges();

        }

        public bool IsCheckedOut(int Id)
        {
            return _context.Checkouts.Where(c => c.LibraryAssets.Id == Id).Any();
        }

        public string GetCurrentHolduserName(int id)
        {
            var hold = _context.Holds
                .Include(a => a.LibraryAssets)
                .Include(a => a.LibraryCard)
                .Where(v => v.Id == id);

            var cardId = hold
                .Include(a => a.LibraryCard)
                .Select(a => a.LibraryCard.Id)
                .FirstOrDefault();

            var user = _context.Users
                .Include(p => p.LibraryCard)
                .First(p => p.LibraryCard.Id == cardId);

              return user.FirstName + " " + user.LastName;
        }
    }
}
