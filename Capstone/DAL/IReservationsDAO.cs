using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationsDAO
    {
        int CreateNewReservation(Reservation newReservation);
    }
}
