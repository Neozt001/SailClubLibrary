using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Data;
using System.Text;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Member Repository Objects using the interface
    /// </summary>
    public class MemberRepositoryAsync : Connection, IMemberRepositoryAsync
    {
        #region Instance Fields
        private Dictionary<string, Member> _members;
        //private string _queryCount = "COUNT(*) FROM Members";
        private string _queryCount = "SELECT COUNT(*) FROM Members";
        private string _queryString = "SELECT * FROM Members";
        private string _insertSql = "INSERT INTO Members Values(@FirstName, @SurName, @PhoneNumber, @Address, @City, @Mail, @TheMemberType, @TheMemberRole)";
        private string _queryDelete = "DELETE FROM Members WHERE Member_Id = @ID";
        private string _queryUpdate = "UPDATE Members " +
            "SET Member_FirstName = @FirstName," +
            " Member_SurName = @SurName," +
            " Member_PhoneNumber = @PhoneNumber," +
            " Member_Address = @Address," +
            " Member_City = @City," +
            " Member_Mail = @Mail," +
            " Member_TheMemberType = @TheMemberType," +
            " Member_TheMemberRole = @TheMemberRole " +
            //" WHERE Member_Id = @ID";
            "WHERE Member_Id = @ID";
        private string _searchSql = "SELECT * FROM Members WHERE Member_ID = @ID";

        //int IMemberRepository.Count => throw new NotImplementedException();


        #region Properties
        /// <summary>
        /// Count used for counting members in _members repository
        /// </summary>
        public Task<int> Count { get { return GetCount(); } }
        //public int Count { get { return _members.Count; } }

        #endregion

        #region Constructor
        /// <summary>
        /// MemberRepository constructor used for making a new member repository called _members with string as key and IMember as value
        /// </summary>
        public MemberRepositoryAsync()
        {
            //_members = new Dictionary<string, Member>();
            //_members = new MockData().MemberData;
        }
        #endregion

        #region Methods


        public async Task<int> GetCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(_queryCount, connection))
            {
                await connection.OpenAsync();
                object? result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }
        //public async Task<int> GetCount()
        //{
        //    int count = 0;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {

        //        try
        //        {
        //            SqlCommand command = new SqlCommand(_queryCount, connection);
        //            command.Connection.OpenAsync();
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            while (reader.Read())
        //            {
        //                count = reader.GetInt32(0);
        //            }
        //            reader.Close();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    return count;
        //}
        // Formål:
        // Tilføje Medlem
        // if-statement:
        // Hvis Dictionary _members ikke indeholder Telefonnummer på det Medlem man vil tilføje. Tilføjes Medlemmet
        // Else if:
        //Medlem bliver ikke tilføjet

        //public async Task<int> Count()
        //{
        //    int numberOfMembersRes = 0;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(_insertSql, connection);
        //            await command.Connection.OpenAsync();
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            reader.Read();
        //            int numberOfMembers = command.ExecuteNonQuery();
        //            numberOfMembersRes = numberOfMembers;
        //            //Thread.Sleep(1000);
        //            //return numberOfRow == 1;
        //            reader.Close();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //        return numberOfMembersRes;
        //    }
        //    return numberOfMembersRes;
        //}

        /// <summary>
        /// Method for adding members to our repository, which runs a check to tell if the phone number is available
        /// </summary>
        public async Task AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_insertSql, connection);
                await command.Connection.OpenAsync();
                //command.Parameters.AddWithValue("@ID", member.Id);
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@SurName", member.SurName);
                command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
                command.Parameters.AddWithValue("@Address", member.Address);
                command.Parameters.AddWithValue("@City", member.City);
                command.Parameters.AddWithValue("@Mail", member.Mail);
                command.Parameters.AddWithValue("@TheMemberType", member.TheMemberType);
                command.Parameters.AddWithValue("@TheMemberRole", member.TheMemberRole);
                //int numberOfRow = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
                //Thread.Sleep(1000);
                //return numberOfRow == 1;
                //return member;
            }
            //return false;
        }
        //public async Task AddMember(Member member)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(_insertSql, connection);
        //            await command.Connection.OpenAsync();
        //            command.Parameters.AddWithValue("@ID", member.Id);
        //            command.Parameters.AddWithValue("@FirstName", member.FirstName);
        //            command.Parameters.AddWithValue("@SurName", member.SurName);
        //            command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
        //            command.Parameters.AddWithValue("@Address", member.Address);
        //            command.Parameters.AddWithValue("@City", member.City);
        //            command.Parameters.AddWithValue("@Mail", member.Mail);
        //            command.Parameters.AddWithValue("@TheMemberType", member.TheMemberType);
        //            command.Parameters.AddWithValue("@TheMemberRole", member.TheMemberRole);
        //            int numberOfRow = command.ExecuteNonQuery();
        //            //Thread.Sleep(1000);
        //            //return numberOfRow == 1;
        //            //return member;
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    //return false;
        //}
        // Formål:
        // At få fat på en list med alle medlemmer/objekter
        // Metoden returnere via en indbygget metode som hedder ToList(); som henter liste med _members Values

        /// <summary>
        /// Method for returning a list of members
        /// </summary>
        public async Task<List<Member>> GetAllMembers()
        {
            List<Member> foundMembers = new List<Member>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryString, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    int memberId = reader.GetInt32("Member_Id");
                    string firstName = reader.GetString("Member_FirstName");
                    string surName = reader.GetString("Member_SurName");
                    string phoneNumber = reader.GetString("Member_PhoneNumber");
                    string memberAddress = reader.GetString("Member_Address");
                    string city = reader.GetString("Member_City");
                    string mail = reader.GetString("Member_Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("Member_TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("Member_TheMemberRole")];
                    Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                    foundMembers.Add(member);
                }
                reader.Close();
            }
            //Console.WriteLine(foundMembers.Count);
            //Console.ReadKey();
            return foundMembers;
        }
        //public async Task<List<Member>> GetAllMembers()
        //{
        //    List<Member> foundMembers = new List<Member>();
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(_queryString, connection);
        //            await command.Connection.OpenAsync();
        //            SqlDataReader reader = await command.ExecuteReaderAsync();
        //            while (reader.Read())
        //            {
        //                int memberId = reader.GetInt32("Member_Id");
        //                string firstName = reader.GetString("Member_FirstName");
        //                string surName = reader.GetString("Member_SurName");
        //                string phoneNumber = reader.GetString("Member_PhoneNumber");
        //                string memberAddress = reader.GetString("Member_Address");
        //                string city = reader.GetString("Member_City");
        //                string mail = reader.GetString("Member_Mail");
        //                MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("Member_TheMemberType")];
        //                MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("Member_TheMemberRole")];
        //                Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
        //                foundMembers.Add(member);
        //            }
        //            reader.Close();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            throw sqlExp;
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    //Console.WriteLine(foundMembers.Count);
        //    //Console.ReadKey();
        //    return foundMembers;
        //}
        // Formål:
        // Fjerne Medlem
        // Metoden sletter via metoden Remove, og sletter telefonnummeret fra _members

        /// <summary>
        /// Method for removing a member from the dictionary, using their phone number
        /// </summary>
        public async Task RemoveMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Member memberToBeDel = await SearchMember(member.PhoneNumber);
                //if(memberToBeDel == null)
                //{
                //    return;
                //}
                SqlCommand command = new SqlCommand(_queryDelete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", member.Id);
                //int numberOfRows = await command.ExecuteNonQueryAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        //public async Task RemoveMember(Member member)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            //Member memberToBeDel = await SearchMember(member.PhoneNumber);
        //            //if(memberToBeDel == null)
        //            //{
        //            //    return;
        //            //}
        //            SqlCommand command = new SqlCommand(_queryDelete, connection);
        //            await command.Connection.OpenAsync();
        //            command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
        //            int numberOfRows = await command.ExecuteNonQueryAsync();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //}
        // Formål:
        // Opdatere Medlem
        // if-statement:
        // Hvis _members indholder Telefonnummeret argumentet, så overskrider de nye værdier de nuværende med samme telefonnummer.

        /// <summary>
        /// Method to update a member's info, using their phone number to distinguish them
        /// </summary>
        //public async void UpdateMember(Member updatedMember)
        //{
        //    if (_members.ContainsKey(updatedMember.PhoneNumber))
        //    {
        //        Member existingMember = _members[updatedMember.PhoneNumber];

        //        existingMember.FirstName = updatedMember.FirstName;
        //        existingMember.SurName = updatedMember.SurName;
        //        existingMember.Address = updatedMember.Address;
        //        existingMember.City = updatedMember.City;
        //        existingMember.Mail = updatedMember.Mail;
        //        existingMember.TheMemberType = updatedMember.TheMemberType;
        //        existingMember.TheMemberRole = updatedMember.TheMemberRole;
        //    }
        //}
        public async Task UpdateMember(Member updatedMember)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand command = new SqlCommand(_queryUpdate, connection);
                await command.Connection.OpenAsync();
                //command.Parameters.AddWithValue("@ID", updatedMember.Id);
                command.Parameters.AddWithValue("@ID", updatedMember.Id);
                command.Parameters.AddWithValue("@FirstName", updatedMember.FirstName);
                command.Parameters.AddWithValue("@SurName", updatedMember.SurName);
                command.Parameters.AddWithValue("@PhoneNumber", updatedMember.PhoneNumber);
                command.Parameters.AddWithValue("@Address", updatedMember.Address);
                command.Parameters.AddWithValue("@City", updatedMember.City);
                command.Parameters.AddWithValue("@Mail", updatedMember.Mail);
                command.Parameters.AddWithValue("@TheMemberType", updatedMember.TheMemberType);
                command.Parameters.AddWithValue("@TheMemberRole", updatedMember.TheMemberRole);
                //int numberOfRow = command.ExecuteNonQuery();
                await command.ExecuteNonQueryAsync();
            }
        }
        //public async Task UpdateMember(Member updatedMember)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand command = new SqlCommand(_queryUpdate, connection);
        //            await command.Connection.OpenAsync();
        //            command.Parameters.AddWithValue("@ID", updatedMember.Id);
        //            command.Parameters.AddWithValue("@FirstName", updatedMember.FirstName);
        //            command.Parameters.AddWithValue("@SurName", updatedMember.SurName);
        //            command.Parameters.AddWithValue("@PhoneNumber", updatedMember.PhoneNumber);
        //            command.Parameters.AddWithValue("@Address", updatedMember.Address);
        //            command.Parameters.AddWithValue("@City", updatedMember.City);
        //            command.Parameters.AddWithValue("@Mail", updatedMember.Mail);
        //            command.Parameters.AddWithValue("@TheMemberType", updatedMember.TheMemberType);
        //            command.Parameters.AddWithValue("@TheMemberRole", updatedMember.TheMemberRole);
        //            int numberOfRow = command.ExecuteNonQuery();
        //        }
        //        catch (SqlException sqlExp)
        //        {
        //            Console.WriteLine("Database error" + sqlExp.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Generel fejl: " + ex.Message);
        //        }
        //        finally
        //        {

        //        }

        //    }
        //}
        /// <summary>
        /// Searches through the member dictionary and returns the member with the given phonenumber. 
        /// </summary>
        //public async Task<Member?> SearchMember(string phoneNumber)
        //{
        //    if (_members.ContainsKey(phoneNumber))
        //    {
        //        return _members[phoneNumber];
        //    }
        //    return null;
        //}
        //public async Task<Member?> SearchMember(string phoneNumber)
        //{
        //    Task<List<Member>> task = GetAllMembers();
        //    List<Member> members = await task;
        //    foreach (Member m in members)
        //    {
        //        if (m.PhoneNumber == phoneNumber)
        //        {
        //            return m;
        //        }
        //    }
        //    return null;
        //}
        public async Task<Member?> SearchMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Member member = new Member();
            
                SqlCommand command = new SqlCommand(_searchSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    int memberId = reader.GetInt32("Member_Id");
                    string firstName = reader.GetString("Member_FirstName");
                    string surName = reader.GetString("Member_SurName");
                    string phoneNumber = reader.GetString("Member_PhoneNumber");
                    string memberAddress = reader.GetString("Member_Address");
                    string city = reader.GetString("Member_City");
                    string mail = reader.GetString("Member_Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("Member_TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("Member_TheMemberRole")];
                    member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole);
                    reader.Close();
                }
                
                return member;
            }
            return null;
        }
        //public async Task<Member?> SearchMember(int id)
        //{
        //    Task<List<Member>> task = GetAllMembers();
        //    List<Member> members = await task;
        //    foreach (Member m in members)
        //    {
        //        if (m.Id == id)
        //        {
        //            return m;
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// Method for printing the info of every member in the dictionary
        /// </summary>
        public async Task PrintAll()
        {
            foreach (Member member in await GetAllMembers())
            {
                Console.WriteLine(member);
                Console.WriteLine();
            }
        }

        public async Task<List<Member>> FilterMembers(string filterCriteria)
        {
            List<Member> mList = [];
            foreach (Member m in await GetAllMembers())
            {
                if (m.FirstName.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
                if (m.SurName.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
                if (m.PhoneNumber.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
                if (m.Address.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
                if (m.City.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
                if (m.Mail.Contains(filterCriteria))
                {
                    mList.Add(m);
                }
            }
            return mList;
        }
        #endregion
    }
}
#endregion