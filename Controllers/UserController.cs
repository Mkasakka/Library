using Library.Models.User;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private IUser _user;

        public UserController (IUser user)
            {
                _user = user;
            }
        public IActionResult Index()
        {
            var allUsers = _user.GetAll();

            var userModels = allUsers.Select(p => new UserDetailModel
            {
                UserID = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                Adress = p.Address,
                MemberSince = _user.Get(p.Id).LibraryCard.Created,
                TelephoneNum = p.Phonenumber,
                HomeBrach = p.HomeBranch.Name,
                Feed = _user.Get(p.Id).LibraryCard.Fees,
                CheckOutHistory = _user.GetCheckoutHistory(p.Id),
                Holds = _user.GetHolds(p.Id)
            }).ToList();

            var Users = new UserIndexModel
            {
                users = userModels
            };
            return View(Users);    
        }
        public IActionResult Detail(int Id)
        {
            var model = _user.Get(Id);

            var viewModel = new UserDetailModel
            {
                UserID = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                LibraryCardId = model.LibraryCard.Id,
                Adress = model.Address,
                MemberSince = model.LibraryCard.Created,
                TelephoneNum = model.Phonenumber,
                HomeBrach = model.HomeBranch.Name,
                Feed = model.LibraryCard.Fees,
                AssetsCheckedOut = model.LibraryCard.Checkouts,
                CheckOutHistory = _user.GetCheckoutHistory(model.Id),
                Holds = _user.GetHolds(model.Id)
            };
            return View(viewModel);
        }




    }
}
