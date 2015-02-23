using System.Collections.Generic;
using System.Xml.Serialization;

namespace RecipeApp.Model
{
    public class StepSection
    {
        public StepSection()
        {
            this.Steps = new List<string>();
        }

        [XmlAttribute("heading")]
        public string Heading { get; set; }

        [XmlArray("steps")]
        [XmlArrayItem("step")]
        public List<string> Steps { get; set; }
    }
}
