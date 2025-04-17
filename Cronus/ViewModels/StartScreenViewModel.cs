using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronus.Commands;
using Cronus.Models.DTOs;
using Cronus.Services;
using Cronus.Stores;
using Cronus.UIComponents.Dialogs;

namespace Cronus.ViewModels
{
    public class StartScreenViewModel : ViewModelBase
    {
        /// <summary>
        /// Used by the application manage the current
        /// <see cref="Book"/>.
        /// </summary>
        private readonly BookStore _bookStore;


        /// <summary>
        /// Executed when the New Log Book button is clicked.
        /// </summary>
        public ICommand NewLogBookButtonClickedCommand { get; }



        public StartScreenViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;

            NewLogBookButtonClickedCommand = new RelayCommand(
                new Action<object?>(OnNewLogBookButtonClicked));
        }



        public void CreateNewLogBookRequested(CreateNewLogBookDialog dlg)
        {
            // Create a new book from the dialog inputs and set it
            // as the book store's current book.
            BookDTO dto = new() { Name = dlg.NameText };
            BookService.CreateNewCurrentBook(_bookStore, dto);
            
            // Update info bar.
            if (!_bookStore.CurrentBook.IsBookVoid)
            {
                OnInfoUpdated("New log book " +
                    $"'{_bookStore.CurrentBook.Name}' create");
            }
        }

        /// <summary>
        /// Handles the New Log Book button clicked event.
        /// </summary>
        /// <param name="obj"></param>
        public void OnNewLogBookButtonClicked(object? obj)
        {
            CreateNewLogBookDialog dlg = DialogService.PromptUserWithCreateNewLogBookDialog();

            if (dlg.DialogResult == true)
            {
                CreateNewLogBookRequested(dlg);
            }
        }
    }
}
