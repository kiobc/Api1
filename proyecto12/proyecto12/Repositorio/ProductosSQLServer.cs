using proyecto12.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace proyecto12.Repositorio
{
    public class ProductosSQLServer : IProductosEnMemoria
    {
        private string CadenaConexion;
        private readonly ILogger<ProductosSQLServer> log;
        public ProductosSQLServer(AccesoDatos cadenaConexion , ILogger<ProductosSQLServer> log)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = log;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task BorrarProductoAsincrono(string SKU)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Borrar";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al eliminar datos" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            await Task.CompletedTask;

        }

        public async Task CrearProductoAsincrono(Producto p)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.ProductosAlta";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = p.Nombre;
                Comm.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = p.Descripcion;
                Comm.Parameters.Add("@Precio", SqlDbType.Float).Value = p.Precio;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = p.SKU;
               await Comm.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta"+ ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
        }

        public async Task <Producto> DameProductoAsincrono(string SKU)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            Producto p = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Obtener";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                if (reader.Read())
                {
                    p = new Producto { 
                        Nombre = reader["Nombre"].ToString(), 
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToDouble(reader["Precio"].ToString()),
                        SKU = reader["SKU"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return p;
        }

        public async Task <IEnumerable<Producto>> DameProductosAsincrono(int pag, int reg)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            List<Producto> productos = new List<Producto>();
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Obtener";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@PAG", SqlDbType.Int).Value=pag;
                Comm.Parameters.Add("@REG", SqlDbType.Int).Value = reg;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                Producto p = null;
                while (reader.Read())
                {
                    p = new Producto
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToDouble(reader["Precio"].ToString()),
                        SKU = reader["SKU"].ToString()
                    };
                    productos.Add(p);
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            return productos;
        }

        public async Task ModificarProductoAsincrono(Producto p)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Modificar";
                Comm.CommandType = System.Data.CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = p.Nombre;
                Comm.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = p.Descripcion;
                Comm.Parameters.Add("@Precio", SqlDbType.Float).Value = p.Precio;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = p.SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al modificar al producto" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            await Task.CompletedTask;
        }
    }
}
