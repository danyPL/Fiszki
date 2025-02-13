using Fiszki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiszki.Scripts.Actions
{
    internal class FlashCard_Action : DataManagment
    {
        public FlashCard_Action()
        {
            LoadData();
        }

        public void DisplayData()
        {
            Console.WriteLine("[");
            foreach (var cardList in flash_cards)
            {
                Console.WriteLine("  [");
                foreach (var innerList in cardList)
                {
                    foreach (var flashcard in innerList)
                    {
                        string firstWords = string.Join(", ", flashcard.FirstWord.Select(s => $"\"{s}\""));
                        string secondWords = string.Join(", ", flashcard.SecondWord.Select(s => $"\"{s}\""));
                        // Język jest pojedynczym stringiem – wystarczy wyświetlić go bez join
                        Console.WriteLine(flashcard.Language);
                        Console.WriteLine($"    [[{firstWords}], [{secondWords}]],");
                    }
                }
                Console.WriteLine("  ],");
            }
            Console.WriteLine("]");
        }
    }
}
