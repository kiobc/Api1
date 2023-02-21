using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Minimarket.Entidades;
using Minimarket.Datos;

namespace Minimarket.Negocio
{
    public class N_Categorias
    {
        public static DataTable Listado_ca(string cTexto)
        {
            D_Categorias Datos = new D_Categorias();
            return Datos.Listado_ca(cTexto);
        }
        public static string Guardar_ca(int nOpcion, E_Categorias oCa)
        {
            D_Categorias Datos = new D_Categorias();
            return Datos.Guardar_ca(nOpcion, oCa);

        }
    }
}
