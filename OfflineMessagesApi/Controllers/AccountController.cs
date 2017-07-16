﻿using Microsoft.AspNet.Identity;
using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OfflineMessagesApi.Controllers
{

    [RoutePrefix("api/accounts")]
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// Returns all the registered users in our system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        /// <summary>
        /// Returns single user by providing it is unique identifier 
        /// </summary>
        /// <param name="Id">Id"User Id</param>
        /// <returns></returns>
        [Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);
            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }
            return NotFound();

        }

        /// <summary>
        /// Responsible to return single user by providing it is username 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);
            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }
            return NotFound();

        }


        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="createUserModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(UserModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User()
            {
                UserName = createUserModel.Username

            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }
    }


}
