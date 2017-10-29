using FluentValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyDashboard.Api.Security;
using MyDashboard.Domain.Commands.Inputs;
using MyDashboard.Domain.Entities;
using MyDashboard.Domain.Enums;
using MyDashboard.Domain.Repositories;
using MyDashboard.Infra.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MyDashboard.Api.Controllers
{
    public class AccountController : BaseController
    {
        private Customer _customer;
        private readonly ICustomerRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;


        public AccountController(IOptions<TokenOptions> jwtOptions, IUow uow, ICustomerRepository repository) : base(uow)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            // Teste

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [HttpPost]
        [Route("v1/authenticate")]
        [AllowAnonymous]                     
        public async Task<IActionResult> Post([FromBody] AuthenticateUserInput command)
        {
            if (command == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null, new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, command.Username),
                new Claim(JwtRegisteredClaimNames.NameId, command.Username),
                new Claim(JwtRegisteredClaimNames.Email, command.Username),
                new Claim(JwtRegisteredClaimNames.Sub, command.Username),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst("MyDashboard")
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims.AsEnumerable(),
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _customer.Id,
                    name = _customer.Name.ToString(),
                    email = _customer.Email.Address,
                    username = _customer.User.Name,
                    profile = _customer.User.ProfileId
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);

        }

        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserInput command)
        {
            var customer = _repository.GetByUserEmail(command.Username);

            if (customer == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if (!customer.Authenticate(customer.Email.Address, command.Password))
                return Task.FromResult<ClaimsIdentity>(null);

            _customer = customer;

            if (customer.User.ProfileId == (int)EnumProfile.Admin)
            {
                return Task.FromResult(new ClaimsIdentity(
                new GenericIdentity(customer.Email.Address, "Token"),
                new[] {
                    new Claim("MyDashboard", "Admin")
                    //,new Claim("MyDashboard", "User")
                }));
            }
            else
            {
                return Task.FromResult(new ClaimsIdentity(
             new GenericIdentity(customer.Email.Address, "Token"),
             new[] {
                    new Claim("MyDashboard", "User")
             }));
            }

        }
    }
}
