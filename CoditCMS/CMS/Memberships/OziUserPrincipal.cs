using System.Security.Principal;
using System.Web.Security;

namespace CMS.Memberships
{
	public class CoditUserPrincipal : IPrincipal
	{
		public CoditUserPrincipal(IIdentity identity)
		{
			Identity = identity;
		}

		public bool IsInRole(string role)
		{
			return Identity.IsAuthenticated && Roles.IsUserInRole(Identity.Name, role);
		}

		public IIdentity Identity { get; private set; }
	}
}