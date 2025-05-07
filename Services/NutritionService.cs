using NutriTrack.Desktop.Models;
using System.Diagnostics;

namespace NutriTrack.Desktop.Services
{
    public class NutritionService
    {
        public NutritionEntry Calculate(NutritionEntry input)
        {
            double leanMass = input.Weight * (1 - input.BodyFat / 100);
            Debug.WriteLine($"[LOG] Lean Body Mass: {leanMass:F2} kg");

            double bmr = 370 + (21.6 * leanMass);
            Debug.WriteLine($"[LOG] BMR (Katch-McArdle): {bmr:F2} kcal");

            double activityFactor = input.Activity switch
            {
                "Low" => 1.2,
                "Light" => 1.375,
                "Moderate" => 1.55,
                "High" => 1.725,
                "Very High" => 1.9,
                _ => 1.55
            };
            Debug.WriteLine($"[LOG] Activity Level: {input.Activity}, Factor: {activityFactor}");

            double tdee = bmr * activityFactor;
            Debug.WriteLine($"[LOG] TDEE before goal: {tdee:F2} kcal");

            double goalMultiplier = input.Goal switch
            {
                "Cut" => 0.8,
                "Bulk" => 1.15,
                _ => 1.0
            };
            Debug.WriteLine($"[LOG] Goal: {input.Goal}, Multiplier: {goalMultiplier}");

            tdee *= goalMultiplier;
            Debug.WriteLine($"[LOG] Final TDEE: {tdee:F2} kcal");

            double protein = leanMass * 2.2;
            double fat = leanMass * 0.8;
            double proteinKcal = protein * 4;
            double fatKcal = fat * 9;
            double carbsKcal = tdee - (proteinKcal + fatKcal);
            double carbs = carbsKcal > 0 ? carbsKcal / 4 : 0;

            Debug.WriteLine($"[LOG] Protein: {protein:F2} g ({proteinKcal:F2} kcal)");
            Debug.WriteLine($"[LOG] Fat: {fat:F2} g ({fatKcal:F2} kcal)");
            Debug.WriteLine($"[LOG] Carbs: {carbs:F2} g ({carbsKcal:F2} kcal)");

            input.Calories = tdee;
            input.Protein = protein;
            input.Fat = fat;
            input.Carbs = carbs;

            return input;
        }
    }
}
