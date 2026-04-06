using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public class BookingRepositoryAsync : Connection, IBookingRepositoryAsync
    {
        #region Instance Field
        private string _queryCount = "SELECT COUNT(*) FROM Bookings";
        private string _queryString = "SELECT b.ID AS BookingID, b.StartDate, b.EndDate, b.Destination, m.ID AS MemberID, m.FirstName, m.SurName, m.PhoneNumber, m.Address, m.City, m.Mail, m.TheMemberType, m.TheMemberRole, m.Image AS MemberImage, bo.ID AS BoatID, bo.SailNumber, bo.Model, bo.Draft, bo.Width, bo.Length, bo.YearOfConstruction, bo.EngineInfo, bo.TheBoatType, bo.Image AS BoatImage FROM Bookings b JOIN Members m ON b.Member_ID = m.ID JOIN Boats bo ON b.Boat_ID = bo.ID";
        private string _queryDelete = "DELETE FROM Bookings WHERE ID = @ID";
        private string _searchSql = "SELECT * FROM Bookings WHERE ID = @ID";
        private string _insertSql = @"INSERT INTO Bookings
            (StartDate, 
            EndDate, 
            Destination, 
            Member_ID, 
            Boat_ID)
            Values(@Start, 
                @End, 
                @Dest, 
                @Member_ID, 
                @Boat_ID)";
        private string _queryUpdate = @"UPDATE Bookings
            SET 
                StartDate = @Start,
                EndDate = @End,
                Destination = @Dest,
                Member_ID = @Member_ID,
                Boat_ID = @Boat_ID,
            WHERE ID = @ID";
        #endregion
        public async Task AddBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_insertSql, connection);
                await command.Connection.OpenAsync();
                //command.Parameters.AddWithValue("@ID", member.Id);
                command.Parameters.AddWithValue("@Start", booking.StartDate);
                command.Parameters.AddWithValue("@End", booking.EndDate);
                command.Parameters.AddWithValue("@Dest", booking.Destination);
                command.Parameters.AddWithValue("@Member_ID", booking.TheMember);
                command.Parameters.AddWithValue("@Boat_ID", booking.TheBoat);
                command.ExecuteNonQuery();
            }
        }
        public async Task RemoveBooking(Booking booking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryDelete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", booking.Id);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task UpdateBooking(Booking newBooking)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(_queryUpdate, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", newBooking.Id);
                command.Parameters.AddWithValue("@Start", newBooking.StartDate);
                command.Parameters.AddWithValue("@End", newBooking.EndDate);
                command.Parameters.AddWithValue("@Dest", newBooking.Destination);
                command.Parameters.AddWithValue("@Member_ID", newBooking.TheMember);
                command.Parameters.AddWithValue("@Boat_ID", newBooking.TheBoat);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<Booking>> GetAllBookings()
        {
            List<Booking> foundBookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryString, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    int bookingId = reader.GetInt32("ID");
                    DateTime start = reader.GetDateTime("StartDate");
                    DateTime end = reader.GetDateTime("EndDate");
                    string dest = reader.GetString("Destination");

                    Member member = new Member(
                        reader.GetInt32("MemberId"), 
                        reader.GetString("FirstName"), 
                        reader.GetString("SurName"), 
                        reader.GetString("PhoneNumber"), 
                        reader.GetString("Address"), 
                        reader.GetString("City"), 
                        reader.GetString("Mail"), 
                        (MemberType)reader.GetInt32("TheMemberType"), 
                        (MemberRole)reader.GetInt32("TheMemberRole"), 
                        reader.GetString("MemberImage"));

                    Boat boat = new Boat(
                        reader.GetInt32("BoatId"),
                        reader.GetString("SailNumber"),
                        reader.GetString("Model"),
                        reader.GetDouble("Draft"),
                        reader.GetDouble("Width"),
                        reader.GetDouble("Length"),
                        reader.GetString("YearOfConstruction"),
                        reader.GetString("EngineInfo"),
                        (BoatType)reader.GetInt32("TheBoatType"),
                        reader.GetString("BoatImage"));
                    Booking booking = new Booking(bookingId, start, end, dest, member, boat);

                    foundBookings.Add(booking);
                }
                reader.Close();
            }
            //Console.WriteLine(foundMembers.Count);
            //Console.ReadKey();
            return foundBookings;
        }

        public async Task<Dictionary<string, int>> GetAllBookingsForMembers()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetBookingCountForMember(Member member)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
