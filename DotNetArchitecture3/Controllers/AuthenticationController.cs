using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using DotNetArchitecture3.CustomModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetArchitecture3.CommanClass;
using Microsoft.Extensions.Configuration;
using DotNetArchitecture3.JWTSettings;
using DotNetArchitecture3.Models;
using System.Text.Json;

namespace DotNetArchitecture3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTHelper _IJWTHelper;
        private readonly ElectionCampaignContext _context;
        public AuthenticationController(IJWTHelper jWTHelper,ElectionCampaignContext context)
        {
            _IJWTHelper = jWTHelper;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult userlogin([FromBody] UserLoginCModel userLogin)
        {
            var Userdetails = _context.UserDetails.Where(x=> x.Mobile.Trim() == userLogin.UserName.Trim() || x.Password.Trim() == userLogin.Password.Trim()).FirstOrDefault();


            try
            {
                if (Userdetails == null)
                {
                    return Ok(new ResponseModel { Status = false, Message = "Incorrect UserName and Password" });

                }
                else if (userLogin.UserName != Userdetails.Mobile)
                {
                    return Ok(new ResponseModel { Status = false, Message = "Incorrect UserName. please try again" });

                }
                else if (userLogin.Password != Userdetails.Password)
                {

                    return Ok(new ResponseModel { Status = false, Message = "Incorrect Password. please try again" });

                }
                else if (userLogin.UserName == Userdetails.Mobile && userLogin.Password == Userdetails.Password && Userdetails.IsDelete==false)
                {
                    var token = _IJWTHelper.Authentication(userLogin.UserName, userLogin.Password);
                    var userid = _IJWTHelper.GetUserDetails();

                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpGet]
        [Route("DataCheck")]
        public IActionResult DataGet()
        {
            var api = "In the above example, if we want to pass the claims to our token";

            return Ok(api);
        }

    }
}
