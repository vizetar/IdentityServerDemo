using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;


namespace IdentitySrvHost.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                ///////////////////////////////////////////
                // Console Client Credentials Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "client",

                    ClientSecrets =
                    {
                        new Secret("secret")
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowOfflineAccess=true,
                    AllowedScopes = { "custom.profile" , "sampleapi" }
                },

             
                ///////////////////////////////////////////
                // Console Resource Owner Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "roclient",
                    ClientSecrets =
                    {
                        new Secret("secretapi")
                    },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                       IdentityServerConstants.StandardScopes.OpenId , "custom.profile" , "sampleapi"
                    }
                }

            };
        }
    }
}
