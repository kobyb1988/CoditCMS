using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace CMS.Memberships.Implementations
{
	public class FormsAuthenticationService : IFormsAuthenticationService
	{
		public void SignIn(string username, bool createPersistentCookie)
		{
			var userData = new CoditIdentity(username);
			var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, CalculateCookieExpirationDate(), createPersistentCookie, userData.ToString());
			var ticket = FormsAuthentication.Encrypt(authTicket);
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public IPrincipal GetPrincipal(string encryptedTicket)
		{
			return new CoditUserPrincipal(GetIdentity(encryptedTicket));
		}

		private IIdentity GetIdentity(string encryptedTicket)
		{
			return new CoditIdentity(Decrypt(encryptedTicket));
		}

		private static DateTime CalculateCookieExpirationDate()
		{
			return DateTime.Now.Add(FormsAuthentication.Timeout);
		}

		private static FormsAuthenticationTicket Decrypt(string encryptedTicket)
		{
			return FormsAuthentication.Decrypt(encryptedTicket);
		}
	}
}