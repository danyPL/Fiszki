using System;

namespace Fiszki.Models
{
    public class FlashCard
    {
        public List<string> FirstWord { get; set; } // Wersja nieprzetłumaczona
        public List<string> SecondWord { get; set; } // Wersja przetłumaczona

        public string Language { get; set; }

        public FlashCard(List<string> firstWord, List<string> secondWord,string language)
        {
            FirstWord = firstWord;
            SecondWord = secondWord;
            Language = language;
        }
    }

}




