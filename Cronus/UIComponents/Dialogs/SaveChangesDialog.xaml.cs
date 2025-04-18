using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cronus.Models;

namespace Cronus.UIComponents.Dialogs
{
    /// <summary>
    /// Interaction logic for SaveChangesDialog.xaml
    /// </summary>
    public partial class SaveChangesDialog : Window
    {
        /// <summary>
        /// The book with unsaved changes.
        /// </summary>
        public Book CurrentBook { get; set; }

        /// <summary>
        /// True if the user wishes to save the book, false
        /// otherwise.
        /// </summary>
        public bool SaveBook { get; private set; } = false;


        /// <summary>
        /// Initializes a new instance of the
        /// <seealso cref="SaveChangesDialog"/> class.
        /// </summary>
        /// <param name="book"></param>
        public SaveChangesDialog(Book book)
        {
            CurrentBook = book;
            DataContext = this;

            InitializeComponent();
        }


        /// <summary>
        /// Handles the Cancel button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Handles the Don't Save button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DontSaveButton_Click(Object sender, RoutedEventArgs e)
        {
            SaveBook = false;
            DialogResult = true;
        }

        /// <summary>
        /// Handles the Save button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(Object sender, RoutedEventArgs e)
        {
            SaveBook = true;
            DialogResult = true;
        }
    }
}
