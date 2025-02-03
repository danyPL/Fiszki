﻿using Fiszki.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki.Gui
{
    internal class Menu
    {
        bool repeat = true;

        public Menu()
        {
            while (repeat)
            {
                // do przeanalizowania przez film i zmodyfikowania wedlug wlasnego pomyslu

                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Witaj oficjalnie w do nauki Fiszek!");
                Console.ResetColor();
                Console.WriteLine("\nUżyj strzałki w góre lub dól aby wybrać opcje i zatwierdź \u001b[32mEnterem\u001b[0m:");
                (int left, int top) = Console.GetCursorPosition();
                var option = 1;
                var decorator = "> \u001b[32m";
                ConsoleKeyInfo key;
                bool isSelected = false;

                while (!isSelected)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("-----------------------------------------------------");

                    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Rozpocznij nauke\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Ustawienia\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Wyloguj się\u001b[0m");
                    Console.WriteLine("-----------------------------------------------------");

                    key = Console.ReadKey(false);

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
                        TrainingView trainingView = new();
                        break;
                    case 2:
                        ConsoleSpinner spinner = new ConsoleSpinner();
                        spinner.Delay = 300;
                        bool repeat = true;
                        while (repeat)
                        {
                            spinner.Turn(displayMsg: "Rozpoczynanie treningu ", sequenceCode: 5);
                           
                        }
                        break;
                    case 3:
                        repeat = false;
                        break;
                }

            }
        }
    }
}
