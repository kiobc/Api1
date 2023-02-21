namespace proyecto12.Modelo
{
    public class AccesoDatos
    {
        private string cadenaConexionSql;
        public string CadenaConexionSQL { get => cadenaConexionSql; }
        public AccesoDatos(string ConexionSQL)
        {
            cadenaConexionSql = ConexionSQL;
        }
    }
}
