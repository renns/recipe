using RecipeApp.EventArgs;
using RecipeApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RecipeApp.UI
{
    public partial class RecipeListUserControl : UserControl
    {
        public ObservableCollection<CategoryItem> Categories { get; set; }

        public RecipeListUserControl()
        {
            InitializeComponent();

            this.Categories = new ObservableCollection<CategoryItem>();

            this.tvRecipes.ItemsSource = this.Categories;
        }

        public event EventHandler<RecipeEventArgs> RecipeSelected;

        public void AddRecipe(Recipe recipe)
        {
            this.AddRecipes(new List<Recipe> { recipe });
        }

        public void AddRecipes(IEnumerable<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                var categoryNames = new List<string>(recipe.Categories);

                if (categoryNames.Count == 0)
                {
                    categoryNames.Add("Uncategorized");
                }

                foreach (var categoryName in categoryNames)
                {
                    var category = this.Categories.SingleOrDefault(c => c.CategoryName == categoryName);

                    if (category == null)
                    {
                        category = new CategoryItem(categoryName);
                        category.RecipeItems.Add(new RecipeItem(recipe));
                        this.Categories.Add(category);
                    }
                    else
                    {
                        category.RecipeItems.Add(new RecipeItem(recipe));
                    }
                }
            }
        }

        private void tvRecipes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is RecipeItem)
            {
                var item = (RecipeItem)e.NewValue;

                this.OnRecipeSelected(item.Recipe);
            }
            else if (e.NewValue is CategoryItem)
            {
                this.OnRecipeSelected(null);
            }
        }

        private void tvRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
            }
        }

        private void OnRecipeSelected(Recipe recipe)
        {
            var handler = this.RecipeSelected;

            if (handler != null)
            {
                handler(this, new RecipeEventArgs(recipe));
            }
        }

        private static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source;
        }
    }
}
