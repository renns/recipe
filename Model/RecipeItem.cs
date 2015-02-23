namespace RecipeApp.Model
{
    public class RecipeItem
    {
        public RecipeItem(Recipe recipe)
        {
            this.Recipe = recipe;
        }

        public Recipe Recipe { get; private set; }
    }
}
