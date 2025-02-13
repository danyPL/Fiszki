using Fiszki.Models;
using Fiszki.Scripts.Actions;
using System;
using System.Linq;
using System.Text;

namespace Fiszki.Gui
{
    public class Settings
    {
        public List<char> difModeList = new() { 'E', 'M', 'H' };
        bool repeat = true;
        public bool showDescription = true;
        public char selectedDifficulty { get; private set; } = 'E'; 

        private void SetDifficulty(char difMode)
        {
            if (difModeList.Contains(difMode))
            {
                selectedDifficulty = difMode;
            }
        }
        public void SelectDifficulty()
        {
            Console.Clear();
            (int left, int top) = Console.GetCursorPosition();
            int option = 1;
            string decorator = "> \u001b[32m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine("Wybierz poziom trudności fiszek");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine($"{(option == 1 ? decorator : "   ")}Hard\u001b[0m");
                Console.WriteLine($"{(option == 2 ? decorator : "   ")}Medium\u001b[0m");
                Console.WriteLine($"{(option == 3 ? decorator : "   ")}Easy\u001b[0m");
                Console.WriteLine("-----------------------------------------------------");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 3 : option - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        option = option == 3 ? 1 : option + 1;
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            switch (option)
            {
                case 1:
                    SetDifficulty('H');
                    break;
                case 2:
                    SetDifficulty('M');
                    break;
                case 3:
                    SetDifficulty('E');
                    break;
            }
        }

        public Settings(char currentDifficulty,bool showDescrpt)
        {
            FlashCard_Action flashCard_Action = new FlashCard_Action();

            selectedDifficulty = currentDifficulty;
            showDescription = showDescrpt;
            while (repeat)
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Ustawienia aplikacji");
                Console.ResetColor();
                Console.WriteLine("\nUżyj strzałki w górę lub dół, aby wybrać opcję i zatwierdź \u001b[32mEnterem\u001b[0m:");
                (int left, int top) = Console.GetCursorPosition();
                int option = 1;
                string decorator = "> \u001b[32m";
                ConsoleKeyInfo key;
                bool isSelected = false;

                while (!isSelected)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Poziom trudnosci: {(selectedDifficulty == 'E' ? "Easy" : selectedDifficulty == 'M' ? "Medium" : "Hard")}\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Pokazuj opis(zammiennik dla zwyklych podpowiedzi): {(showDescription ? "Tak" : "Nie")}\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Wyjdz \u001b[0m");
                    Console.WriteLine("-----------------------------------------------------");

                    key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            option = option == 1 ? 3 : option - 1;
                            break;
                        case ConsoleKey.DownArrow:
                            option = option == 3 ? 1 : option + 1;
                            break;
                        case ConsoleKey.Enter:
                            isSelected = true;
                            break;
                    }
                }

                switch (option)
                {
                    case 1:
                        SelectDifficulty();
                        break;
                    case 2:
                        showDescription = !showDescription;
                        break;
                    case 3:
                        repeat = false;
                        break;
                }

                Config config = new Config(selectedDifficulty.ToString(), showDescription,difModeList);
                flashCard_Action.SaveConfig(config);
            }
        }
    }
}
