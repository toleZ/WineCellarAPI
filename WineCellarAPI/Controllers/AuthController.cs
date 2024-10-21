﻿using Common.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WineCellarApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] CredentialsForAuthenticateDTO credentials)
        {
            User? userAuthenticated = _userService.Authenticate(credentials);

            if (userAuthenticated == null) { return Unauthorized("Invalid credentials."); }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Los claims son datos en clave->valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", userAuthenticated.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
            claimsForToken.Add(new Claim("given_name", userAuthenticated.Username)); //Lo mismo para given_name y family_name, son las convenciones para nombre y apellido. Ustedes pueden usar lo que quieran, pero si alguien que no conoce la app

            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              signature);

            string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

    }
}