// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Spelling.Dutch
{
	[Register ("Learn_Vocabulary")]
	partial class Learn_Vocabulary
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnSkipTimer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDutchTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDutchWord { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblEnglishTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblEnglishWord { get; set; }
		
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
