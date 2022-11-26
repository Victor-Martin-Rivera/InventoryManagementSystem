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
    public partial class Registrarte : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlCommand cm2 = new SqlCommand();
       
        public Registrarte()
        {
            InitializeComponent();
        }

        private void Registrarte_Load(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContra.Text != txtRepeat.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("¿Está seguro de que desea guardar este usuario?", "Guardar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbLogin(username,nombre_completo,contra,telefono)VALUES(@username,@nombre_completo,@contra,@telefono)", con);
                    cm.Parameters.AddWithValue("@username", txtUsuario.Text);
                    cm.Parameters.AddWithValue("@nombre_completo", txtCompleto.Text);
                    cm.Parameters.AddWithValue("@contra", txtContra.Text);
                    cm.Parameters.AddWithValue("@telefono", txtTelefono.Text);

                    cm2 = new SqlCommand("INSERT INTO tbCustomer(customer_nombre,customer_telefono)VALUES(@customer_nombre, @customer_telefono)", con);
                    cm2.Parameters.AddWithValue("@customer_nombre", txtUsuario.Text);
                    cm2.Parameters.AddWithValue("@customer_telefono", txtTelefono.Text);
                    MessageBox.Show("El usuario se ha guardado correctamente.");

                    Clear();
                    con.Open();
                    cm2.ExecuteNonQuery();
                    cm.ExecuteNonQuery();
                    con.Close();

                    LoginUsuario main = new LoginUsuario();
                    this.Hide();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            
        }
        public void Clear()
        {
            txtUsuario.Clear();
            txtCompleto.Clear();
            txtContra.Clear();
            txtRepeat.Clear();
            txtTelefono.Clear();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

}
