using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_windows
{
    public interface Interface1
    {
        int CardNumber { get; }
        string Name { get; }
        DateTime Birthday { get; }

        int calcAge(DateTime date);
    }
}
