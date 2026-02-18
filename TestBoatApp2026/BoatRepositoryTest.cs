using SailClubLibrary.Models;
using SailClubLibrary.Services;
using System.Linq;

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
        [TestMethod]
        public void RemoveBoatTest()
        {
            //Arrange 
            BoatRepository repo = new BoatRepository();
            //Act
            repo.RemoveBoat("16-3335");
            //Assert
            Assert.IsNull(repo.SearchBoat("16-3335"));
            
            ////Arrange 
            //BoatRepository repo = new BoatRepository();
            //Boat? testBoat = repo.SearchBoat("16-3335");
            ////Act
            //repo.RemoveBoat("16-3335");
            ////Assert
            //Assert.IsFalse(repo.GetAllBoats().Contains(testBoat));
        }
        [TestMethod]
        public void UpdateBoatTest()
        {
            //Arrange
            BoatRepository repo = new BoatRepository();
            Boat testBoat = new Boat(1, BoatType.TERA, "Model", "16-3335", "Is  not very good :3", 22, 13, 23, "1982");
            //Act
            repo.UpdateBoat(testBoat);
            //Assert
            Assert.IsTrue(repo.SearchBoat("16-3335").Draft == 22);
        }
        [TestMethod]
        public void SearchBoatTest()
        {
            //Arrange
            BoatRepository repo = new BoatRepository();
            //Act
            Boat testBoat = repo.SearchBoat("16-3335");
            //Assert
            Assert.AreEqual("16-3335", testBoat.SailNumber);
        }
        //[TestMethod]
        //public void PrintAllBoats()
        //{

        //}
        [TestMethod]
        public void FilterBoatsTest()
        {
            //Arrange
            BoatRepository repo = new BoatRepository();
            //Act
            //Assert
            Assert.AreEqual(2, repo.FilterBoats("Model").Count);
        }

    }
}
