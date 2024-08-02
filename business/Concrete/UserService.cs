
using core.Abstract;
using data_access.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using data_access.Models;
using core.Helpers;
using business.Abstract;


namespace business.Concrete
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,

            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }



        public async Task<IServiceOutput> Register(RegisterModel model)
        {
            try
            {
                // E-posta adresi ve kullanıcı adının benzersizliğini kontrol et
                var existingUserByEmail = await _userManager.FindByEmailAsync(model.email);
                var existingUserByUsername = await _userManager.FindByNameAsync(model.username);

                if (existingUserByEmail != null)
                {
                    return await ServiceOutput.GenerateAsync(422, false, "Bu e-posta adresi zaten kullanılıyor.");
                }

                if (existingUserByUsername != null)
                {
                    return await ServiceOutput.GenerateAsync(422, false, "Bu kullanıcı adı zaten kullanılıyor.");
                }

                // Yeni kullanıcı oluştur
                var user = new AppUser
                {
                    UserName = model.username,
                    Email = model.email,
                };

                var result = await _userManager.CreateAsync(user, model.password);

                if (!result.Succeeded)
                {
                    return await ServiceOutput.GenerateAsync(422, false, result.Errors.FirstOrDefault()?.Description ?? "Kullanıcı oluşturulamadı.");
                }

                return await ServiceOutput.GenerateAsync(200, true, "Başarılı");
            }
            catch (Exception ex)
            {
                return await ServiceOutput.GenerateAsync(500, false, "İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }





        private string? GenerateJwtToken(AppUser user)
        {
            try
            {
             

                var keyString = _configuration["JwtKey"];
                if (string.IsNullOrEmpty(keyString))
                {
                    return null;
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpire"]));

                var token = new JwtSecurityToken(
                    _configuration["JwtIssuer"],
                    _configuration["JwtAudience"],
                    expires: expires,
                    signingCredentials: creds
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IServiceOutput> Login(LoginModel model)
        {
            try
            {
                TokenModel returnModel = new TokenModel();

                // Kullanıcıyı e-posta ile bul
                var userByEmail = await _userManager.FindByEmailAsync(model.email);
                // Kullanıcıyı kullanıcı adı ile bul
                var userByUsername = await _userManager.FindByNameAsync(model.username);

                // E-posta ile kullanıcı bulunamadıysa kullanıcı adı ile kontrol et
                var user = userByEmail ?? userByUsername;

                if (user != null && await _userManager.CheckPasswordAsync(user, model.password.Trim()))
                {
                    var token = GenerateJwtToken(user);
                    if (token == null)
                    {
                        return await ServiceOutput.GenerateAsync(422, false, "Token oluşturulamadı");
                    }

                    var accessTokenExpireTimeSpan = TimeSpan.FromDays(Convert.ToDouble(_configuration["JwtExpire"])).TotalSeconds;

                    returnModel.access_token = token.ToString();
                    returnModel.token_type = "bearer";
                    returnModel.expires_in = accessTokenExpireTimeSpan.ToString();
                    returnModel.issued = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
                    returnModel.expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpire"])).ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";

                    return await ServiceOutput.GenerateAsync(200, true, "Başarılı", data: returnModel);
                }
                else
                {
                    return await ServiceOutput.GenerateAsync(422, false, "Geçersiz e-posta/ad veya şifre.");
                }
            }
            catch (Exception ex)
            {
                return await ServiceOutput.GenerateAsync(500, false, "İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

    }
}

