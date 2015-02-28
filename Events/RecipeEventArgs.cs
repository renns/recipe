using RecipeApp.Model;
using System;

namespace RecipeApp.Events
{
    public class RecipeEventArgs : EventArgs
    {
        public RecipeEventArgs(Recipe recipe)
        {
            this.Recipe = recipe;
        }

        public Recipe Recipe { get; private set; }
    }
}
