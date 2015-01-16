using System;
using System.Xml.Serialization;
using System.IO;
using Spelling.Core.Models;
using Spelling.Core;
using Foundation;
using System.Collections.ObjectModel;

namespace Spelling
{
    /// <summary>
    /// Current App session. Deals with sending feedback, keeping track of the user coins etc...
    /// </summary>
    public static class Client
    {

        public static void Initialize(string targetLanguage, string nativeLanguage)
        {
            TargetLanguage = targetLanguage;
            NativeLanguage = nativeLanguage;
        }

        public static string TargetLanguage { get; private set; }
        public static string NativeLanguage { get; private set; }

    }
}
    