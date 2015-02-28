using RecipeApp.Events;
using RecipeApp.Utilities;
using System;
using System.IO;
using System.Windows;

namespace RecipeApp.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Directory.CreateDirectory(FileSystem.RecipeDirectory);

            var recipes = FileSystem.LoadCollection(FileSystem.RecipeDirectory);

            this.ucRecipeList.AddRecipes(recipes);
            this.ucRecipeList.RecipeSelected += RecipeSelectedHandler;

            this.ucMenu.RecipesImported += RecipesImportedHandler;
            this.ucMenu.EditModeSelected += EditModeSelectedHandler;
            this.ucMenu.ViewModeSelected += ViewModeSelectedHandler;

            // Load list of Transforms from Transforms dir and send to ViewRecipeUserControl
        }

        void RecipeSelectedHandler(object sender, RecipeEventArgs e)
        {
            this.ucViewRecipe.SetSelectedRecipe(e.Recipe);
            this.ucEditRecipe.SetSelectedRecipe(e.Recipe);
        }

        void RecipesImportedHandler(object sender, RecipesEventArgs e)
        {
            foreach (var recipe in e.Recipes)
            {
                FileSystem.SaveRecipe(recipe, FileSystem.RecipeDirectory);
            }

            this.ucRecipeList.AddRecipes(e.Recipes);
        }

        void EditModeSelectedHandler(object sender, EventArgs e)
        {
            this.ucViewRecipe.Visibility = Visibility.Hidden;
            this.ucEditRecipe.Visibility = Visibility.Visible;
        }

        void ViewModeSelectedHandler(object sender, EventArgs e)
        {
            this.ucViewRecipe.Visibility = Visibility.Visible;
            this.ucEditRecipe.Visibility = Visibility.Hidden;
        }
    }
}
