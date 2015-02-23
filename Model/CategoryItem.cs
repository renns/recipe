using System.Collections.ObjectModel;

namespace RecipeApp.Model
{
    public class CategoryItem
    {
        public CategoryItem(string categoryName)
        {
            this.CategoryName = categoryName;
            this.RecipeItems = new ObservableCollection<RecipeItem>();
        }

        public string CategoryName { get; private set; }

        public ObservableCollection<RecipeItem> RecipeItems { get; private set; }

        public bool Focusable { get; set; }
    }
}
