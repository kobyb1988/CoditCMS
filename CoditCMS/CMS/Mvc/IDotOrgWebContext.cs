using System.Web.Mvc;
using DB.Entities;

namespace CMS.Mvc
{
	public interface IDotOrgWebContext
	{
		IMetadataEntity Metadata { get; set; }
		dynamic ViewBag { get; }
		ViewDataDictionary ViewData { get; set; }
	}
}