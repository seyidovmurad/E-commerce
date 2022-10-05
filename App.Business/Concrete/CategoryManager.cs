using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{

    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(int id)
        {
            _categoryDal.Delete(_categoryDal.Get(c => c.CategoryId == id));
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

    }
}
