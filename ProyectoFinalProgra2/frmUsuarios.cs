﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoFinalProgra2.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace ProyectoFinalProgra2
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtNombreCompleto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //dgvData.Rows.Add(new object[] { "", txtId.Text, txtDocumento.Text, txtNombreCompleto.Text, txtCorreo.Text, txtClave.Text,
            //    ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(), ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
            //    ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
            //});

            //Limpiar();

        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "";
            txtDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new Utilidades.OpcionCombo() { Texto = "Activo", Valor = 1 });
            cboEstado.Items.Add(new Utilidades.OpcionCombo() { Texto = "No Activo", Valor = 0 });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                cboRol.Items.Add(new Utilidades.OpcionCombo() { Texto = item.Descripcion, Valor = item.IdRol });
                cboRol.DisplayMember = "Texto";
                cboRol.ValueMember = "Valor";
                cboRol.SelectedIndex = 0;

            }

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                    cboBusqueda.DisplayMember = "Texto";
                    cboBusqueda.ValueMember = "Valor";
                    cboBusqueda.SelectedIndex = 0;
                }
            }

            //mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { "", item.IdUsuario , item.Documento, item.NombreCompleto, item.Correo, item.Clave, item.oRol.IdRol,
                item.oRol.Descripcion, item.Estado == true ?1 : 0,
                item.Estado == true ?"Activo" : "No activo",
            });

            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if(e.RowIndex < 0) return;

            if (e.ColumnIndex == 0 )
            {
                e.Paint(e.CellBounds,DataGridViewPaintParts.All);

                var w = Properties.Resources.check_box_16dp_000000_FILL0_wght100_GRAD0_opsz20.Width - 30;
                var h = Properties.Resources.check_box_16dp_000000_FILL0_wght100_GRAD0_opsz20.Height - 30;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check_box_16dp_000000_FILL0_wght100_GRAD0_opsz20, new Rectangle(x, y, w, h));
                e.Handled = true;

            }

        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar") { 
            
                int indice = e.RowIndex;

                if (indice >= 0) {


                    txtIndice.Text = indice.ToString(); 
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    txtConfirmarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();

                    foreach (OpcionCombo oc in cboRol.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idRol"].Value))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);

                            cboRol.SelectedIndex = indice_combo;
                            break;
                        }

                    }

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);

                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }

                    }

                }

            }

        }
    }
}
