using PastaFlow_DIAZ_PEREZ.Models;
using PastaFlow_DIAZ_PEREZ.Services;
using PastaFlow_DIAZ_PEREZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PastaFlow_DIAZ_PEREZ.Forms
{
    public partial class FRegistrarEmpleado : Form
    {
        public FRegistrarEmpleado()
        {
            InitializeComponent();
        }

        private void FRegEmpleado_Load(object sender, EventArgs e)
        {
            List<Rol> roles = new ServiceRol().ListarRoles();
            foreach (Rol item in roles)
            {
                cBoxRol.Items.Add(new ComboOption() { Valor = item.Id_rol, Texto = item.Nombre_rol });
            }
            cBoxRol.DisplayMember = "Texto";
            cBoxRol.ValueMember = "Valor";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Add(new object[] {
                txtEmpNombre.Text,
                txtEmpApellido.Text,
                txtEmpDNI.Text,
                txtEmpCorreo.Text,
                txtEmpTelefono.Text,
                ((ComboOption)cBoxRol.SelectedItem).Texto.ToString(),
                "",
                ""
            });

           

            LimpiarRegistro();
        }

        private void LimpiarRegistro()
        {
            txtEmpNombre.Clear();
            txtEmpApellido.Clear();
            txtEmpDNI.Clear();
            txtEmpCorreo.Clear();
            txtEmpTelefono.Clear();
            txtEmpContrasena.Clear();
            txtEmpRContrasena.Clear();
            cBoxRol.SelectedIndex = -1;
        }

        private void txtEmpNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                // Si no es una letra, cancela el ingreso del carácter
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtEmpNombre.Text.Length >= 15)
            {
                e.Handled = true;
                MessageBox.Show("El Nombre debe contener 15 caracteres como máximo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtEmpApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                // Si no es una letra, cancela el ingreso del carácter
                e.Handled = true;
            }
            if (char.IsLetter(e.KeyChar) && txtEmpApellido.Text.Length >= 15)
            {
                e.Handled = true;
                MessageBox.Show("El Apellido debe contener 15 caracteres como máximo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtEmpDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo permite ingresar numeros en el textbox
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            //Solo permite ingresar 8 caracteres en el textbox
            if (char.IsDigit(e.KeyChar) && txtEmpDNI.Text.Length >= 8)
            {
                e.Handled = true;
                MessageBox.Show("El Número Documento debe contener 8 digitos como mínimo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtEmpTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            //Solo permite ingresar 8 caracteres en el textbox
            if (char.IsDigit(e.KeyChar) && txtEmpTelefono.Text.Length <= 10)
            {
                e.Handled = true;
                MessageBox.Show("El Teléfono debe contener 10 digitos como mínimo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtEmpContra_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtEmpRContra_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
