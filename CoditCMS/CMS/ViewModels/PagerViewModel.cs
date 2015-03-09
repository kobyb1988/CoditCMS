using System;
using System.Web.Mvc;

namespace CMS.ViewModels
{
	public class PagerViewModel
	{
		public int Page { get; set; }
		public int ItemsCount { get; set; }
		public int PageSize { get; set; }

		public Func<int, ActionResult> Action { get; set; }

		public int PageCount
		{
			get
			{
				var pages = ItemsCount / PageSize;
				if (pages * PageSize < ItemsCount)
					pages++;
				return pages;
			}
		}
	}
}