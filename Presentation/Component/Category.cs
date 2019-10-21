using Microsoft.AspNetCore.Mvc;
using Presentation.Data.Interfaces;
using Presentation.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Component
{
    public class Category : ViewComponent
    {
        private readonly ICategoryRepo _categoryRepository;
        public Category(ICategoryRepo categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Categories.OrderBy(p => p.Name);
            return View(categories);
        }
    }
}
