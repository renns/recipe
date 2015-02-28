using RecipeApp.Model;
using System;
using System.Collections.Generic;

namespace RecipeApp.Events
{
    public class RecipesEventArgs : EventArgs
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
