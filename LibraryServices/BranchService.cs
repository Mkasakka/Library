using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
    public class BranchService : ILibraryBranch
    {
        private LibraryContext _context;
        public BranchService(LibraryContext context)

        {
            _context = context;
        }
        public void Add(LibraryBranch newlibraryBranch)
        {
            _context.LibraryBranches.Add(newlibraryBranch);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                .Include(p=>p.User)
                .Include(b => b.LibraryAssets);
    
        }

        public IEnumerable<LibraryAsset> GetAllAssets(int id)
        {
            return _context.LibraryBranches.FirstOrDefault(h => h.Id == id).LibraryAssets;
        }

        public IEnumerable<User> GetAllUsers(int id)
        {
            return _context.LibraryBranches.FirstOrDefault(h => h.Id == id).User;
        }

        public LibraryBranch GetById(int Id)
        {
            return _context.LibraryBranches
                .Include(b => b.User)
                .Include(b => b.Name)
                .Include(b => b.LibraryAssets)
                .Include(b => b.Telephone)
                .FirstOrDefault(h => h.Id == Id);
        }

        public IEnumerable<string> GetHours(int Id)
        {
            var hours = _context.BranchHours.Where(h => h.Branch.Id == Id);
            return DataHelpers.HumanizeBizHours(hours);
        }

        public bool IsOpen(int Id)
        {
            var Hoursnow = DateTime.Now.Hour;
            var Daynow = (int)DateTime.Now.DayOfWeek+1;
            var hours = _context.BranchHours.Where(h => h.Branch.Id == Id);
            var TodaysHours = hours.FirstOrDefault(d => d.DayOfWeek == Daynow);

            return Hoursnow<TodaysHours.CloseTime && Hoursnow>TodaysHours.OpenTime;
        }
    }
}
