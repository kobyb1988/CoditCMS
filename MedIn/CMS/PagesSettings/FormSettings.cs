using System.Collections.Generic;
using System.Linq;
using CMS.PagesSettings.Forms;

namespace CMS.PagesSettings
{
	public class FormSettings
	{
		public List<TabsSettings> Tabs { get; set; }
		public bool? Localizable { get; set; }

		public List<FieldSettings> Fields
		{
			get { return Tabs.OrderBy(t => t.Sort).SelectMany(t => t.Fields).ToList(); }
		}
	}
}
