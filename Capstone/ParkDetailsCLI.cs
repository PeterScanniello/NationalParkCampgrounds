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
        private IReservationsDAO reservationDAO;



        public ParkDetailsCLI(ICampgroundsDAO campgroundsDAO, IParksDAO parkDAO, ISitesDAO siteDAO, IReservationsDAO reservationDAO)
        {
            this.campgroundDAO = campgroundsDAO;
            this.parkDAO = parkDAO;
            this.siteDAO = siteDAO;
            this.reservationDAO = reservationDAO;
        }
        //public ICampgroundsDAO IcampgroundsDAO { get; }
        public void ParkDetailsMenu(int parkId)
        {
            IList<Campground> campgrounds = campgroundDAO.GetCampgrounds(parkId);

            while (true)
            {
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Reservations");
                Console.WriteLine("3) Return to Previous Screen");

                int userSelection = int.Parse(Console.ReadLine());

                if(userSelection == 1)
                {
                    
                    foreach (Campground campground in campgrounds)
                    {
                        Console.WriteLine($"{campground.CampgroundId}, {campground.Name}, {campground.OpenFrom}, {campground.OpenTo}, {campground.DailyFee:C0}");
                    }
                }
                if (userSelection == 2)
                {
                    try
                    {
                        
                        //TODO if user enters a date with no campsites available, return to asking them to enter a new date


                        Console.WriteLine("Please enter your preferred campground's number: ");
                        int selectedCampground = int.Parse(Console.ReadLine());
                        Console.WriteLine("What is your arrival date? (Enter as YYYY-MM-DD) ");
                        DateTime selectedFromDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("What is your departure date? (Enter as YYYY-MM-DD) ");
                        DateTime selectedToDate = DateTime.Parse(Console.ReadLine());

                        TimeSpan ts = selectedToDate - selectedFromDate;
                        decimal totalCost = ts.Days * campgrounds[selectedCampground - 1].DailyFee;

                        IList<Site> sites = siteDAO.GetAvailableSites(selectedCampground, selectedFromDate, selectedToDate);
                        if(sites.Count == 0)
                        {
                            Console.WriteLine("There are no available sites, please enter another date range");
                           

                        }
                        foreach (Site site in sites)
                        {
                            Console.WriteLine($"{site.SiteNumber}, {site.MaxOccupancy}, {site.IsAccessible}, {site.MaxRvLength}, {site.HasUtilities}, {totalCost}");

                            Console.WriteLine("1) Search for Available Reservation");
                            Console.WriteLine("2) Return to Previous Screen");

                            int reservationSelection = int.Parse(Console.ReadLine());
                            if (reservationSelection == 1)
                            {
                                Console.WriteLine("Which site should be reserved (enter 0 to cancel)");
                                int siteSelection = int.Parse(Console.ReadLine());
                                Console.WriteLine("What name should the reservation be made under?");
                                string reservationName = Console.ReadLine();
                                int reservationNumber = reservationDAO.CreateNewReservation(siteSelection, reservationName, selectedFromDate, selectedToDate);
                                Console.WriteLine($"The reservation has been made and the confirmation id is {reservationNumber}");
                            }
                            if (reservationSelection == 2)
                            {
                                break;
                            }
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.WriteLine(ex.Message);
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
