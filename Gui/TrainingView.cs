using Fiszki.Models;
using Fiszki.Scripts;
using Fiszki.Scripts.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiszki.Gui
{
    internal class TrainingView
    {
        private FlashCard_Action flashcardActions = new FlashCard_Action();

        private List<string> Quotes = new List<string>() { "Było blisko", "Nuh uh", "Następnym razem się uda" };

        public void Exam(string language, char mode, bool showDescription)
        {
         
            List<FlashCard> allMatchingCards = new List<FlashCard>();
            foreach (var cardList in flashcardActions.flash_cards)
            {
                foreach (var innerList in cardList)
                {
                    foreach (var flashcard in innerList)
                    {
                        if (flashcard.Language.Equals(language, StringComparison.OrdinalIgnoreCase) &&
                            flashcard.Difficulty.Equals(mode.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            allMatchingCards.Add(flashcard);
                        }
                    }
                }
            }
            Console.WriteLine($"Wybierz ile fiszek chcesz się nauczyć (od 1 do {allMatchingCards.Count}):");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Błąd: Podaj liczbę całkowitą większą od 0.");
            }

            List<FlashCard> examCards = allMatchingCards.Take(count).ToList();

            if (examCards.Count == 0)
            {
                Console.WriteLine($"Brak fiszek dla języka: {language} i poziomu trudności: {mode}");
                Console.ReadKey(true);
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Wybrano język: {language} | Poziom trudności: {mode}");
            Console.ResetColor();
            Console.WriteLine("\nRozpoczynamy egzamin.\n");

            Random rnd = new Random();
            examCards = examCards.OrderBy(x => rnd.Next()).ToList();

            int score = Question(examCards, mode,showDescription);

            Console.WriteLine($"Egzamin zakończony. Twój wynik: {score} na {examCards.Count} ({(score * 100) / examCards.Count}%)");
            Console.WriteLine("Naciśnij enter , aby wrócić do menu...");
            Console.ReadKey(true);
        }

           public int Question(List<FlashCard> examCards, char mode,bool showDescription)
        {
            int score = 0;
            Random rnd = new();

            foreach (var flashcard in examCards)
            {
                Console.Clear();
                Console.WriteLine($"Pytanie: {string.Join(", ", flashcard.FirstWord)}");

                if (mode == 'E')
                {
                    string hint = (!showDescription
                        ? GenerateHint(flashcard.SecondWord?.FirstOrDefault()?.ToString() ?? "")
                        : (flashcard.Hint != null
                            ? (flashcard.Language == "PL" ? flashcard.Hint.PL : flashcard.Hint.ENG)
                            : ""));

                    if (!string.IsNullOrWhiteSpace(hint))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Podpowiedź: {hint}");
                        Console.ResetColor();
                    }
                }

                Console.Write("Twoja odpowiedź: ");
                string userInput = Console.ReadLine()?.Trim().ToLower() ?? "";
                var correctAnswers = flashcard.SecondWord.Select(ans => ans.Trim().ToLower()).Distinct().ToList();

                bool isCorrect;

                if (mode == 'M') 
                {
                    var userAnswers = userInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                               .Select(s => s.Trim().ToLower()) 
                                               .ToList();

                   
                    bool allCorrectGiven = !userAnswers.Except(correctAnswers).Any(); 
                    bool allRequiredGiven = !correctAnswers.Except(userAnswers).Any(); 

                    isCorrect = allCorrectGiven && allRequiredGiven;
                }
                else
                {
                    isCorrect = correctAnswers.Contains(userInput.Trim().ToLower());
                }

                Console.ForegroundColor = isCorrect ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(isCorrect ? "Poprawnie!" : $"Niepoprawnie! Prawidłowa odpowiedź to: {string.Join(", ", flashcard.SecondWord)}");
                Console.ResetColor();
                
                if (isCorrect) score++;
                Console.WriteLine("Wciśnij Enter, aby przejść do następnego pytania...");
                Console.ReadLine();
            }

            return score;
        }
   
        private string GenerateHint(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            if (word.Length == 1)
                return word;  

            return word[0] + " " + new string('_', word.Length - 1);
        }

        public TrainingView(char diffMode,bool showDescription)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            HashSet<string> languagesSet = new HashSet<string>();
            foreach (var cardList in flashcardActions.flash_cards)
            {
                foreach (var innerList in cardList)
                {
                    foreach (var flashcard in innerList)
                    {
                        if (!string.IsNullOrEmpty(flashcard.Language))
                        {
                            languagesSet.Add(flashcard.Language);
                        }
                    }
                }
            }

            List<string> languages = languagesSet.ToList();
            if (languages.Count == 0)
            {
                Console.WriteLine("Brak dostępnych języków w danych fiszek.");
                Console.ReadKey(true);
                return;
            }

            int option = 0;
            bool isSelected = false;
            ConsoleKeyInfo key;

            while (!isSelected)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Witaj! Oto lista dostępnych języków do nauki fiszek:");
                Console.ResetColor();
                Console.WriteLine("\nUżyj strzałek (góra/dół) aby wybrać, zatwierdź Enterem:");

                for (int i = 0; i < languages.Count; i++)
                {
                    string prefix = (i == option) ? "> " : "  ";
                    Console.WriteLine($"{prefix}{languages[i]}");
                }

                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = (option == 0) ? languages.Count - 1 : option - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        option = (option == languages.Count - 1) ? 0 : option + 1;
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            string selectedLanguage = languages[option];
            Exam(selectedLanguage, diffMode, showDescription);

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey(true);
        }
    }
}
