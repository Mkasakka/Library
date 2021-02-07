using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;
        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }
        public void Add(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
           return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location );
        }

        public string GetAuthorOrDirector(int Id)
        {
            var IsBook = _context.LibraryAssets.OfType<Book>()
                 .Where(asset => asset.Id == Id).Any();
            var IsVideo = _context.LibraryAssets.OfType<Video>()
                .Where(asset => asset.Id == Id).Any();

            return IsBook ?
                _context.Books.FirstOrDefault(asset => asset.Id == Id).Author :
                 _context.Videos.FirstOrDefault(asset => asset.Id == Id).Director
                 ?? "Unknown";

        }

        public LibraryAsset GetById(int Id)
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == Id);
        }

        public LibraryBranch GetCurrentLocation(int Id)
        {
            return _context.LibraryAssets
                .FirstOrDefault(asset => asset.Id == Id).Location;
        }

        public string GetDewwyIndex(int Id)
        {
            if (_context.Books.Any(Book => Book.Id == Id))
            {
                return _context.Books
                    .FirstOrDefault(Book => Book.Id == Id).DeweyIndex;
            }
            else
            {
                return "";
            }
        }

        public string GetIsbn(int Id)
        {
            if (_context.Books.Any(Book => Book.Id == Id))
            {
                return _context.Books
                    .FirstOrDefault(Book => Book.Id == Id).ISBN;
            }
            else
            {
                return "";
            }
        }

        public string GetTitle(int Id)
        {
            return GetById(Id).Title; 
        }

        public string GetType(int Id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(asset => asset.Id == Id).Any();
            return book ? "Book" : "Video" ?? "Unknown";
        }
    }
}
