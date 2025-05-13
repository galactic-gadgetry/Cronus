using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cronus.Models
{
    public enum ProjectStatus
    {
        Active,
        Archived,
    }


    public class Project : IConfirmDeletion, INotifyPropertyChanged
    {
        // Backing Fields
        private string code = string.Empty;
        private string name = string.Empty;
        private ProjectStatus status;
        private string wbs = string.Empty;



        public string Code
        {
            get => code;
            set
            {
                code = value;
                OnPropertyChanged(nameof(Code));
            }
        }


        public DateTime CreatedDateTime { get; init; }


        public string CreatedDateString =>
            CreatedDateTime.ToString("dd MMMM yyyy");


        public Guid ID { get; init; }


        public bool IsUnassignedProject { get; init; } = false;


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public ProjectStatus Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }


        public string StatusText => Status.ToString();


        public string Wbs
        {
            get => wbs;
            set
            {
                wbs = value;
                OnPropertyChanged(nameof(Wbs));
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;



        public Project()
        {
            CreatedDateTime = DateTime.Now;
            ID = Guid.NewGuid();
            Status = ProjectStatus.Active;
        }



        public (bool, string?) ContainsDetailConflict(Project project)
        {
            if (Code.ToLower() == project.Code.ToLower())
            {
                return (true, "Code");
            }
            else if(Wbs.ToLower() == project.Wbs.ToLower())
            {
                return (true, "WBS");
            }
            else
            {
                return (false, null);
            }
        }


        public string GetModelTypeString()
        {
            return "Project";
        }


        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
