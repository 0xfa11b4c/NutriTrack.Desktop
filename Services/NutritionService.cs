using NutriTrack.Desktop.Models;

namespace NutriTrack.Desktop.Services
{
    public class NutritionService
    {
        public NutritionEntry Calculate(NutritionEntry input)
        {
            double leanMass = input.Weight * (1 - input.BodyFat / 100);

            double bmr = 370 + (21.6 * leanMass);

            double activityFactor = input.Activity switch
            {
                "Low" => 1.2,
                "Light" => 1.375,
                "Moderate" => 1.55,
                "High" => 1.725,
                "Very High" => 1.9,
                _ => 1.55
            };

            double tdee = bmr * activityFactor;

            double goalMultiplier = input.Goal switch
            {
                "Cut" => 0.8,
                "Dry" => 0.85,
                "Bulk" => 1.15,
                _ => 1.0
            };

            double proteinPerKg = input.BodyFat switch
            {
                >= 30 => 2.3,
                >= 20 => 2.5,
                >= 10 => 2.7,
                < 10 => 3.0,
                _ => 2.5
            };

            tdee *= goalMultiplier;

            double protein = leanMass * proteinPerKg;
            double fat = leanMass * 0.8;
            double proteinKcal = protein * 4;
            double fatKcal = fat * 9;
            double carbsKcal = tdee - (proteinKcal + fatKcal);
            double carbs = carbsKcal > 0 ? carbsKcal / 4 : 0;

            input.Calories = tdee;
            input.Protein = protein;
            input.Fat = fat;
            input.Carbs = carbs;

            return input;
        }
    }
}
