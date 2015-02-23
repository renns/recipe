using RecipeApp.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RecipeApp.Utilities
{
    public static class FileSystem
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Recipe));

        public static readonly string RecipeDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Recipes");
        public static readonly string TransformsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Transforms");

        public static IEnumerable<Recipe> LoadCollection(string collectionDirectory)
        {
            return Directory.EnumerateFiles(collectionDirectory, "*.recipe").Select(LoadRecipe);
        }

        public static Recipe LoadRecipe(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return Serializer.Deserialize(reader) as Recipe;
            }
        }

        public static void SaveRecipe(Recipe recipe, string directory)
        {
            var fileName = recipe.Title + ".recipe";

            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName.Replace(c.ToString(), string.Empty);
            }

            var path = Path.Combine(directory, fileName);

            using (var writer = new StreamWriter(path))
            {
                Serializer.Serialize(writer, recipe);
            }
        }

        public static XDocument SerializeRecipe(Recipe recipe)
        {
            var xDocument = new XDocument();

            using (var writer = xDocument.CreateWriter())
            {
                Serializer.Serialize(writer, recipe);
            }

            return xDocument;
        }
    }
}
