using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki.Models
{
    public class FlashCard
    {
        public int Id { get; set; }
        public string First_L {  get; set; }
        public string Second_L { get; set;}
        public string Typed { get; set; }
        

        public string FirstWord { get; { 
            switch (Typed)
                {
                    case Translation.Single_T:

                        break;
                    case Translation.Multiply_T:
                        string[] t = firstWord.Split('|');

                        break;
                    case Translation.Sentence_T:

                        break;
                }

            }; }
        public string SecondWord { get; set; }
    }
}
