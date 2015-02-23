using RecipeApp.Model;

namespace RecipeApp.EventArgs
{
    public class RecipeEventArgs : System.EventArgs
    {
        public RecipeEventArgs(Recipe recipe)
        {
            this.Recipe = recipe;
        }

        public Recipe Recipe { get; private set; }
    }
}
