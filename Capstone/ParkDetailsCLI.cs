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
        private ISitesDAO siteDAO;



        public ParkDetailsCLI(ICampgroundsDAO campgroundsDAO, IParksDAO parkDAO, ISitesDAO siteDAO)
        {
            this.campgroundDAO = campgroundsDAO;
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
        }
        //public ICampgroundsDAO IcampgroundsDAO { get; }
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
                    Console.WriteLine("Please enter your preferred campground's number: ");
                    int selectedCampground = int.Parse(Console.ReadLine());
                    Console.WriteLine("What is your arrival date? (Enter as YYYY-MM-DD) ");
                    DateTime selectedFromDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("What is your departure date? (Enter as YYYY-MM-DD) ");
                    DateTime selectedToDate = DateTime.Parse(Console.ReadLine());
                    IList<Site> sites = siteDAO.GetAvailableSites(selectedCampground, selectedFromDate, selectedToDate);
                    foreach (Site site in sites)
                    {
                        Console.WriteLine($"{site.SiteNumber}, {site.MaxOccupancy}, {site.IsAccessible}, {site.MaxRvLength}, {site.HasUtilities}");
                        // TODO Return cost column (campground.DailyFee * length of res
                    }
                }
                if (userSelection == 3)
                {
                    break;
                }
            }
        }
    }
}
