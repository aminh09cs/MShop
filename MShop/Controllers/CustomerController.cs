using DataAccess.MShop.Entities;
using DataAccess.MShop.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MShop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IMShopUnitOfWork _unitOfWork;

        public CustomerController(IConfiguration configuration, IMShopUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("Login")]
        public async Task<ActionResult> Login(CustomerLogin_RequestData requestData)
        {
            var returnData = new ReturnData();

            // Step 1: Call login
            try
            {
                if (requestData == null || string.IsNullOrEmpty(requestData.username) || string.IsNullOrEmpty(requestData.password))
                {
                    returnData.ReturnCode = (int)CommonLibs.Enum_ReturnCode.DataNotValid;
                    returnData.ReturnMsg = "The input data is invalid";

                    return Ok(returnData);

                }

                requestData.password = CommonLibs.Sercurity.EncryptPassword(requestData.password);
                
                var customer = await _unitOfWork._customerRepository.Login(requestData);

                if(customer == null || customer.id <= 0)
                {
                    returnData.ReturnCode = (int)CommonLibs.Enum_ReturnCode.LoginFail;
                    returnData.ReturnMsg = "Username or password is wrong";

                    return Ok(returnData);
                }
                // Step 2: Return Claim
                // Create claim to save customer data (fullname, id)

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, customer.username),
                    new Claim(ClaimTypes.PrimarySid, customer.id.ToString())
                };

                //Send claims to create token func
                var newAccessToken = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = GenerateRefreshToken();

                //Step 3: Generate refresh token and update to database
                var expiredDateSettingDay = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                var request = new UpdateRefreshTokenExpired_RequestData
                {
                    id = customer.id,
                    refresh_token = refreshToken,
                    refresh_token_expired = DateTime.Now.AddDays(Convert.ToInt32(expiredDateSettingDay))
                };

                var update = await _unitOfWork._customerRepository.UpdateRefreshTokenExpired(request);
                _unitOfWork.SaveChange();

                var customerLoginResponse = new CustomerLogin_ReturnData
                {

                    username = customer.username,
                    token = token,
                    refresh_token = refreshToken,
                    email = customer.email

                };
                return Ok(customerLoginResponse);
            }

            catch (Exception)
            {

                throw;
            }
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
