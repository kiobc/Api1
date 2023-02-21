using proyecto12.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace proyecto12.Repositorio
{
    public class UsuarioSQLServer:IUsuariosSQLServer
    {
        private string CadenaConexion;
        private readonly ILogger<UsuarioSQLServer> log;
        public UsuarioSQLServer(AccesoDatos cadenaConexion, ILogger<UsuarioSQLServer> log)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task<UsuarioAPI>DameUsuario(LoginAPI login)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            UsuarioAPI u = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuarioAPI_Obtener";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@UsuarioAPI", SqlDbType.VarChar, 500).Value = login.usuarioAPI;
                Comm.Parameters.Add("@passAPI", SqlDbType.VarChar, 50).Value = login.passAPI;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                if (reader.Read())
                {
                    u = new UsuarioAPI
                    {
                        Usuario = reader["UsuarioAPI"].ToString(),
                        Email = reader["EmailUsuario"].ToString(),
                    };
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al loggearse" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return u;
        }
    }
}
