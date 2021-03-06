﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationManagementDBEntity.Models;
using NotificationManagementDBEntity.Repositories;
using UserManagement.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement
{
	[Route("api/v1")]
    [ApiController]
	public class UserController : Controller
	{
        private readonly IUserManagementHelper _iUserManagementHelper;
        public UserController(IUserManagementHelper iUserManagementHelper)
        {
            _iUserManagementHelper = iUserManagementHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(int userId)
        {
            try
            {
                return Ok( await _iUserManagementHelper.GetUser(userId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDetails1"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(UserDetails userDetails1)
        {
            try
            {
                UserDetails userDetails= await _iUserManagementHelper.Login(userDetails1.UserName, userDetails1.UserPassword);
                if (userDetails == null)
                    return Ok("Invalid User");
                else
                    return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserDetails userDetails)
        {
            try
            {
                return Ok(await _iUserManagementHelper.RegisterUser(userDetails));
            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDetails userDetails)
        {
            try
            {
                await _iUserManagementHelper.UpdateUser(userDetails);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
