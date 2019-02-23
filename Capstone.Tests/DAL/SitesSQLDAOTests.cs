using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests.DAL;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class SitesSQLDAOTests : NationalParksDAOTests
    {
        [TestMethod]
        public void GetSitesTest_ShouldReturn_AvailableSites()
        {
            SitesSQLDAO dao = new SitesSQLDAO(ConnectionString);

            IList<Site> sites = dao.GetAvailableSites(NewCampgroundId);

            Assert.AreEqual(1, parks.Count);
        }
    }
}
