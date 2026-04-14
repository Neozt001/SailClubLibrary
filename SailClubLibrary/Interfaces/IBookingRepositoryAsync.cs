using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookingRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        Task AddBooking(Booking booking);
        Task RemoveBooking(Booking b);
        Task<List<Booking>> GetAllBookings();
        Task UpdateBooking(Booking newBooking);
        Task<Booking?> SearchBooking(int id);
    }
}
