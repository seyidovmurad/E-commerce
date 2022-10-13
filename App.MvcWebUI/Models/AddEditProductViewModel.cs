using App.Entities.Concrete;

namespace App.MvcWebUI
{
    public class AddEditProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public bool IsActionAdd { get; internal set; }
    }
}