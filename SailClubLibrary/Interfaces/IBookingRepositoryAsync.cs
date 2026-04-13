using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IBookingRepositoryAsync
    {
        Task AddBooking(Booking booking);
        Task RemoveBooking(Booking b);
        Task<List<Booking>> GetAllBookings();
        Task UpdateBooking(Booking newBooking);
        Task<Booking?> SearchBooking(int id);
        //Task<int> GetBookingCountForMember(Member member);
        //Task<Dictionary<string, int>> GetAllBookingsForMembers();
    }
}
