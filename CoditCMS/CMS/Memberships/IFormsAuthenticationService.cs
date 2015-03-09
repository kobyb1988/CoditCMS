using System.Security.Principal;

namespace CMS.Memberships
{
	public interface IFormsAuthenticationService
	{
		void SignIn(string username, bool createPersistentCookie);
		void SignOut();
		IPrincipal GetPrincipal(string value);
	}
}