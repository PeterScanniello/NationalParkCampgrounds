using Capstone.DAL;
using Capstone.Models;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class MainMenuCLI : CLIHelper
    {
        private IParksDAO parkDAO;
        private ICampgroundsDAO campgroundDAO;
        private ISitesDAO siteDAO;
        private IReservationsDAO reservationDAO;

        public MainMenuCLI(IParksDAO parkDAO, ICampgroundsDAO campgroundDAO, ISitesDAO siteDAO, IReservationsDAO reservationDAO)
        {
            this.parkDAO = parkDAO;
            this.campgroundDAO = campgroundDAO;
            this.siteDAO = siteDAO;
        }

        public IParksDAO IParksDAO { get; }

        public void RunMainMenu()
        {
            while(true)
            {
                Console.WriteLine("Welcome to the National Parks Database!");
                Console.WriteLine("Select a park for further details.");
                IList<Park> allParks = parkDAO.GetAllParks();
                foreach(Park park in allParks)
                {
                    Console.WriteLine($"{park.ParkId}) {park.Name}");

                }
                Console.WriteLine("Q) Quit");

                string mainChoice = Console.ReadLine();

                try
                {
                    if (mainChoice == "q" || mainChoice == "Q")
                    {
                        Console.Clear();
                        break;

                    }
                    else
                    {
                        int mainChoiceInt = int.Parse(mainChoice);
                        if (mainChoiceInt <= allParks.Count && mainChoiceInt > 0)
                        {
                            IList<Park> parkDetails = parkDAO.ReturnParkDetails(mainChoiceInt);
                            foreach (Park park in parkDetails)
                            {
                                Console.WriteLine($"{park.Name}");
                                Console.WriteLine($"Location: {park.Location}");
                                Console.WriteLine($"Established: {park.EstablishedDate}");
                                Console.WriteLine($"Area: {park.Area}");
                                Console.WriteLine($"Annual Visitors: {park.Visitors}");
                                Console.WriteLine($"{park.Description}");

                                ParkDetailsCLI parkDetailsMenu = new ParkDetailsCLI(campgroundDAO, parkDAO, siteDAO, reservationDAO);
                                parkDetailsMenu.ParkDetailsMenu(mainChoiceInt);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.WriteLine(ex.Message);
                }

                



                // TryParse: after checking for Q, try to parse user's input into int; check that int is within Count
                //Console.WriteLine();
                //

                //if (mainChoice == "1")
                //{
                //    ReturnParkDetails(int parkId)
                //}
            }
        }
    }
}
