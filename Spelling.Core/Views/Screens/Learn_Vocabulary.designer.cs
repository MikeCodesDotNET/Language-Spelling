// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;
using UIKit;

namespace Spelling.Core.Views.Screens
{
	[Register ("Learn_Vocabulary")]
	partial class Learn_Vocabulary
	{
		[Outlet]
		UIButton btnSkipTimer { get; set; }

		[Outlet]
		UILabel lblDutchTitle { get; set; }

		[Outlet]
		UILabel lblDutchWord { get; set; }

		[Outlet]
		UILabel lblEnglishTitle { get; set; }

		[Outlet]
		UILabel lblEnglishWord { get; set; }
	
		void ReleaseDesignerOutlets ()
		{
			if (lblEnglishTitle != null) {
				lblEnglishTitle.Dispose ();
				lblEnglishTitle = null;
			}

			if (lblEnglishWord != null) {
				lblEnglishWord.Dispose ();
				lblEnglishWord = null;
			}

			if (lblDutchTitle != null) {
				lblDutchTitle.Dispose ();
				lblDutchTitle = null;
			}

			if (lblDutchWord != null) {
				lblDutchWord.Dispose ();
				lblDutchWord = null;
			}

			if (btnSkipTimer != null) {
				btnSkipTimer.Dispose ();
				btnSkipTimer = null;
			}
		}
	}
}
