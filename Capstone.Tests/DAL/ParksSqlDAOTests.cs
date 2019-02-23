using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests.DAL;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class ParksSqlDAOTests : NationalParksDAOTests
    {
        [TestMethod]
        public void GetParksTest_ShouldReturn_AllParks()
        {
            ParksSqlDAO dao = new ParksSqlDAO(ConnectionString);

            IList<Park> parks = dao.GetAllParks();

            Assert.AreEqual(1, parks.Count);
        }

        [TestMethod]
        public void GetParkDetailsTest_ShouldReturn_SelectedParksDetails()
        {
            ParksSqlDAO dao = new ParksSqlDAO(ConnectionString);

            IList<Park> parks = dao.ReturnParkDetails(NewParkId);

            Assert.AreEqual(1, parks.Count);
        }
    }
}
