using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace CustomNavigation
{
	public class CustomNavigationController: UINavigationController
	{
		public CustomNavigationBar CustomNavigationBar;
		public UIColor FontColor;
		public UIColor TextColor
		{
			set {
				CustomNavigationBar.TintColor = value;
				if (CustomNavigationBar.RespondsToSelector (new MonoTouch.ObjCRuntime.Selector ("BarTintColor"))) {
					CustomNavigationBar.BarTintColor = value;
				}
				UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes () {
					TextShadowColor = UIColor.Clear,
					TextColor = value
				}, UIControlState.Normal);
				CustomNavigationBar.SetTitleTextAttributes (new UITextAttributes () {
					TextShadowColor = UIColor.Clear,
					TextColor = value
				});

			}
		}
		public UIImage BackButtonBackgroundImage;
		public UIImage BackButtonIcon;

		public CustomNavigationController (UIViewController viewcontroller) : base (viewcontroller)
		{
			SetNavigationBarHidden (true, false);
			CustomNavigationBar = new CustomNavigationBar(this);
			var height = 44;
			if (UIDevice.CurrentDevice.CheckSystemVersion (7,0)) {
				height = 64;
			}

			// to have the same height 

			// styling
			// back button

			//UIBarButtonItem.Appearance.SetBackButtonBackgroundImage (UIImage.FromFile ("monkey.png"), UIControlState.Normal, UIBarMetrics.Default);

			// custom font how-to:
			// download ttf font and link reference into resources folder
			// Once you’ve added the font, you should then right click it and select the ‘Properties’ menu option. You will need to change the build property ‘Copy to output directory’ to ‘Always copy.’
			// The next step is to tell iOS where your custom font is stored. iOS will load these fonts at startup, so it pays to use custom fonts sparingly—it can slow the startup time of your App.
			// To tell iOS you are using custom fonts, you should open the Info.plist file and select ‘Source’ at the bottom of the view. This will change the UI to be more inline with Xcode’s property editor.
			// Once you’ve done this, you should then double click on ‘Add new entry’ and select the ‘Fonts provided by application’ option. It is here that you input the location of the font so that iOS knows where to look when loading the fonts at startup. Because I have put the font in Resources, I can simply type ‘HollywoodHills.ttf’ as the value. If I had put the font in Resources/Fonts, then I would set the value to ‘Fonts/HollywoodHills.ttf’.
			// usage: UIFont.FromName("Ail et Fines Herbes", 20f)

			CustomNavigationBar.Frame = new System.Drawing.RectangleF(0, 0, View.Bounds.Width, height);
			CustomNavigationBar.Translucent = true;
			CustomNavigationBar.BackgroundColor = new UIColor (new CGColor (0,0,0,0.5f));
			CustomNavigationBar.ShadowImage = null;
			CustomNavigationBar.SetBackgroundImage (null, UIBarMetrics.Default);
			CustomNavigationBar.PushNavigationItem (viewcontroller.NavigationItem, false);
			CustomNavigationBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			View.Add (CustomNavigationBar);
		}

		public override UIViewController PopViewControllerAnimated (bool animated)
		{
			CustomNavigationBar.PopNavigationItemAnimated (animated, false);
			return base.PopViewControllerAnimated (animated);
		}

		public override void PushViewController (UIViewController viewController, bool animated)
		{
			UpdateOffset (viewController.View);
			base.PushViewController (viewController, animated);

			if (CustomNavigationBar != null) {
				var backButton = new UIBarButtonItem (NSObjectFlag.Empty);
				backButton.Title = viewController.NavigationItem.Title;

				if (BackButtonIcon != null)
					backButton.Image = BackButtonIcon;

				//backButton.SetBackgroundImage (BackButtonBackgroundImage, UIControlState.Normal, UIBarMetrics.Default);
				//backButton.SetBackButtonBackgroundImage (UIImage.FromFile ("empty.gif"), UIControlState.Normal, UIBarMetrics.Default);
				//backButton.SetBackButtonBackgroundImage (BackButtonIcon, UIControlState.Normal, UIBarMetrics.Default);
				//backButton.I
				

				if (CustomNavigationBar.TopItem != null) {
					CustomNavigationBar.TopItem.BackBarButtonItem = backButton;
					//CustomNavigationBar.TopItem.SetHidesBackButton (true, false);
					//CustomNavigationBar.TopItem.LeftItemsSupplementBackButton = false;
				}

				CustomNavigationBar.PushNavigationItem (viewController.NavigationItem, animated);
			}
		}

		protected void UpdateOffset (object view)
		{
			var height = 44;
			var scrollView = view as UIScrollView;

			// this only works if the view controller's View is deriving from UIScrollView
			if (scrollView != null) {
				var inset = scrollView.ContentInset;
				inset.Top = height;
				scrollView.ContentInset = inset;
				var offset = scrollView.ContentOffset;
				offset.Y = -height;
				scrollView.ContentOffset = offset;
				var scrollInset = scrollView.ScrollIndicatorInsets;
				scrollInset.Top = height;
				scrollView.ScrollIndicatorInsets = scrollInset;
				scrollView.ScrollRectToVisible (new RectangleF (0,-height,0,0), false);
			}
		}
	}

	public class CustomNavigationBar : UINavigationBar
	{
		UINavigationController CustomNavigationController;

		public CustomNavigationBar (UINavigationController controller)
		{
			CustomNavigationController = controller;
		}

		public override void Draw (System.Drawing.RectangleF rect)
		{
			// keep this method empty to not render the whole gradient shizzle on iOS6 and bellow
		}

		public UINavigationItem PopNavigationItemAnimated (bool animated, bool popViewController)
		{
			if (popViewController) {
				CustomNavigationController.PopViewControllerAnimated (animated);
				return null;
			}
			return base.PopNavigationItemAnimated (animated);
		}

		public override UINavigationItem PopNavigationItemAnimated (bool animated)
		{
			return PopNavigationItemAnimated (animated, true);
		}
	}
}

