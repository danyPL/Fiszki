using Fiszki.Models;
using Fiszki.Scripts;
using Fiszki.Scripts.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Fiszki.Gui
{
    internal class TrainingView
    {
        FlashCard_Action flashcardActions = new FlashCard_Action();
        List<string> Quotes = new List<string>() { "Bylo blisko","Nuh uh","Nastepnym razem sie uda"};
        public void Exam(string language)
        {
            // Utwórz instancję, która ładuje dane fiszek
            FlashCard_Action flashcardActions = new FlashCard_Action();

            // Zbierz wszystkie fiszki, które mają pasujący język
            List<FlashCard> examCards = new List<FlashCard>();
            foreach (var cardList in flashcardActions.flash_cards)
            {
                foreach (var innerList in cardList)
                {
                    foreach (var flashcard in innerList)
                    {
                        if (flashcard.Language.Equals(language, StringComparison.OrdinalIgnoreCase))
                        {
                            examCards.Add(flashcard);
                        }
                    }
                }
            }

            // Jeśli brak fiszek dla danego języka, informujemy użytkownika i kończymy
            if (examCards.Count == 0)
            {
                Console.WriteLine($"Brak fiszek dla języka: {language}");
                Console.ReadKey(true);
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Wybrano język: {language}");
            Console.ResetColor();
            Console.WriteLine("\nRozpoczynamy egzamin.\n");

            int score = 0;
            Random rnd = new Random();

            // tasujemy fiszki :>
            examCards = examCards.OrderBy(x => rnd.Next()).ToList();

            // Dynamicznie aktualizuje ilość zdobytych punktów poprzez zwracanie odpowiedniego wyniku z danego pytania
            score = Question(examCards, score);

           
            Console.WriteLine($"Egzamin zakończony. Twój wynik: {score} na {examCards.Count} {score * examCards.Count}%");
            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey(true);
        }

        public int Question(List<FlashCard> examCards, int score)
        {
            Random rnd = new();
            foreach (var flashcard in examCards)
            {
                Console.Clear();
                string questionText = string.Join(", ", flashcard.FirstWord);

                string correctAnswers = string.Join(", ", flashcard.SecondWord);

                Console.WriteLine($"Pytanie: {questionText}");
                Console.Write("Twoja odpowiedź: ");
                string userAnswer = Console.ReadLine().Trim();

                // Sprawdzamy, czy odpowiedź użytkownika znajduje się w liście poprawnych odpowiedzi (bez uwzględniania wielkości liter)
                bool isCorrect = flashcard.SecondWord.Any(answer => string.Equals(answer, userAnswer, StringComparison.OrdinalIgnoreCase));
                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Poprawnie!");
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Niepoprawnie! Prawidłowa odpowiedź to: {correctAnswers}");
                }
                Console.ResetColor();
                Console.WriteLine("Wciśnij enter, aby przejść do następnego pytania!");
                ConsoleSpinner spinner = new ConsoleSpinner();
                spinner.Delay = 300;
                bool repeat = true;
                while (repeat)
                {
                    spinner.Turn(displayMsg: $"{Quotes[rnd.Next(0,Quotes.Count)]}", sequenceCode: 6);

                }
                Console.ReadKey(true);
               
            }
            return score;
        }

        public TrainingView()
        {
            // Tworzymy instancję, która ładuje dane fiszek

            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            // Zbieramy unikalne języki z fiszek
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

            // Interfejs wyboru języka
            int option = 0;
            ConsoleKeyInfo key;
            bool isSelected = false;
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

            // Wybrany język
            string selectedLanguage = languages[option];

            Exam(selectedLanguage);

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey(true);
        }
    }
}
