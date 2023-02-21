using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minimarket.Entidades;
using Minimarket.Negocio;

namespace Minimarket.Presentacion
{
    public partial class Frm_Categorias : Form
    {
        public Frm_Categorias()
        {
            InitializeComponent();
        }
        #region "Mis Vriables"
        int Codigo_ca = 0;
        int Estadoguarda = 0;
        #endregion 

        #region "Mis metodos"
        private void Formato_ca()
        {
            Dgv_principal.Columns[0].Width = 100;
            Dgv_principal.Columns[0].HeaderText = "CODIGO_CA";
            Dgv_principal.Columns[1].Width = 300;
            Dgv_principal.Columns[1].HeaderText = "CATEGORIA";
        }

        private void Listado_ca(string cTexto)
        {
            try
            {
                Dgv_principal.DataSource = N_Categorias.Listado_ca(cTexto);
                this.Formato_ca();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void Estado_BotonesPrincipales(bool lEstado)
        {
            this.Btn_nuevo.Enabled = lEstado;
            this.Btn_actualizar.Enabled = lEstado;
            this.Btn_eliminar.Enabled = lEstado;
            this.Btn_reporte.Enabled = lEstado;
            this.Btn_salir.Enabled = lEstado;

        }
        private void Estado_BotonesProcesos(bool lEstado)
        {
            this.Btn_cancelar.Visible = lEstado;
            this.Btn_guardar.Visible = lEstado;
            this.Btn_retornar.Visible =! lEstado;
        }
        private void Selecciona_item()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_principal.CurrentRow.Cells["codigo_ca"].Value)))
            {
                MessageBox.Show("No se tiene informacion para visualizar", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Codigo_ca =Convert.ToInt32(Dgv_principal.CurrentRow.Cells["codigo_ca"].Value);
                Txt_descripcion_ca.Text = Convert.ToString( Dgv_principal.CurrentRow.Cells["descripcion_ca"].Value);
            }
            
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            Estadoguarda = 1;
            this.Estado_BotonesPrincipales(false);
            this.Estado_BotonesProcesos(true);
            Txt_descripcion_ca.Text = "";
            Txt_descripcion_ca.ReadOnly = false;
            tbp_principal.SelectedIndex = 1;
            Txt_descripcion_ca.Focus();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.Listado_ca("%");
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            if(Txt_descripcion_ca.Text == String.Empty)
            {
                MessageBox.Show("Falta ingresar datos requeridos(*)", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                E_Categorias oCa = new E_Categorias();
                String Rpta = "";
                oCa.Codigo_ca = this.Codigo_ca;
                oCa.Descripcion_ca = Txt_descripcion_ca.Text.Trim();
                Rpta = N_Categorias.Guardar_ca(Estadoguarda, oCa);
                if(Rpta=="OK")
                {
                    this.Listado_ca("%");
                    MessageBox.Show("Los datos han sido guardados correctamente", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Estadoguarda = 0;
                    this.Estado_BotonesPrincipales(true);
                    this.Estado_BotonesProcesos(false);
                    Txt_descripcion_ca.Text = "";
                    Txt_descripcion_ca.ReadOnly = true;
                    tbp_principal.SelectedIndex = 0;
                    this.Codigo_ca = 0;
                }
                else
                {
                    MessageBox.Show(Rpta, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void Btn_actualizar_Click(object sender, EventArgs e)
        {
            Estadoguarda = 2;
            this.Estado_BotonesPrincipales(false);
            this.Estado_BotonesProcesos(true);
            this.Selecciona_item();
            tbp_principal.SelectedIndex = 1;
            Txt_descripcion_ca.ReadOnly = false;
            
            Txt_descripcion_ca.Focus();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            Estadoguarda = 0;
            Txt_descripcion_ca.Text = "";
            Txt_descripcion_ca.ReadOnly = true;
            this.Estado_BotonesPrincipales(true);
            this.Estado_BotonesProcesos(false);
            tbp_principal.SelectedIndex = 0;
        }

        private void Dgv_principal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dgv_principal_DoubleClick(object sender, EventArgs e)
        {
            this.Selecciona_item();
            this.Estado_BotonesProcesos(false);
            tbp_principal.SelectedIndex = 1;

        }

        private void Btn_retornar_Click(object sender, EventArgs e)
        {
            this.Estado_BotonesProcesos(false);
            tbp_principal.SelectedIndex = 0;
        }
    }
}
