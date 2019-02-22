using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationsSQLDAO : IReservationsDAO
    {
        private string connectionString;
        public ReservationsSQLDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CreateNewReservation(int siteSelection, string reservationName, DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation VALUES @siteID, @name, @fromDate, @toDate);", conn);
                    cmd.Parameters.AddWithValue("@siteID", siteSelection);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate );
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation", conn);
                    int newReservationID = Convert.ToInt32(cmd.ExecuteScalar());

                    return newReservationID;
                    
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred retrieving the parks list.");
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
