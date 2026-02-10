using SailClubLibrary.Services;
using SailClubLibrary.Models;

namespace TestBoatApp2026
{
    [TestClass]
    public sealed class BoatRepositoryTest
    {
        [TestMethod]
        public void CountTest()
        {
            //Arrange 
            BoatRepository repo = new BoatRepository();

            //Act


            //Assert
            Assert.AreEqual(repo.Count, 2); 
        }

        [TestMethod]
        public void AddBoatTest()
        {
            //Arrange
            BoatRepository repo = new BoatRepository();
            Boat b = new Boat(3, BoatType.TERA, "TERA", "11111", "EngineInfo", 1, 2, 3, "2020");

            //Act 
            repo.AddBoat(b);

            //Assert
            Assert.AreEqual(b, repo.SearchBoat("11111"));
        }

        [TestMethod]
        public void GetAllBoatsTest()
        {
            //Arrange
            BoatRepository repo = new BoatRepository();
            //Act
            
            //Assert
            Assert.AreEqual(repo.GetAllBoats().Count, 2);
        }
    }
}
