using System.Collections.Generic;

namespace Fiszki.Models
{
    // Klasa reprezentująca pojedynczą fiszkę z pliku JSON
    public class FlashcardJson
    {
        public string difficulty { get; set; }
        public List<string> PL { get; set; }
        public List<string> ENG { get; set; }
        public FlashcardHint hint { get; set; }
    }

    public class FlashcardHint
    {
        public string? PL { get; set; }
        public string? ENG { get; set; }
        public FlashcardHint(string? pl,string? eng) {
            PL = pl;
            ENG = eng;
        }  
    }

    // Klasa opakowująca listę fiszek (odpowiada strukturze JSON)
    public class FlashcardsRoot
    {
        public List<FlashcardJson> flashcards { get; set; }
    }

    // Klasa reprezentująca fiszkę używaną w aplikacji
    public class FlashCard
    {
        public List<string> FirstWord { get; set; }   // Pytanie (w zależności od języka)
        public List<string> SecondWord { get; set; }    // Odpowiedź (odwrotność pytania)
        public string Language { get; set; }            // "PL" lub "ENG"
        public string Difficulty { get; set; }          // Poziom trudności, np. "E", "M", "H"
        public FlashcardHint Hint { get; set; }

        public FlashCard(List<string> firstWord, List<string> secondWord, string language, string difficulty,FlashcardHint hint)
        {
            FirstWord = firstWord;
            SecondWord = secondWord;
            Language = language;
            Difficulty = difficulty;
            Hint = hint;
        }
    }
}
