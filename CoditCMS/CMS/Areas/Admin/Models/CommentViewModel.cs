using System.ComponentModel.DataAnnotations;

namespace CMS.Areas.Admin.Models
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		[DataType(DataType.MultilineText)]
		public string Text { get; set; }
	}
}