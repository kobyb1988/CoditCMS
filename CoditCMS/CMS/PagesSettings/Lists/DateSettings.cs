using CMS.Mvc;

namespace CMS.PagesSettings.Lists
{
	public class DateSettings : ColSettings
	{
		public string Format { get; set; }
		public override string Control { get { return ControlsNames.Date; } }
	}
}
