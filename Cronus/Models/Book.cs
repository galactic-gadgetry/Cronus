using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronus.Models
{
    public class Book
    {
        // Backing Fields
        private string name = string.Empty;


        public bool IsBookVoid { get; set; } = false;


        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }
    }
}
