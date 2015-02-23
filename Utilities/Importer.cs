using Microsoft.Win32;
using RecipeApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace RecipeApp.Utilities
{
    public static class Importer
    {
        public static List<Recipe> ImportRecipes(string fileExtension, string fileFilter, Func<Stream, Recipe> importer)
        {
            var recipes = new List<Recipe>();

            var dialog = new OpenFileDialog();
            dialog.DefaultExt = fileExtension;
            dialog.Filter = fileFilter;
            dialog.Multiselect = true;
            dialog.Title = "Select";

            if (dialog.ShowDialog() == true)
            {
                var errors = new List<string>();

                foreach (var fileName in dialog.FileNames)
                {
                    try
                    {
                        using (var stream = File.OpenRead(fileName))
                        {
                            recipes.Add(importer(stream));
                        }
                    }
                    catch
                    {
                        errors.Add(fileName);
                    }
                }

                if (errors.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Unable to import the following files:");
                    sb.AppendLine();

                    foreach (var fileName in errors)
                    {
                        sb.AppendLine(fileName);
                    }

                    sb.AppendLine();
                    sb.AppendLine("Please send the files to Raph in order to determine why it failed.");

                    MessageBox.Show(sb.ToString(), "Failed Imports", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            return recipes;
        }
    }
}
