using Capstone.DAL;
using Capstone.Models;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ParkDetailsCLI : CLIHelper
    {
        private ICampgroundsDAO campgroundDAO;
        private IParksDAO parkDAO;

        public ParkDetailsCLI()
        {

        }
      
        public ParkDetailsCLI(ICampgroundsDAO campgroundsDAO, IParksDAO parkDAO)
        {
            this.campgroundDAO = campgroundDAO;
            this.parkDAO = parkDAO;
        }
        public ICampgroundsDAO IcampgroundsDAO { get; }
        public void ParkDetailsMenu(int parkId)
        {
            while (true)
            {
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservations");
                Console.WriteLine("3) Return to Previous Screen");

                int userSelection = int.Parse(Console.ReadLine());

                if(userSelection == 1)
                {
                    IList<Campground> campgrounds = campgroundDAO.GetCampgrounds(parkId);
                    foreach (Campground campground in campgrounds)
                    {
                        Console.WriteLine($"{campground.CampgroundId}, {campground.Name}, {campground.OpenFrom}, {campground.OpenTo}, {campground.DailyFee}");
                    }
                }
                if (userSelection == 2)
                {
                   // SearchForReservations();
                }
                if (userSelection == 3)
                {
                    break;
                }
            }
        }
    }
}
