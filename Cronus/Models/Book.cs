using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Services;

namespace Cronus.Models
{
    public enum BookStatus
    {
        Active,
        Archived,
    }

    public class Book : IConfirmDeletion, INotifyPropertyChanged
    {
        // Backing Fields
        private bool hasUnsavedChanges = false;
        private string headerSaveFilePath = string.Empty;
        private string name = string.Empty;



        public string CreatedDateString =>
            CreatedDateTime.ToString("dd MMMM yyyy");


        public DateTime CreatedDateTime { get; init; }


        public bool HasUnsavedChanges
        {
            get => hasUnsavedChanges;
            set
            {
                hasUnsavedChanges = value;
                OnPropertyChanged(nameof(HasUnsavedChanges));
            }
        }


        public string HeaderSaveFilePath
        {
            get => headerSaveFilePath;
            set
            {
                headerSaveFilePath = value;
                HasUnsavedChanges = true;
            }
        }


        public Guid ID { get; init; }


        public bool IsBookVoid { get; set; } = false;


        public string Name
        {
            get => name;
            set
            {
                name = value;
                HasUnsavedChanges = true;
            }
        }


        public string SaveFilePath { get; init; } = string.Empty;


        public BookStatus Status { get; set; }


        public string StatusText => Status.ToString();



        public event PropertyChangedEventHandler? PropertyChanged;



        public Book()
        {
            CreatedDateTime = DateTime.Now;
            ID = Guid.NewGuid();
            HeaderSaveFilePath = Path.Combine(
                FileService.SaveFileDirectory,
                ID.ToString() + "_head.json");
            SaveFilePath = Path.Combine(
                FileService.SaveFileDirectory,
                ID.ToString() + ".json");
        }



        public string GetModelTypeString()
        {
            return "Log Book";
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
