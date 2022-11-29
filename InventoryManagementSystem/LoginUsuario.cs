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
    public partial class LoginUsuario : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public LoginUsuario()
        {
            InitializeComponent();
        }

        private void lblRegister_MouseEnter(object sender, EventArgs e)
        {
            lblRegister.Font = new Font(lblRegister.Font.Name, lblRegister.Font.SizeInPoints, FontStyle.Underline);
        }

        private void lblRegister_MouseLeave(object sender, EventArgs e)
        {
            lblRegister.Font = new Font(lblRegister.Font.Name, lblRegister.Font.SizeInPoints, FontStyle.Regular);
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            Registrarte reg = new Registrarte();
            reg.ShowDialog();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicacion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cm = new SqlCommand("SELECT * FROM tbCustomer WHERE nombre_usuario=@nombre_usuario AND password=@password", con);
                cm.Parameters.AddWithValue("@nombre_usuario", txtNombre.Text);
                cm.Parameters.AddWithValue("@password", txtContra.Text);
                con.Open();
                dr = cm.ExecuteReader();
                dr.Read();
               
                if (dr.HasRows)
                {
                    MessageBox.Show("Bienvenid@ " + dr["nombre_completo"].ToString() + " ", "Acceso Permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainUsuarios main = new MainUsuarios();
                    this.Hide();
                    main.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Usuario o contraseña invalido!", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtContra.Clear();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
                txtContra.UseSystemPasswordChar = true;
            else
                txtContra.UseSystemPasswordChar = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            this.Dispose();
            form.ShowDialog();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            lblAdmin.Font = new Font(lblAdmin.Font.Name, lblAdmin.Font.SizeInPoints, FontStyle.Underline);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            lblAdmin.Font = new Font(lblAdmin.Font.Name, lblAdmin.Font.SizeInPoints, FontStyle.Regular);
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }

        private void txtContra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }
    }
}
