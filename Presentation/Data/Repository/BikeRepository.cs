using Microsoft.EntityFrameworkCore;
using Presentation.Data.Interfaces;
using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Repository
{
    public class BikeRepository : IBikeRepo
    {
        private readonly ApplicationDbContext _db;

        public BikeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Bike> Bikes => _db.Bikes.Include(c => c.Category);

        public Bike GetBikeByID(int id) => _db.Bikes.FirstOrDefault(p => p.BikeID == id);

    }
}
