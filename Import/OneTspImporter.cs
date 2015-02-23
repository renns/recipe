using RecipeApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecipeApp.Import
{
    public static class OneTspImporter
    {
        public static Recipe Import(Stream stream)
        {
            List<string> lines;

            using (var reader = new StreamReader(stream))
            {
                lines = ReadLines(reader);
            }

            ValidateLines(lines);

            return ParseLines(lines);
        }

        private static List<string> ReadLines(TextReader reader)
        {
            var lines = new List<string>();

            var line = reader.ReadLine();

            while (line != null)
            {
                if (line != string.Empty)
                {
                    lines.Add(line);
                }

                line = reader.ReadLine();
            }

            return lines;
        }

        private static void ValidateLines(List<string> lines)
        {
            if (lines.Count < 3 ||
                GetLineType(lines[0]) != LineType.Header ||
                GetLineType(lines.Last()) != LineType.Footer ||
                GetLineType(lines[1]) != LineType.Unknown)
            {
                throw new Exception("Not a valid OneTsp file");
            }
        }

        private static Recipe ParseLines(List<string> lines)
        {
            var state = LineType.Title;
            var recipe = new Recipe();
            var currentIngredientSection = new IngredientSection();
            var currentStepSection = new StepSection();

            foreach (var line in lines)
            {
                if (state == LineType.Title && GetLineType(line) == LineType.Unknown)
                {
                    recipe.Title = line;
                    state = LineType.Description;
                }
                else if (state == LineType.Description && GetLineType(line) == LineType.Unknown)
                {
                    recipe.Description = line;
                    state = LineType.Unknown;
                }
                else if (GetLineType(line) == LineType.Author)
                {
                    recipe.Author = line.Substring(8);
                }
                else if (GetLineType(line) == LineType.SourceUri)
                {
                    recipe.SourceUri = line.Substring(5);
                }
                else if (GetLineType(line) == LineType.Yield)
                {
                    recipe.Yield = line.Substring(7);
                }
                else if (GetLineType(line) == LineType.PreparationTime)
                {
                    recipe.PreparationTime = line.Substring(11);
                }
                else if (GetLineType(line) == LineType.CookingTime)
                {
                    recipe.CookingTime = line.Substring(14);
                }
                else if (GetLineType(line) == LineType.TotalTime)
                {
                    recipe.TotalTime = line.Substring(12);
                }
                else if (GetLineType(line) == LineType.Ingredients)
                {
                    state = LineType.Ingredients;
                }
                else if (state == LineType.Ingredients && GetLineType(line) == LineType.Heading)
                {
                    if (currentIngredientSection.Ingredients.Count > 0)
                    {
                        recipe.IngredientSections.Add(currentIngredientSection);
                    }

                    currentIngredientSection = new IngredientSection { Heading = line.Substring(3, line.Length - 6) };
                }
                else if (state == LineType.Ingredients && GetLineType(line) == LineType.Unknown)
                {
                    currentIngredientSection.Ingredients.Add(line);
                }
                else if (GetLineType(line) == LineType.Steps)
                {
                    state = LineType.Steps;
                }
                else if (state == LineType.Steps && GetLineType(line) == LineType.Heading)
                {
                    if (currentStepSection.Steps.Count > 0)
                    {
                        recipe.StepSections.Add(currentStepSection);
                    }

                    currentStepSection = new StepSection { Heading = line.Substring(3, line.Length - 6) };
                }
                else if (state == LineType.Steps && GetLineType(line) == LineType.Unknown)
                {
                    currentStepSection.Steps.Add(Regex.Replace(line, @"^[0-9]+\. ", string.Empty));
                }
                else if (GetLineType(line) == LineType.Notes)
                {
                    state = LineType.Notes;
                }
                else if (state == LineType.Notes && GetLineType(line) == LineType.Unknown)
                {
                    recipe.Notes.Add(line);
                }
            }

            if (currentIngredientSection.Ingredients.Count > 0)
            {
                recipe.IngredientSections.Add(currentIngredientSection);
            }

            if (currentStepSection.Steps.Count > 0)
            {
                recipe.StepSections.Add(currentStepSection);
            }

            return recipe;
        }

        private static LineType GetLineType(string line)
        {
            if (line == "----- Recipe exported from One tsp. (ver 0.1)")
            {
                return LineType.Header;
            }
            else if (line.StartsWith("Source: "))
            {
                return LineType.Author;
            }
            else if (line.StartsWith("URL: "))
            {
                return LineType.SourceUri;
            }
            else if (line.StartsWith("Yield: "))
            {
                return LineType.Yield;
            }
            else if (line.StartsWith("Prep time: "))
            {
                return LineType.PreparationTime;
            }
            else if (line.StartsWith("Cooking time: "))
            {
                return LineType.CookingTime;
            }
            else if (line.StartsWith("Total time: "))
            {
                return LineType.TotalTime;
            }
            else if (line == "INGREDIENTS")
            {
                return LineType.Ingredients;
            }
            else if (line == "DIRECTIONS")
            {
                return LineType.Steps;
            }
            else if (line.StartsWith("-- ") && line.EndsWith(" --"))
            {
                return LineType.Heading;
            }
            else if (line == "NOTES")
            {
                return LineType.Notes;
            }
            else if (line == "----- Recipe end")
            {
                return LineType.Footer;
            }
            else
            {
                return LineType.Unknown;
            }
        }
    }
}
