using CMS.Mvc;

namespace CMS.PagesSettings.Lists
{
	public class ImageSettings : ColSettings
	{
		public string ImageWidth { get; set; }
		public override string Control { get { return ControlsNames.Image; } }
	}
}
