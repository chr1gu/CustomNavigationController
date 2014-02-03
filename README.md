# CustomNavigationController

Xamarin.iOS class that simplifies styling of the UINavigationViewController


## How-to use custom Fonts
Download a ttf/otf font, put it into the resources folder and link the reference

Once you’ve added the font, you should then right click it and select the ‘Properties’ menu option. You will need to change the build property ‘Copy to output directory’ to ‘Always copy.’

The next step is to tell iOS where your custom font is stored. iOS will load these fonts at startup, so it pays to use custom fonts sparingly—it can slow the startup time of your App.

To tell iOS you are using custom fonts, you should open the Info.plist file and select ‘Source’ at the bottom of the view. This will change the UI to be more inline with Xcode’s property editor.

Once you’ve done this, you should then double click on ‘Add new entry’ and select the ‘Fonts provided by application’ option. It is here that you input the location of the font so that iOS knows where to look when loading the fonts at startup. Because I have put the font in Resources, I can simply type ‘HollywoodHills.ttf’ as the value. If I had put the font in Resources/Fonts, then I would set the value to ‘Fonts/HollywoodHills.ttf’.

Usage:
```csharp
UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
	Font = UIFont.FromName("Ail et Fines Herbes", 25f)
});
```
