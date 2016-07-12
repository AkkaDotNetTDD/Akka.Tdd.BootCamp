using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayServiceLib
{
    public interface IDisplayService
    {
        bool SendDisplayMessage(string displayMessage);
    }
}
