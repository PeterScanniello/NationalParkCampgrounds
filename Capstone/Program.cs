using Capstone.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");

            IParksDAO parkDAO = new ParksSqlDAO(connectionString);
            ICampgroundsDAO campgroundDAO = new CampGroundsSQLDAO(connectionString);
            ISitesDAO siteDAO = new SitesSQLDAO(connectionString);
            IReservationsDAO reservationDAO = new ReservationsSQLDAO(connectionString);

            MainMenuCLI mainMenuCLI = new MainMenuCLI(parkDAO, campgroundDAO, siteDAO, reservationDAO);
            mainMenuCLI.RunMainMenu();
        }
    }
}
