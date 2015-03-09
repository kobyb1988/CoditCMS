using CMS.Mvc;

namespace CMS.PagesSettings.Lists
{
	public class CreateGlobalActionSettings : GlobalActionSettings
	{
		public override string Control { get { return ControlsNames.Create; } }
	}
}
