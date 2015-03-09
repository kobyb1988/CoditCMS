using System;
using System.Web.Mvc;
using Libs.Services;

namespace CMS.Services
{
	public class Logger : ILogger
	{
		public Logger()
		{
		}

		private static ILogger _instance;

		public static ILogger Instance 
		{
			get { return _instance ?? (_instance = DependencyResolver.Current.GetService<ILogger>()); }
		}

		public void LogException(Exception exception, string message = null)
		{
			
		}
	}
}