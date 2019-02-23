using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.DAL;
using Capstone.Models;
using Capstone.Tests.DAL;
using System;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class ReservationsSQLDAOTests : NationalParksDAOTests
    {
        //[TestMethod]
        //public void GetReservationTest_ShouldReturn_AllReservations()
        //{
        //    Reservation reservation = new Reservation();
        //    reservation.SiteId = NewSiteId;
        //    reservation.Name = "New Reservation";
        //    reservation.FromDate = Convert.ToDateTime("2019-05-20");
        //    reservation.ToDate = Convert.ToDateTime("2019-05-24");

        //    ReservationsSQLDAO dao = new ReservationsSQLDAO(ConnectionString);
        //    int startingRowCount = GetRowCount("reservation");

        //    dao.CreateNewReservation(reservation);

        //    int endingRowCount = GetRowCount("reservation");
        //    Assert.AreNotEqual(startingRowCount, endingRowCount);

        //}
    }
}

