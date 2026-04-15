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
    /// 
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


        public Task<int> Count { get { return GetCount(); } }
         

        
        public BoatRepositoryAsync()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="boat"></param>
        /// <returns></returns>
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
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            return foundBoats;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveBoat(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(_queryDelete, connection);
                await command.Connection.OpenAsync();
                command.Parameters.AddWithValue("@ID", id);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedBoat"></param>
        /// <returns></returns>
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
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
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
}