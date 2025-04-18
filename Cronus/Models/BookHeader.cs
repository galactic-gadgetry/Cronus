using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronus.Models
{
    public class BookHeader
    {

        public string BookSaveFilePath { get; init; } = string.Empty;


        public string CreatedDateString =>
            CreatedDateTime.ToString("dd MMMM yyyy");


        public DateTime CreatedDateTime { get; init; }


        public Guid ID { get; init; }


        public string Name { get; init; } = string.Empty;


        public string SaveFilePath { get; init; } = string.Empty;


        public string Status { get; init; } = string.Empty;
    }
}
