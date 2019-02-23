using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace Capstone.Tests.DAL
{
    [TestClass]
    public class NationalParksDAOTests
    {
        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Database=npcampground;Trusted_Connection=True;";

        /// <summary>
        /// Holds the newly generated reservation id.
        /// </summary>
        protected int NewReservationId { get; private set; }

        /// <summary>
        /// Holds the newly generated park id.
        /// </summary>
        protected int NewParkId { get; private set; }


        /// <summary>
        /// Holds the newly generated campground id.
        /// </summary>
        protected int NewCampgroundId { get; private set; }

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            // Begin the transaction
            transaction = new TransactionScope();

            // Get the SQL Script to run
            string sql = File.ReadAllText("DAL\\test-script.sql");

            // Execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // If there is a row to read
                if (reader.Read())
                {
                    this.NewParkId = Convert.ToInt32(reader["newParkId"]);
                    this.NewReservationId = Convert.ToInt32(reader["newReservationId"]);
                    this.NewCampgroundId = Convert.ToInt32(reader["newCampgroundId"]);
                }
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        /// <summary>
        /// Gets the row count for a table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
