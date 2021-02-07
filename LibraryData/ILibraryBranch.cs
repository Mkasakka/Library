using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace LibraryData
{
    public interface ILibraryBranch
    {
        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<User> GetAllUsers(int id);
        IEnumerable<LibraryAsset> GetAllAssets(int id);
        LibraryBranch GetById(int Id);
        void Add(LibraryBranch newlibraryBranch);
        bool IsOpen(int Id);
        IEnumerable<string> GetHours(int Id);


    }
}
