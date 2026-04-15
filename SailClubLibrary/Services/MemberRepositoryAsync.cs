using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Data;
using System.Text;

namespace SailClubLibrary.Services
{
    public class MemberRepositoryAsync : Connection, IMemberRepositoryAsync
    {
        #region Instance Fields
        private string _queryCount = "SELECT COUNT(*) FROM Members";
        private string _queryString = "SELECT * FROM Members";
        private string _insertSql = "INSERT INTO Members Values(@FirstName, @SurName, @PhoneNumber, @Address, @City, @Mail, @TheMemberType, @TheMemberRole, @Image, @Password)";
        private string _queryDelete = "DELETE FROM Members WHERE ID = @ID";
        private string _queryUpdate = "UPDATE Members " +
            " SET FirstName = @FirstName," +
            " SurName = @SurName," +
            " PhoneNumber = @PhoneNumber," +
            " Address = @Address," +
            " City = @City," +
            " Mail = @Mail," +
            " TheMemberType = @TheMemberType," +
            " TheMemberRole = @TheMemberRole, " +
            " Image = @Image," +
            " Password = @Password" +
            " WHERE ID = @ID";
        private string _searchSql = "SELECT * FROM Members WHERE ID = @ID";
        private string _searchByPhoneSql = "SELECT * FROM Members WHERE PhoneNumber = @PhoneNumber";
        private string _verifySql = "SELECT * FROM Members WHERE PhoneNumber = @PhoneNumber AND Password = @Password";
        #region Properties
        /// <summary>
        /// Returns the number of Members in the list
        /// </summary>
        public Task<int> Count { get { return GetCount(); } }
        #endregion

        #region Constructor
        public MemberRepositoryAsync()
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Facilitets the implementation of the Count property
        /// </summary>
        /// <returns>Returns a Task which the result is an integer</returns>
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
        /// <summary>
        /// Adds a Member object to the table Members by executing an SQL command 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_insertSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@SurName", member.SurName);
                command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
                command.Parameters.AddWithValue("@Address", member.Address);
                command.Parameters.AddWithValue("@City", member.City);
                command.Parameters.AddWithValue("@Mail", member.Mail);
                command.Parameters.AddWithValue("@TheMemberType", member.TheMemberType);
                command.Parameters.AddWithValue("@TheMemberRole", member.TheMemberRole);
                command.Parameters.AddWithValue("@Image", member.Image);
                command.Parameters.AddWithValue("@Password", member.Password);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Removes a member record from the table Members by using a Member object and executing an SQL command
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task RemoveMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryDelete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", member.Id);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// Updates a member record in the table Members by using a member object and executing an SQL command
        /// </summary>
        /// <param name="updatedMember"></param>
        /// <returns></returns>
        public async Task UpdateMember(Member updatedMember)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(_queryUpdate, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", updatedMember.Id);
                command.Parameters.AddWithValue("@FirstName", updatedMember.FirstName);
                command.Parameters.AddWithValue("@SurName", updatedMember.SurName);
                command.Parameters.AddWithValue("@PhoneNumber", updatedMember.PhoneNumber);
                command.Parameters.AddWithValue("@Address", updatedMember.Address);
                command.Parameters.AddWithValue("@City", updatedMember.City);
                command.Parameters.AddWithValue("@Mail", updatedMember.Mail);
                command.Parameters.AddWithValue("@TheMemberType", updatedMember.TheMemberType);
                command.Parameters.AddWithValue("@TheMemberRole", updatedMember.TheMemberRole);
                command.Parameters.AddWithValue("@Image", updatedMember.Image);
                command.Parameters.AddWithValue("@Password", updatedMember.Password);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// Searchs for a member object by ID and executing an SQL command 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Member object</returns>
        public async Task<Member?> SearchMember(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Member member = new Member();
                SqlCommand command = new SqlCommand(_searchSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int memberId = reader.GetInt32("ID");
                    string firstName = reader.GetString("FirstName");
                    string surName = reader.GetString("SurName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string memberAddress = reader.GetString("Address");
                    string city = reader.GetString("City");
                    string mail = reader.GetString("Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("TheMemberRole")];
                    string image = reader.GetString("Image");
                    string password = reader.GetString("Password");
                    return new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole, image, password);
                }
                return null;
            }
        }
        /// <summary>
        /// Searchs for a member object by phone and executing an SQL command 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Member object</returns>
        public async Task<Member?> SearchMemberByPhone(string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Member member = new Member();
                SqlCommand command = new SqlCommand(_searchByPhoneSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@PhoneNumber", phone);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int memberId = reader.GetInt32("ID");
                    string firstName = reader.GetString("FirstName");
                    string surName = reader.GetString("SurName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string memberAddress = reader.GetString("Address");
                    string city = reader.GetString("City");
                    string mail = reader.GetString("Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("TheMemberRole")];
                    string image = reader.GetString("Image");
                    string password = reader.GetString("Password");
                    return new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole, image, password);
                }
                return null;
            }
        }
        /// <summary>
        /// Verifies a member object by phone, password and then executing an SQL command 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns>Member object</returns>
        public async Task<Member?> VerifyMember(string phone, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_verifySql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@PhoneNumber", phone);
                command.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int memberId = reader.GetInt32("ID");
                    string firstName = reader.GetString("FirstName");
                    string surName = reader.GetString("SurName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string memberAddress = reader.GetString("Address");
                    string city = reader.GetString("City");
                    string mail = reader.GetString("Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("TheMemberRole")];
                    string image = reader.GetString("Image");
                    string pass = reader.GetString("Password");
                    return new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole, image, pass);
                }
                return null;
            }
        }
        /// <summary>
        /// Retrieves a list of all member records in table Members by executing an SQL command
        /// </summary>
        /// <returns>Returns an async Task which result is a List of Members</returns>
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
                    int memberId = reader.GetInt32("ID");
                    string firstName = reader.GetString("FirstName");
                    string surName = reader.GetString("SurName");
                    string phoneNumber = reader.GetString("PhoneNumber");
                    string memberAddress = reader.GetString("Address");
                    string city = reader.GetString("City");
                    string mail = reader.GetString("Mail");
                    MemberType memberType = Enum.GetValues<MemberType>()[reader.GetInt32("TheMemberType")];
                    MemberRole memberRole = Enum.GetValues<MemberRole>()[reader.GetInt32("TheMemberRole")];
                    string image = reader.GetString("Image");
                    string password = reader.GetString("Password");
                    Member member = new Member(memberId, firstName, surName, phoneNumber, memberAddress, city, mail, memberType, memberRole, image, password);

                    foundMembers.Add(member);
                }
                reader.Close();
            }
            return foundMembers;
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