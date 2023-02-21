using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using proyecto12.DTO;
using proyecto12.Modelo;
using proyecto12.Repositorio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proyecto12.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> log;
        private readonly IUsuariosSQLServer repositorio;
        public LoginController(IConfiguration configuration, ILogger<LoginController> log, IUsuariosSQLServer repositorio)
        {
            this.configuration = configuration;
            this.log = log;
            this.repositorio = repositorio;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Login(LoginAPI usuariologin)
        {
            UsuarioAPI Usuario = null;
            Usuario = await AutenticarUsuarioAsync(usuariologin);
            if (Usuario == null)
                throw new Exception("Credenciales no validas");
            else
                Usuario = GenerarTokenJWT(Usuario);
            return Usuario.convertirDTO();

        }
        private async Task<UsuarioAPI> AutenticarUsuarioAsync(LoginAPI usuariologin)
        {
            UsuarioAPI usuarioAPI = await repositorio.DameUsuario(usuariologin);
            return usuarioAPI;

        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[]
            {
                new Claim("usuario",usuarioInfo.Usuario),
                 new Claim("email",usuarioInfo.Email),
                  new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
            };

            var _Payload = new JwtPayload(

                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(24)
                ) ;

            var _Token=new JwtSecurityToken(
                _Header, _Payload);
            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);
            return usuarioInfo;
        }
    }
}
