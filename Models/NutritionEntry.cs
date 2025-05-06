namespace NutriTrack.Desktop.Models
{
    public class NutritionEntry
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public double BodyFat { get; set; }

        public string Gender { get; set; } = "Male";
        public string Goal { get; set; } = "Maintain";
        public string Activity { get; set; } = "Moderate";
        
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbs { get; set; }
    }
}