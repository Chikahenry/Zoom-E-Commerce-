using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Interfaces
{
    public interface IBikeRepo
    {
        IEnumerable<Bike> Bikes { get; }
        Bike GetBikeByID(int id);
    }
}
