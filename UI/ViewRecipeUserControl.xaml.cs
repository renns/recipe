using RecipeApp.Model;
using RecipeApp.Utilities;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace RecipeApp.UI
{
    public partial class ViewRecipeUserControl : UserControl
    {
        private XPathDocument transform;

        public ViewRecipeUserControl()
        {
            InitializeComponent();

            // TODO: Move this out of the constructor so that it can render in design view
            using (var fs = File.OpenRead(Path.Combine(FileSystem.TransformsDirectory, "Default.xslt")))
            {
                this.transform = new XPathDocument(fs);
            }
        }

        public void SetSelectedRecipe(Recipe recipe)
        {
            if (recipe == null)
            {
                this.htmlPanel.Text = string.Empty;
            }
            else
            {
                var doc = FileSystem.SerializeRecipe(recipe);
                var sb = new StringBuilder();

                using (var reader = doc.CreateReader())
                using (var writer = new StringWriter(sb))
                {
                    var xslt = new XslCompiledTransform();
                    xslt.Load(this.transform);
                    xslt.Transform(reader, null, writer);
                }

                this.htmlPanel.Text = sb.ToString();
            }
        }
    }
}
