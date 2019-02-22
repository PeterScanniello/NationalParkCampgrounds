using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SitesSQLDAO : ISitesDAO
    {
        private string connectionString;
        public SitesSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Site> GetAvailableSites(int campgroundId, DateTime fromDate, DateTime toDate)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT site.site_number, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities
                                FROM campground
                                JOIN site ON campground.campground_id = site.campground_id
                                LEFT JOIN reservation ON site.site_id = reservation.site_id
                                WHERE site.campground_id = @enteredCampgroundId 
                                AND (SELECT MONTH(@enteredFromDate) AS Month) BETWEEN 
                                campground.open_from_mm AND campground.open_to_mm
                                AND (SELECT MONTH(@enteredToDate) AS Month) BETWEEN 
                                campground.open_from_mm AND campground.open_to_mm
                                AND site.site_id NOT IN 
                                (SELECT site.site_id FROM site 
                                LEFT JOIN reservation ON site.site_id = reservation.site_id
                                WHERE (((reservation.from_date BETWEEN @enteredFromDate AND @enteredToDate)
                                AND (reservation.to_date BETWEEN  @enteredFromDate AND @enteredToDate))
                                OR ((reservation.from_date >= Convert(datetime, @enteredFromDate))
                                AND (reservation.from_date <= Convert(datetime, @enteredToDate)))
                                OR ((reservation.to_date >= Convert(datetime, @enteredFromDate))
                                AND (reservation.to_date <= Convert(datetime, @enteredToDate)))))
                                GROUP BY site.SiteNumber, site.MaxOccupancy, site.IsAccessible, site.MaxRvLength, site.HasUtilities;", conn);
                    cmd.Parameters.AddWithValue("@enteredCampgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@enteredFromDate", fromDate);
                    cmd.Parameters.AddWithValue("@enteredToDate", toDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertReaderToSite(reader);
                        sites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred retrieving the site availability list.");
                Console.WriteLine(ex.Message);
                throw;
            }
            return sites;
        }

        private Site ConvertReaderToSite(SqlDataReader reader)
        {
            Site site = new Site();
            site.SiteId = Convert.ToInt32(reader["site_id"]);
            site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
            site.SiteNumber = Convert.ToInt32(reader["site_number"]);
            site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.IsAccessible = Convert.ToBoolean(reader["accessible"]);
            site.MaxRvLength = Convert.ToDouble(reader["max_rv_length"]);
            site.HasUtilities = Convert.ToBoolean(reader["utilities"]);

            return site;
        }
    }
}
