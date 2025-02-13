using Fiszki.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    public abstract class DataManagment : Paths, IDataManagement
    {
        public List<List<List<FlashCard>>> flash_cards = new List<List<List<FlashCard>>>();
        public Config config;
        public void LoadConfig()
        {
            if (!File.Exists(PathConfig))
                return;

            string json = File.ReadAllText(PathConfig);
            config = JsonSerializer.Deserialize<Config>(json) ?? new Config("Easy", true, new List<char> { 'E', 'M', 'H' });
            
        }

        public virtual void SaveConfig(Config conifgU)
        {
            string json = JsonSerializer.Serialize(conifgU, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(PathConfig, json);
        }
        public virtual void LoadData()
        {
            flash_cards.Clear();
            LoadConfig();
            if (!File.Exists(PathFlashCards))
                return;

            string json = File.ReadAllText(PathFlashCards);
            FlashcardsRoot root = JsonSerializer.Deserialize<FlashcardsRoot>(json);
            if (root != null && root.flashcards != null)
            {
                foreach (var fc in root.flashcards)
                {

                    FlashcardHint hintPL = new FlashcardHint(fc.hint.PL, fc.hint.ENG);
                    FlashcardHint hintENG = new FlashcardHint(fc.hint.PL, fc.hint.ENG);

                    FlashCard cardPL = new FlashCard(fc.PL, fc.ENG, "PL", fc.difficulty,hintPL);

                    FlashCard cardENG = new FlashCard(fc.ENG, fc.PL, "ENG", fc.difficulty,hintENG);

                    flash_cards.Add(new List<List<FlashCard>> { new List<FlashCard> { cardPL } });
                    flash_cards.Add(new List<List<FlashCard>> { new List<FlashCard> { cardENG } });
                }
            }
        }
    }
}
