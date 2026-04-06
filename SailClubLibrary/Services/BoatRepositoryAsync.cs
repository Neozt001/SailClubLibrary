using Microsoft.Data.SqlClient;
using SailClubLibrary.Data;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Boat Repository Objects using the interface
    /// </summary>
    public class BoatRepositoryAsync : Connection, IBoatRepositoryAsync
    {
        #region Instance Field
        private string _queryCount = "SELECT COUNT(*) FROM Boats";
        private string _queryString = "SELECT * FROM Boats";
        private string _queryDelete = "DELETE FROM Boats WHERE ID = @ID";
        private string _searchSql = "SELECT * FROM Boats WHERE ID = @ID";
        private string _insertSql = @"INSERT INTO Boats
            (SailNumber, 
                   Model, 
                   Draft, 
                   Width, 
                   Length, 
                   YearOfConstruction, 
                   EngineInfo, 
                   TheBoatType,
                   Image)
            Values(@SailNumber, 
                   @Model, 
                   @Draft, 
                   @Width, 
                   @Length, 
                   @YearOfConstruction, 
                   @EngineInfo, 
                   @BoatType,
                   @Image)";
        private string _queryUpdate = "UPDATE Boats " +
            "SET Model = @Model," +
            " Draft = @Draft," +
            " Width = @Width," +
            " Length = @Length," +
            " YearOfConstruction = @YearOfConstruction," +
            " EngineInfo = @EngineInfo," +
            " TheBoatType = @BoatType," +
            " Image = @Image " +
            " WHERE ID = @ID";
        #endregion

        #region Properties
        //public int Count { get { return _boats.Count; } }
        public Task<int> Count { get { return GetCount(); } }
        #endregion  

        #region Constructor
        public BoatRepositoryAsync()
        {
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
        /// <summary>
        /// Adds a Boat Object to the Dictionary. 
        /// </summary>
        public async Task AddBoat(Boat boat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_insertSql, connection);
                await command.Connection.OpenAsync();
                //command.Parameters.AddWithValue("@ID", member.Id);
                command.Parameters.AddWithValue("@SailNumber", boat.SailNumber);
                command.Parameters.AddWithValue("@Model", boat.Model);
                command.Parameters.AddWithValue("@Draft", boat.Draft);
                command.Parameters.AddWithValue("@Width", boat.Width);
                command.Parameters.AddWithValue("@Length", boat.Length);
                command.Parameters.AddWithValue("@YearOfConstruction", boat.YearOfConstruction);
                command.Parameters.AddWithValue("@EngineInfo", boat.EngineInfo);
                command.Parameters.AddWithValue("@BoatType", boat.TheBoatType);
                command.Parameters.AddWithValue("@Image", boat.Image);
                //int numberOfRow = command.ExecuteNonQuery();
                command.ExecuteNonQuery();
                //Thread.Sleep(1000);
                //return numberOfRow == 1;
                //return member;
            }
            //return false;
        }

        /// <summary>
        /// Collects all the Boats Objects in the Dictionary and files them into a list
        /// </summary>
        public async Task<List<Boat>> GetAllBoats()
        {
            List<Boat> foundBoats = new List<Boat>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryString, connection);
                await command.Connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    int boatId = reader.GetInt32("ID");
                    string sailNumber = reader.GetString("SailNumber");
                    string model = reader.GetString("Model");
                    double draft = reader.GetDouble("Draft");
                    double width = reader.GetDouble("Width");
                    double length = reader.GetDouble("Length");
                    string yearOfConstruction = reader.GetString("YearOfConstruction");
                    string engineInfo = reader.GetString("EngineInfo");
                    BoatType boatType = Enum.GetValues<BoatType>()[reader.GetInt32("TheBoatType")];
                    string image = reader.GetString("Image");
                    Boat boat = new Boat(boatId, sailNumber, model, draft, width, length, yearOfConstruction, engineInfo, boatType, image);
                    foundBoats.Add(boat);
                }
                reader.Close();
            }
            //Console.WriteLine(foundMembers.Count);
            //Console.ReadKey();
            return foundBoats;
        }

        /// <summary>
        /// Removes a Boat Object from the Dictionary
        /// </summary>
        /// 
        public async Task RemoveBoat(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryDelete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                //int numberOfRows = await command.ExecuteNonQueryAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Updates the info of a Boat Object found by parameter with input info
        /// </summary>
        public async Task UpdateBoat(Boat updatedBoat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(_queryUpdate, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", updatedBoat.Id);
                command.Parameters.AddWithValue("@SailNumber", updatedBoat.SailNumber);
                command.Parameters.AddWithValue("@Model", updatedBoat.Model);
                command.Parameters.AddWithValue("@Draft", updatedBoat.Draft);
                command.Parameters.AddWithValue("@Width", updatedBoat.Width);
                command.Parameters.AddWithValue("@Length", updatedBoat.Length);
                command.Parameters.AddWithValue("@YearOfConstruction", updatedBoat.YearOfConstruction);
                command.Parameters.AddWithValue("@EngineInfo", updatedBoat.EngineInfo);
                command.Parameters.AddWithValue("@BoatType", updatedBoat.TheBoatType);
                command.Parameters.AddWithValue("@Image", updatedBoat.Image);
                //int numberOfRow = command.ExecuteNonQuery();
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Searches through the boat dictionary and returns the boat with the given sailnumber. 
        /// </summary>
        public async Task<Boat?> SearchBoat(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Boat boat = new Boat();

                SqlCommand command = new SqlCommand(_searchSql, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int boatId = reader.GetInt32("ID");
                    string sailNumber = reader.GetString("SailNumber");
                    string model = reader.GetString("Model");
                    double draft = reader.GetDouble("Draft");
                    double width = reader.GetDouble("Width");
                    double length = reader.GetDouble("Length");
                    string yearOfConstruction = reader.GetString("YearOfConstruction");
                    string engineInfo = reader.GetString("EngineInfo");
                    BoatType boatType = Enum.GetValues<BoatType>()[reader.GetInt32("TheBoatType")];
                    string image = reader.GetString("Image");
                    boat = new Boat(boatId, sailNumber, model, draft, width, length, yearOfConstruction, engineInfo, boatType, image);
                    reader.Close();
                }

                return boat;
            }
            return null;
        }
        public async Task<List<Boat>> FilterBoats(string filterCriteria)
        {
            List<Boat> bList = [];
            foreach (Boat b in await GetAllBoats())
            {
                if (b.Model.Contains(filterCriteria))
                {
                    bList.Add(b);
                }
            }
            return bList;
        }
    }
        #endregion
}
