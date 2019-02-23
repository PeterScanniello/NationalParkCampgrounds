using Capstone.DAL;
using Capstone.Models;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                Console.WriteLine();
                Console.WriteLine("1) View Campgrounds");
                Console.WriteLine("2) Search for Availability");
                Console.WriteLine("3) Return to Previous Screen");

                int userSelection = int.Parse(Console.ReadLine());

                if(userSelection == 1)
                {

                    Console.WriteLine("".PadRight(5) + "Name".PadRight(30) + "Open".PadRight(20) + "Close".PadRight(20) + "Daily Fee".PadRight(10));
                    foreach (Campground campground in campgrounds)
                    {
                        string openFromMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenFrom);
                        string openToMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campground.OpenTo);
                        Console.WriteLine(campground.CampgroundId.ToString().PadRight(5) + campground.Name.ToString().PadRight(30) + openFromMonth.ToString().PadRight(20) + openToMonth.ToString().PadRight(20) + "$" + campground.DailyFee);
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
                        Console.WriteLine("Site ID".PadRight(10) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(15) + "Utility".PadRight(10) + "Cost".PadRight(10));
                        foreach (Site site in sites)
                        {
                            Console.WriteLine(site.SiteId.ToString().PadRight(10) + site.MaxOccupancy.ToString().PadRight(15) + site.IsAccessible.ToString().PadRight(15) + site.MaxRvLength.ToString().PadRight(15) + site.HasUtilities.ToString().PadRight(10) + totalCost.ToString().PadRight(10));

                            Console.WriteLine();
                            Console.WriteLine("1) Make a Reservation");
                            Console.WriteLine("2) Return to Previous Screen");

                            int reservationSelection = int.Parse(Console.ReadLine());
                            if (reservationSelection == 1)
                            {
                                Console.WriteLine("Which site should be reserved (enter 0 to cancel)");
                                int siteSelection = int.Parse(Console.ReadLine());
                                Console.WriteLine("What name should the reservation be made under?");
                                string reservationName = Console.ReadLine();
                                Reservation newReservation = new Reservation()
                                {
                                    SiteId = siteSelection,
                                    Name = reservationName,
                                    FromDate = selectedFromDate,
                                    ToDate = selectedToDate,
                                    //CreateDate = DateTime.Now
                                };

                                int reservationNumber = reservationDAO.CreateNewReservation(newReservation);
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
