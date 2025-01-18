using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Models
{
    public class RailLocation
    {
        public Track Track { get; set; }
        public Train Train { get; set; }
        public int? Position { get; set; }

        public string CurrentLocation => Track?.Name ?? $"{Train?.TrainSymbol?.Name} ({Train?.TimeOnDuty?.ToString("MM/dd/yyyy HH:mm")})";
    }
}
