using RecipeApp.Events;
using RecipeApp.Import;
using RecipeApp.Model;
using RecipeApp.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp.UI
{
    public partial class MenuUserControl : UserControl
    {
        public MenuUserControl()
        {
            InitializeComponent();
        }

        public event EventHandler<RecipesEventArgs> RecipesImported;

        public event EventHandler EditModeSelected;

        public event EventHandler ViewModeSelected;

        private void ImportOneTsp_Click(object sender, RoutedEventArgs e)
        {
            var recipes = Importer.ImportRecipes(".txt", "Onetsp. Recipes|*.txt", OneTspImporter.Import);
            this.OnRecipesImported(recipes);
        }

        private void EditMode_Checked(object sender, RoutedEventArgs e)
        {
            this.OnEditModeSelected();
        }

        private void EditMode_Unchecked(object sender, RoutedEventArgs e)
        {
            this.OnViewModeSelected();
        }

        private void OnRecipesImported(List<Recipe> recipes)
        {
            var handler = this.RecipesImported;

            if (handler != null)
            {
                handler(this, new RecipesEventArgs(recipes));
            }
        }

        private void OnEditModeSelected()
        {
            var handler = this.EditModeSelected;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private void OnViewModeSelected()
        {
            var handler = this.ViewModeSelected;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
