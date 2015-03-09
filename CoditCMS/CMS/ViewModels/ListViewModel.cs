using System.Collections.Generic;
using CMS.Infrastructure;
using CMS.PagesSettings;

namespace CMS.ViewModels
{
    public class ListViewModel
    {
        public Settings Settings { get;set; }
        public IEnumerable<object> ListData { get; set; }
    	public FilterParameters FilterParameters { get; set; }
    }
}