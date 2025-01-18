using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio
{
    public interface ISaveable
    {
        event EventHandler OnSave;
        Task Save();
    }
}
