namespace CMS.PagesSettings.Forms
{
	public class WysiwygSettings : FieldSettings
	{
		public int Rows { get; set; }
		public override string Control { get { return "ozi_Wysiwyg"; } }
	}
}
