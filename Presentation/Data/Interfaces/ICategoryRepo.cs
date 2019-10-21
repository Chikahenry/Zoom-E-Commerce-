using Presentation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Data.Interfaces
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> Categories { get; }

    }
}
