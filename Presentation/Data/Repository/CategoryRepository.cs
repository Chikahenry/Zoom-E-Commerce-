using Presentation.Data.Interfaces;
using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Repository
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> Categories => _db.Categories;


    }
}
