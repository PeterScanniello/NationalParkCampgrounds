using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests.DAL;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class CampGroundsSQLDAOTests : NationalParksDAOTests
    {

         [TestMethod]
         public void GetCampgroundTest_ShouldReturn_AllCampgrounds()
         {
             CampGroundsSQLDAO dao = new CampGroundsSQLDAO(ConnectionString);

             IList<Campground> campgrounds = dao.GetCampgrounds(NewParkId);

             Assert.AreEqual(1, campgrounds.Count);
         }
        
    }
}
