﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace SampleMicroservices.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {

            new ApiResource("resource_catalog"){Scopes={ "catalog_fullpermission" } },
            new ApiResource("photo_stock_catalog"){Scopes={ "photo_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                    new IdentityResources.Email(),
                    new IdentityResources.OpenId(),//required
                    new IdentityResources.Profile(),
                    new IdentityResource(){Name="roles",DisplayName="Roles",Description="User Roles",UserClaims=new[]{ "role"} }

                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","access the catalog for full permission"),
                new ApiScope("photo_fullpermission","access the photo for full permission"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
              new Client()
              {
                  ClientName = "Asp.Net.Core.Mvc",
                  ClientId="WebMvcClient",
                  ClientSecrets={new Secret("secret".Sha256()) },
                  AllowedGrantTypes=GrantTypes.ClientCredentials,
                  AllowedScopes={ "catalog_fullpermission", "photo_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
              },
                            new Client()
              {
                  ClientName = "Asp.Net.Core.Mvc",
                  ClientId="WebMvcClientForUser",
                  AllowOfflineAccess=true,
                  ClientSecrets={new Secret("secret".Sha256()) },
                  AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                  AllowedScopes={ IdentityServerConstants.StandardScopes.Email,
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    IdentityServerConstants.StandardScopes.OfflineAccess,//refresh token
                                    "roles",
                                    IdentityServerConstants.LocalApi.ScopeName
                                },
                  AccessTokenLifetime=1*60*60,
                  RefreshTokenExpiration=TokenExpiration.Absolute,
                  AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                  RefreshTokenUsage=TokenUsage.ReUse,

              }
            };
    }
}