using Library.Models.LibraryBranches;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{

    public class BranchController : Controller
    {
        private ILibraryBranch _branch;
        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index()
        {
            var Tempmodel = _branch.GetAll().Select(p => new BranchDetailModel
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Telephone = p.Telephone,
                Description = p.Description,
                OpenDate = p.OpenDate.ToString("yyyyy'-'MM"),
                NumberOfUser = _branch.GetAllUsers(p.Id).Count(),
                TotalLibraryAssetCount = _branch.GetAllAssets(p.Id).Count(),
                ImageUrl = "",
                IsOpen = _branch.IsOpen(p.Id),
                HoursOpen = _branch.GetHours(p.Id)
            });
            var model = new BranchIndexModel
            {
                Branches = Tempmodel
            };
            return View(model);
        }
        public IActionResult Detail(int Id)
        {
            var p = _branch.GetById(Id);
            var model = new BranchDetailModel
            {
                Id = p.Id,
                Name = p.Name,
                Address = p.Address,
                Telephone = p.Telephone,
                Description = p.Description,
                OpenDate = p.OpenDate.ToString("yyyyy'-'MM"),
                NumberOfUser = _branch.GetAllUsers(Id).Count(),
                TotalLibraryAssetCount = _branch.GetAllAssets(Id).Count(),
                ImageUrl = "",
                IsOpen = _branch.IsOpen(Id),
                TotalLibraryAssetValue = _branch.GetAllAssets(Id).Sum(a=>a.Cost),
                HoursOpen = _branch.GetHours(Id)
            };
            return View(model);
        }

    }
}
