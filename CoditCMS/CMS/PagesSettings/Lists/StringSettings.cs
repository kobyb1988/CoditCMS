using CMS.Mvc;

namespace CMS.PagesSettings.Lists
{
	public class StringSettings : ColSettings
	{
		//TODO не реализовано
		public int Maxlength { get; set; }
		public override string Control { get { return ControlsNames.String; } }
	}
}
