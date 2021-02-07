using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int Id);
        void Add(LibraryAsset newAsset);
        string GetAuthorOrDirector(int Id);
        string GetDewwyIndex(int Id);
        string GetType(int Id);
        string GetTitle(int Id);
        string GetIsbn(int Id);


        LibraryBranch GetCurrentLocation(int Id);
    }
}
