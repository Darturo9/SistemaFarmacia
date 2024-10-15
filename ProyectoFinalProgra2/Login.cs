using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace ProyectoFinalProgra2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            List<Usuario> TEST = new CN_Usuario().Listar();


            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == txtnumdocumento.Text && u.Clave == txtclave.Text).FirstOrDefault();
            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);
                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;

            }
            else
            {
                MessageBox.Show("No existe el usuario ingresado","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            
        }

        private void frm_closing(object sender,FormClosingEventArgs e)
        {
            txtclave.Text = "";
            txtnumdocumento.Text = "";

            this.Show();
        }
    }
}
