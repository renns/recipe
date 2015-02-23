using System.Collections.Generic;
using System.Xml.Serialization;

namespace RecipeApp.Model
{
    [XmlRootAttribute("recipe")]
    public class Recipe
    {
        public Recipe()
        {
            this.Version = "1";
            this.IngredientSections = new List<IngredientSection>();
            this.StepSections = new List<StepSection>();
            this.Notes = new List<string>();
            this.Categories = new List<string>();
            this.Tags = new List<string>();
        }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("sourceUri")]
        public string SourceUri { get; set; }

        [XmlElement("imageUri")]
        public string ImageUri { get; set; }

        [XmlElement("yield")]
        public string Yield { get; set; }

        [XmlElement("preparationTime")]
        public string PreparationTime { get; set; }

        [XmlElement("cookingTime")]
        public string CookingTime { get; set; }

        [XmlElement("totalTime")]
        public string TotalTime { get; set; }

        [XmlArray("ingredientSections")]
        [XmlArrayItem("ingredientSection")]
        public List<IngredientSection> IngredientSections { get; set; }

        [XmlArray("stepSections")]
        [XmlArrayItem("stepSection")]
        public List<StepSection> StepSections { get; set; }

        [XmlArray("notes")]
        [XmlArrayItem("note")]
        public List<string> Notes { get; set; }

        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<string> Categories { get; set; }

        [XmlArray("tags")]
        [XmlArrayItem("tag")]
        public List<string> Tags { get; set; }
    }
}
