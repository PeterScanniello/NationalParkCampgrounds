using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISitesDAO
    {
        IList<Site> GetAvailableSites(int campgroundId, DateTime fromDate, DateTime toDate);
    }
}
