using System.Collections.Generic;
using CMS.PagesSettings.Forms;

namespace CMS.PagesSettings
{
	public class TabsSettings
	{
		public List<FieldSettings> Fields { get; set; }
		public string Name { get; set; }
		public int Sort { get; set; }
	}
}
