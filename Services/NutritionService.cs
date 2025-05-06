using NutriTrack.Desktop.Models;

namespace NutriTrack.Desktop.Services
{
    public class NutritionService
    {
        public NutritionEntry Calculate(NutritionEntry input)
        {
            double leanMass = input.Weight * (1 - input.BodyFat / 100);

            double bmr = input.Gender == "Female"
                ? 10 * input.Weight + 6.25 * input.Height - 5 * input.Age - 161
                : 10 * input.Weight + 6.25 * input.Height - 5 * input.Age + 5;

            double activity = input.Activity switch
            {
                "Low" => 1.2,
                "Light" => 1.375,
                "Moderate" => 1.55,
                "High" => 1.725,
                "Very High" => 1.9,
                _ => 1.55
            };

            double tdee = bmr * activity;

            tdee *= input.Goal switch
            {
                "Cut" => 0.8,
                "Bulk" => 1.15,
                _ => 1.0
            };

            double protein = leanMass * 2.2;
            double fat = leanMass * 0.8;
            double proteinKcal = protein * 4;
            double fatKcal = fat * 9;
            double carbsKcal = tdee - (proteinKcal + fatKcal);
            double carbs = carbsKcal > 0 ? carbsKcal / 4 : 0;

            return new NutritionEntry
            {
                Calories = tdee,
                Protein = protein,
                Fat = fat,
                Carbs = carbs
            };
        }
    }
}
