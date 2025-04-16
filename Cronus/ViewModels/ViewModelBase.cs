using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronus.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event EventHandler<string>? InfoUpdated;

        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnInfoUpdated(string info)
        {
            InfoUpdated?.Invoke(this, info);
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
