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

        public int CreateNewReservation(Reservation newReservation)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@siteID, @name, @fromDate, @toDate);", conn);
                    cmd.Parameters.AddWithValue("@siteID", newReservation.SiteId);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@fromDate", newReservation.FromDate );
                    cmd.Parameters.AddWithValue("@toDate", newReservation.ToDate);
                    //cmd.Parameters.AddWithValue("@createdDate", newReservation.CreateDate);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation", conn);
                    int newReservationID = Convert.ToInt32(cmd.ExecuteScalar());

                    Console.WriteLine($"Thank you! Your reservation ID is {newReservationID}.");

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
