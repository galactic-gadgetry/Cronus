using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;

namespace Cronus.Stores
{
    public class BookStore
    {
        // Backing Fields
        private Book currentBook;



        public Book CurrentBook
        {
            get => currentBook;
            set
            {
                currentBook = value;
                OnCurrentBookChanged();
            }
        }



        public Action? CurrentBookChanged;



        public BookStore()
        {
            currentBook = new() { IsBookVoid = true };
        }


        public BookStore(Book book)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            currentBook = book;
        }



        private void OnCurrentBookChanged()
        {
            CurrentBookChanged?.Invoke();
        }
    }
}
