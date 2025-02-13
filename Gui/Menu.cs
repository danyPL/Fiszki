using Fiszki.Scripts;
using System;
using System.Text;

namespace Fiszki.Gui
{
    internal class Menu
    {
        bool repeat = true;
        char difMode = 'E';
        bool showDescription = false;
        public Menu()
        {
            while (repeat)
            {
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Witaj w aplikacji do nauki fiszek!");
                Console.ResetColor();
                Console.WriteLine("\nUżyj strzałek w górę lub dół, aby wybrać opcję i zatwierdź \u001b[32mEnterem\u001b[0m:");
                (int left, int top) = Console.GetCursorPosition();
                int option = 1;
                string decorator = "> \u001b[32m";
                ConsoleKeyInfo key;
                bool isSelected = false;

                while (!isSelected)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Rozpocznij naukę\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Ustawienia\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Wyloguj się\u001b[0m");
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
                        TrainingView trainingView = new TrainingView(difMode,showDescription);
                        break;
                    case 2:
                        Settings settingView = new Settings(difMode,showDescription);
                        difMode = settingView.selectedDifficulty;
                        showDescription = settingView.showDescription;
                        break;
                    case 3:
                        repeat = false;
                        break;
                }
            }
        }
    }
}
