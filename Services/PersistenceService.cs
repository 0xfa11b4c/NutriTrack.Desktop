using System.IO;
using System.Text.Json;
using NutriTrack.Desktop.Core;
using NutriTrack.Desktop.Models;

namespace NutriTrack.Desktop.Services
{
    public static class PersistenceService
    {
        private static readonly string Folder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NutriTrack");
        private static readonly string FilePath = Path.Combine(Folder, "userData.json");

        public static void Save(NutritionEntry entry)
        {
            if (!Directory.Exists(Folder)) Directory.CreateDirectory(Folder);
            var json = JsonSerializer.Serialize(entry, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public static void Load()
        {
            if (!File.Exists(FilePath)) return;

            var json = File.ReadAllText(FilePath);
            var entry = JsonSerializer.Deserialize<NutritionEntry>(json);
            if (entry != null) AppState.CurrentEntry = entry;
        }
    }
}
