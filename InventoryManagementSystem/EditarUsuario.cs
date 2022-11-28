using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class EditarUsuario : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlCommand cm2 = new SqlCommand();
        public EditarUsuario()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            txtUserName.Clear();
            txtFullName.Clear();
            txtPass.Clear();
            txtRepass.Clear();
            txtPhone.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtRepass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("¿Esta seguro de que desea actualizar este usuario?", "Actualizar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbLogin SET nombre_completo = @nombre_completo, contra=@contra, telefono=@telefono WHERE username LIKE '" + txtUserName.Text + "' ", con);
                    cm.Parameters.AddWithValue("@nombre_completo", txtFullName.Text);
                    cm.Parameters.AddWithValue("@contra", txtPass.Text);
                    cm.Parameters.AddWithValue("@telefono", txtPhone.Text);

                    cm2 = new SqlCommand("UPDATE tbCustomer SET customer_nombre = @customer_nombre, customer_telefono=@customer_telefono WHERE customer_nombre LIKE '" + txtUserName.Text + "' ", con);
                    cm2.Parameters.AddWithValue("@customer_nombre", txtFullName.Text);
                    cm2.Parameters.AddWithValue("@customer_telefono", txtPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    cm2.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El usuario ha sido actualizado con éxito!");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
