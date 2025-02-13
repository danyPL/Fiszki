using System.Collections.Generic;

namespace Fiszki.Models
{
    public class Config
    {
        public string DifficultyMode { get; set; }
        public bool ShowDescription { get; set; }
        public List<char> DifficultyLevels { get; set; }

        public Config(string difficultyMode, bool showDescription, List<char> difficultyLevels)
        {
            DifficultyMode = difficultyMode;
            ShowDescription = showDescription;
            DifficultyLevels = difficultyLevels ?? new List<char> { 'E', 'M', 'H' };
        }
    }
}
