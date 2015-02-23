using RecipeApp.Model;
using System.Collections.Generic;

namespace RecipeApp.EventArgs
{
    public class RecipesEventArgs : System.EventArgs
    {
        public RecipesEventArgs(IEnumerable<Recipe> recipes)
        {
            var recipesList = new List<Recipe>();
            recipesList.AddRange(recipes);
            this.Recipes = recipesList;
        }

        public IEnumerable<Recipe> Recipes { get; private set; }
    }
}
