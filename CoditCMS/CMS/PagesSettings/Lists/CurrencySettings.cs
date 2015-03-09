using CMS.Mvc;

namespace CMS.PagesSettings.Lists
{
	public class CurrencySettings : ColSettings
	{
		//TODO не реализовано
		public string Format { get; set; }
		public override string Control { get { return ControlsNames.Currency; } }
	}
}
