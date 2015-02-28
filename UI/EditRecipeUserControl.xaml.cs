using RecipeApp.Model;
using System.Windows.Controls;

namespace RecipeApp.UI
{
    public partial class EditRecipeUserControl : UserControl
    {
        public EditRecipeUserControl()
        {
            InitializeComponent();
        }

        public void SetSelectedRecipe(Recipe recipe)
        {
            this.tbTitle.Text = recipe.Title;
            this.tbDescription.Text = recipe.Description;
            this.tbAuthor.Text = recipe.Author;
            this.tbSourceUri.Text = recipe.SourceUri;
            this.tbImageUri.Text = recipe.ImageUri;
            this.tbYield.Text = recipe.Yield;
            this.tbPreparationTime.Text = recipe.PreparationTime;
            this.tbCookingTime.Text = recipe.CookingTime;
            this.tbTotalTime.Text = recipe.TotalTime;
        }
    }
}
