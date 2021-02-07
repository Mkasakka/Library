using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Library.Models.Catalog;
using Library.Models.Checkout;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _asset;
        private ICheckOut _checkOut;
        public CatalogController(ILibraryAsset asset, ICheckOut checkOut)
        {
            _asset = asset;
            _checkOut = checkOut;
        }
        public IActionResult Index()
        {
            var assetModels = _asset.GetAll();
            var listingResult = assetModels
                .Select(result => new AssetIndexListengModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _asset.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _asset.GetDewwyIndex(result.Id),
                    Title = result.Title,
                    Type = _asset.GetType(result.Id)
                });
            var model = new AssetIndexModel()
            {
                Asset = listingResult
            };
            return View(model);
        }


        public IActionResult Detail(int Id)
        {

            var asset = _asset.GetById(Id);
            var currentholds = _checkOut.GetCurrentHolds(Id)
                .Select(a => new AssetHoldModel
                {
                    DateHoldPlaced = _checkOut.GetCurrentHoldPlaced(a.Id).ToString("d"),
                    UserName = _checkOut.GetCurrentHolduserName(a.Id)
                }); 
            var model = new AssetDetailModel
            {
                AssetId = Id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDircetor = _asset.GetAuthorOrDirector(Id),
                CurrentLocation = asset.Location.Name,
                DeweyCallNumber = _asset.GetDewwyIndex(Id),
                ISBN = _asset.GetIsbn(Id),
                CheckoutHistory = _checkOut.GetCheckoutHistory(Id),
                LatestCheckOut = _checkOut.GetLatestCheckout(Id),
                UserName = _checkOut.GetCurrentCheckOutUserName(Id),
                CurrentHolds = currentholds
            };
            return View(model);
        }

        public IActionResult CheckOuts(int Id)
        {
            var asset = _asset.GetById(Id);
            var model = new CheckoutModel
            {

                Title = asset.Title,
                AssetId = Id,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                IsCheckedout = _checkOut.IsCheckedOut(Id)
            };
            return View(model);
        }
        public IActionResult CheckIn( int Id)
        {
            _checkOut.CheckInItem(Id);
            return RedirectToAction("Detail", new { id = Id });

        }


        public IActionResult Hold(int Id)
        {
            var asset = _asset.GetById(Id);
            var model = new CheckoutModel
            {

                Title = asset.Title,
                AssetId = Id,
                ImageUrl = asset.ImageUrl,
                LibraryCardId = "",
                HoldCount = _checkOut.GetCurrentHolds(Id).Count(),
                IsCheckedout = _checkOut.IsCheckedOut(Id)
            };
            return View(model);
        }
        public IActionResult MarkLost(int assetId)
        {
            _checkOut.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });

        }
        public IActionResult MarkFound(int assetId)
        {
            _checkOut.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkOut.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });

        }
        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkOut.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });

        }
 
    }
}
