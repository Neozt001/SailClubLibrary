using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Exceptions
{
    /// <summary>
    /// Exception class for when a boat sail number already exists
    /// </summary>
    public class BookingDoesntExistsException : Exception
    {
        public BookingDoesntExistsException(string message) : base(message)
        {

        }
    }
}
