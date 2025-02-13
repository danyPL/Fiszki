using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    public abstract class Paths
    {
        protected string PathFlashCards { get; private set; }
        protected string PathConfig { get; private set; }


        protected Paths()
        {
            PathFlashCards = Path.Combine(Directory.GetCurrentDirectory(), "data", "fiszki.json");
            PathConfig = Path.Combine(Directory.GetCurrentDirectory(), "data", "config.json");
            
        }
    }
}
