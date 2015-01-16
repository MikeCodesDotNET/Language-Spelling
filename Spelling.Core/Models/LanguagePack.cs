using System.Collections.Generic;
using System.IO;

namespace Spelling.Core.Models
{
    internal class LanguagePack
    {
        public string Name {get; set;}

        public string NativeLanguage { get; set;}
        public string TargetLanguage { get; set;}

        public Word SelectedWord
        {
            get
            {
                return _selectedWord;
            }
        }

        public void SelectWord(Word word)
        {
            _selectedWord = word;
        }

        public void SelectWord(int index)
        {
            _selectedWord = Words[index];
        }

        public FileInfo Info {get; set;}
        public List<Word> Words = new List<Word>();

        Word _selectedWord;
    }
}

