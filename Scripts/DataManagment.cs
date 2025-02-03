using Fiszki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    abstract class DataManagment : Paths, IDataManagement
    {
        public List<List<List<FlashCard>>> flash_cards = new List<List<List<FlashCard>>>();
        public virtual void LoadData()
        {
            flash_cards.Clear();

            if (!File.Exists(PathFlashCards))
                return;

            using (StreamReader reader = new StreamReader(PathFlashCards))
            {
                string line;
                string language = ""; // Zmienna na przechowanie informacji o językach
                bool isFirstLine = true; // Flaga, czy to pierwsza linijka

                // Pattern dla podziału pytania i odpowiedzi
                string pattern = @"^(?'questions'[^\-]*)\s*-\s*(?'anwsers'.*)$";

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0)
                        continue; // pomijamy puste linie

                    if (isFirstLine)
                    {
                        if (line.StartsWith("LG:", StringComparison.OrdinalIgnoreCase))
                        {
                            language = line.Substring(3).Trim();

                            isFirstLine = false;
                            continue; 
                        }
                        isFirstLine = false;
                    }

                    // Przetwarzamy kolejne linie jako dane flashcardów
                    Match match = Regex.Match(line, pattern);
                    if (match.Success)
                    {
                        string questionGroups = match.Groups["questions"].Value.Trim();
                        string anwsersGroups = match.Groups["anwsers"].Value.Trim();

                        List<string> questions = questionGroups.Split('|').Select(x => x.Trim()).ToList();
                        List<string> anwsers = anwsersGroups.Split('|').Select(x => x.Trim()).ToList();
                        
                        // Dodajemy kartę do flash_cards; zakładamy, że FlashCard ma konstruktor przyjmujący
                        // listy pytań, odpowiedzi oraz informację o językach.
                        flash_cards.Add(new List<List<FlashCard>>
                {
                    new List<FlashCard>
                    {
                        new FlashCard(questions, anwsers, language)
                    }
                });
                    }
                }
            }
        }


   
      




    }
}
