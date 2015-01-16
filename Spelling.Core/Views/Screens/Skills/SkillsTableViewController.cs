using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml.Serialization;
using Spelling.Core.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Spelling.Core.Views.Screens;

namespace Spelling.Core
{
	partial class SkillsTableViewController : UITableViewController
	{
		public SkillsTableViewController (IntPtr handle) : base (handle)
		{
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            LoadLanguagePacks();

            var packTitles = LanguagePacks.Select(x=>x.Name).Distinct().ToList();
            TableView.DataSource = new SkillsTableDataSource(packTitles);
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (_learnVocab == null)
                _learnVocab = new Learn_Vocabulary();

            _learnVocab.SelectedLanguagePack = LanguagePacks[indexPath.Row];
            _learnVocab.Reset();
            this.PresentViewControllerAsync(_learnVocab, true);
        }

        void LoadLanguagePacks()
        {
            var fileEntries = Directory.GetFiles("vocabulary");
            foreach (var fileName in fileEntries)
            {
                // do something with fileName
                var info = new FileInfo(fileName);
                var _fileName = fileName;
                var name = info.Name;
                if (name.Contains(".xml"))
                {
                    name = name.Replace(".xml", "");

                    var languagePack = new Models.LanguagePack();
                    languagePack.Name = name;
                    languagePack.Info = info;

                    //Get words
                    var sr = new  StreamReader(_fileName);
                    var deserializer = new XmlSerializer(typeof(List<Word>));
                    languagePack.Words = (List<Word>)deserializer.Deserialize(sr);

                    languagePack.NativeLanguage = Client.NativeLanguage;
                    languagePack.TargetLanguage = Client.TargetLanguage;

                    if (LanguagePacks == null)
                        LanguagePacks = new ObservableCollection<LanguagePack>();
                    LanguagePacks.Add(languagePack);
                }
            }
        }

        ObservableCollection<LanguagePack> LanguagePacks;
        Learn_Vocabulary _learnVocab;

	}

}
