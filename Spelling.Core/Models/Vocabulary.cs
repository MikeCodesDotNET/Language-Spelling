using System;

namespace Spelling.Core.Models
{
    public class Vocabulary
    {
        public Vocabulary(Word word)
        {
            Word = word;
        }

        public string NativeLanguage {get; set;}
        public string TargetLanguage {get; set;}
        public Word Word {get; set;}

    }
}

