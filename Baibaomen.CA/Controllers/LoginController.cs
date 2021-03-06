﻿using Baibaomen.CA.ViewModels;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Baibaomen.CA.Controllers
{
    /// <summary>
    /// Handle CA logins.
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        static readonly string CAAddress = System.Configuration.ConfigurationManager.AppSettings["CA.Address"];
        static readonly string CAClientId = System.Configuration.ConfigurationManager.AppSettings["CA.Client.Id"];
        static readonly string CAClientSecret = System.Configuration.ConfigurationManager.AppSettings["CA.Client.Secret"];
        static readonly string CAClientScopes = System.Configuration.ConfigurationManager.AppSettings["CA.Client.Scopes"];

        /// <summary>
        /// Login and get access token.
        /// </summary>
        /// <param name="loginView"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginView loginView)
        {

            var resp = await GetUserTokenAsync(loginView.Account, loginView.Password);
            return Ok(new { AccessToken = resp.AccessToken });
        }

        //[Route("logout")]
        //[HttpPost]
        //public IHttpActionResult Logout() {

        //}

        async Task<TokenResponse> GetUserTokenAsync(string account, string password)
        {
            var client = new TokenClient(
                CAAddress + "connect/token",
                CAClientId,
                CAClientSecret);

            return await client.RequestResourceOwnerPasswordAsync(account, password, CAClientScopes);
        }
    }
}
