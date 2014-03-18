using System;

namespace Spelling.Core.Models
{
    public class Vocabulary
    {
        public Vocabulary(string language, string word)
        {
            Language = language;
            Word = word;
        }

        public string Language {get; set;}
        public string Word {get; set;}

    }
}

