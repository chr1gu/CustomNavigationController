using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Sample.iOS
{
	public partial class TestView : UIViewController
	{
		public TestView () : base ("TestView", null)
		{

		}

		partial void PushAnotherView (NSObject sender)
		{
			NavigationController.PushViewController (new AnotherView (), true);
		}

		partial void PushDialogView (NSObject sender)
		{
			NavigationController.PushViewController (new DialogViewController1 (), true);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

