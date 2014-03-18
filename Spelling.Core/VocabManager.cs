using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Spelling.Core.Models;

namespace Spelling.Core
{
    public static class VocabManager
    {
        public static List<Word> ImportWords()
        {
            var sr = new  StreamReader("vocabulary/Words.xml");
            var deserializer = new XmlSerializer(typeof(List<Word>));
            var _tempList = (List<Word>)deserializer.Deserialize(sr);
            return _tempList;
        }
    }
}

