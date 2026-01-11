using ST10363214_PROG_POEP3.Model;
using ST10363214_PROG_POEP3.Util;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10363214_PROG_POEP3
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Recipe currentRecipe;

        public MainWindow()
        {
            InitializeComponent();
            FoodGroupComboBox.ItemsSource = Constants.FOOD_GROUPS;
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe == null)
            {
                MessageBox.Show("Please create and complete a recipe before saving.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentRecipe.SaveOriginalQuantities();

            recipes.Add(currentRecipe);
            RecipesListBox.Items.Add(currentRecipe.Name);

            RecipeNameTextBox.Clear();
            IngredientsListBox.Items.Clear();
            StepsListBox.Items.Clear();

            currentRecipe = null;
            MessageBox.Show("Recipe saved successfully.", "Recipe Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe == null)
            {
                if (string.IsNullOrWhiteSpace(RecipeNameTextBox.Text))
                {
                    MessageBox.Show("Please enter a recipe name first.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentRecipe = new Recipe(RecipeNameTextBox.Text);
            }

            if (string.IsNullOrWhiteSpace(IngredientNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(IngredientQuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(IngredientUnitTextBox.Text) ||
                string.IsNullOrWhiteSpace(IngredientCaloriesTextBox.Text) ||
                FoodGroupComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all ingredient details.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var ingredient = new Ingredient(
                IngredientNameTextBox.Text,
                double.Parse(IngredientQuantityTextBox.Text),
                IngredientUnitTextBox.Text,
                double.Parse(IngredientCaloriesTextBox.Text),
                FoodGroupComboBox.SelectedItem.ToString()
            );

            currentRecipe.Ingredients.Add(ingredient);
            IngredientsListBox.Items.Add($"{ingredient.Name} ({ingredient.Quantity} {ingredient.UnitOfMeasurement}, {ingredient.Calories} calories, {ingredient.FoodGroup})");

            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe == null)
            {
                MessageBox.Show("Please create a recipe and add ingredients before adding steps.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(StepTextBox.Text))
            {
                MessageBox.Show("Please enter a step description.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentRecipe.Steps.Add(StepTextBox.Text);
            StepsListBox.Items.Add(StepTextBox.Text);

            StepTextBox.Clear();
        }

        private void DisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe from the list.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedRecipeName = RecipesListBox.SelectedItem.ToString();
            var selectedRecipe = recipes.Find(r => r.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                var recipeDetails = new StringBuilder();
                recipeDetails.AppendLine($"{Constants.RECIPE_TITLE}: {selectedRecipe.Name}");
                recipeDetails.AppendLine(Constants.INGREDIENTS_TITLE);
                foreach (var ingredient in selectedRecipe.Ingredients)
                {
                    recipeDetails.AppendLine($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
                }
                recipeDetails.AppendLine(Constants.STEPS_TITLE);
                for (int i = 0; i < selectedRecipe.Steps.Count; i++)
                {
                    recipeDetails.AppendLine($"{i + 1}. {selectedRecipe.Steps[i]}");
                }
                recipeDetails.AppendLine($"{Constants.TOTAL_CALORIES_MESSAGE} {selectedRecipe.GetTotalCalories()}");

                MessageBox.Show(recipeDetails.ToString(), "Recipe Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe from the list.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(ScaleComboBox.Text))
            {
                MessageBox.Show("Please enter a scaling factor.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var scale = double.Parse(ScaleComboBox.Text); //exception unhandled*****
            var selectedRecipeName = RecipesListBox.SelectedItem.ToString();
            var selectedRecipe = recipes.Find(r => r.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                selectedRecipe.ScaleIngredients(scale);
                MessageBox.Show("Recipe ingredients scaled successfully.", "Scaling", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            // Clear all fields and lists
            RecipeNameTextBox.Clear();
            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            StepTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
            IngredientsListBox.Items.Clear();
            StepsListBox.Items.Clear();
            RecipesListBox.Items.Clear();
            recipes.Clear();
            currentRecipe = null;

            MessageBox.Show("All recipes have been cleared.", "Clear Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipe from the list.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedRecipeName = RecipesListBox.SelectedItem.ToString();
            var selectedRecipe = recipes.Find(r => r.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                selectedRecipe.ResetQuantities();
                MessageBox.Show("Recipe ingredients reset successfully.", "Resetting", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void FilterByButton_Click(object sender, RoutedEventArgs e)
        {
            FilterByListBox.Items.Clear();

            if (FilterByComboBox.SelectedItem == null || string.IsNullOrWhiteSpace(FilterByTextBox.Text))
            {
                MessageBox.Show("Please select a filter and enter a value.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string filterType = ((ComboBoxItem)FilterByComboBox.SelectedItem).Content.ToString();
            string filterValue = FilterByTextBox.Text.Trim();

            switch (filterType)
            {
                case "a. Ingredient name in recipes":
                    FilterRecipesByIngredientName(filterValue);
                    break;
                case "b. Food group in recipes":
                    FilterRecipesByFoodGroup(filterValue);
                    break;
                case "c. Maximum calories":
                    FilterRecipesByMaxCalories(filterValue);
                    break;
                default:
                    MessageBox.Show("Invalid filter selected.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void FilterRecipesByIngredientName(string ingredientName)
        {
            var filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientName.ToLower())));

            foreach (var recipe in filteredRecipes)
            {
                FilterByListBox.Items.Add(recipe.Name);
            }
        }

        private void FilterRecipesByFoodGroup(string foodGroup)
        {
            var filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.ToLower() == foodGroup.ToLower()));

            foreach (var recipe in filteredRecipes)
            {
                FilterByListBox.Items.Add(recipe.Name);
            }
        }

        private void FilterRecipesByMaxCalories(string maxCalories)
        {
            if (!double.TryParse(maxCalories, out double calories))
            {
                MessageBox.Show("Please enter a valid number for maximum calories.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var filteredRecipes = recipes.Where(r => r.GetTotalCalories() <= calories);

            foreach (var recipe in filteredRecipes)
            {
                FilterByListBox.Items.Add(recipe.Name);
            }
        }


    }
}