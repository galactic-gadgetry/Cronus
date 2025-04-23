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


        /// <summary>
        /// The current <see cref="Book"/> instance focused by the
        /// application.
        /// </summary>
        public Book CurrentBook
        {
            get => currentBook;
            set
            {
                currentBook = value;
                OnCurrentBookChanged();
            }
        }

        /// <summary>
        /// The current <see cref="Project"/> in the
        /// <seealso cref="CurrentBook"/> focused by the application.
        /// </summary>
        public Project? CurrentFocusedProject { get; set; }


        /// <summary>
        /// Raised when the <seealso cref="CurrentBook"/> property
        /// is set.
        /// </summary>
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


        /// <summary>
        /// Handles the setting of the <seealso cref="CurrentBook"/>
        /// property.
        /// </summary>
        private void OnCurrentBookChanged()
        {
            CurrentBookChanged?.Invoke();
        }
    }
}
