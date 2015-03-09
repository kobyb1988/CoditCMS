using System.ComponentModel.DataAnnotations;

namespace $rootnamespace$.Areas.Admin.Models
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		[DataType(DataType.MultilineText)]
		public string Text { get; set; }
	}
}