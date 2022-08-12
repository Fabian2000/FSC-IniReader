using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSC_IniReader.Exceptions
{
    public class FSCIniException : Exception
    {
        public FSCIniException() : base()
        {
        }

        public FSCIniException(string? message) : base(message)
        {
        }

        public FSCIniException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
