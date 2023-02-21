using proyecto12.Modelo;

namespace proyecto12.Repositorio
{
    public interface IUsuariosSQLServer
    {
        Task<UsuarioAPI> DameUsuario(LoginAPI login);
    }
}
