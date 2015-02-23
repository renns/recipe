using System.Collections.Generic;
using System.Xml.Serialization;

namespace RecipeApp.Model
{
    public class IngredientSection
    {
        public IngredientSection()
        {
            this.Ingredients = new List<string>();
        }

        [XmlAttribute("heading")]
        public string Heading { get; set; }

        [XmlArray("ingredients")]
        [XmlArrayItem("ingredient")]
        public List<string> Ingredients { get; set; }
    }
}
