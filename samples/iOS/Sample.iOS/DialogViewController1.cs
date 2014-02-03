using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

namespace Sample.iOS
{
	public partial class DialogViewController1 : DialogViewController
	{
		public DialogViewController1 () : base (UITableViewStyle.Grouped, null, true)
		{
			Root = new RootElement ("Meals") {
				new Section ("Dinner") {
					new RootElement ("Dessert", new RadioGroup ("dessert", 2)) {
						new Section () {
							new RadioElement ("Ice Cream", "dessert"),
							new RadioElement ("Milkshake", "dessert"),
							new RadioElement ("Chocolate Cake", "dessert")
						}
					}
				}
			};
		}
	}
}
