using Sales.DAL;
using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL
{
    public class CategoryServices
    {
        private eTools2021Context _context;

        internal CategoryServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<CategoryInfo> Category_FetchCategory()
        {
            IEnumerable<CategoryInfo> categories = _context.Categories
                                                        .Select(x => new CategoryInfo
                                                        {
                                                            CategoryID = x.CategoryID,
                                                            Description = x.Description
                                                        });
            return categories.ToList();
        }
    }
}
