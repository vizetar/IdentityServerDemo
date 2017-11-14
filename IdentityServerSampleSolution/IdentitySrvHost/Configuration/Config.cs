using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySrvHost.Configuration
{
	public class Config
	{
		// scopes define the API resources in your system
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
		 {
				new ApiResource("api1", "My API"),
		new ApiResource("scope.readaccess", "Example API"),
		new ApiResource("scope.fullaccess", "Example API"),
		new ApiResource("YouCanActuallyDefineTheScopesAsYouLike", "Example API")
		 };
		}
	}
}
