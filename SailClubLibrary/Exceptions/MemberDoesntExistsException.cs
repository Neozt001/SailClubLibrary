using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class MemberDoesntExistsException : Exception
    {
        public MemberDoesntExistsException(string message) : base(message)
        {

        }
    }
}