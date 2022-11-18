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
    public partial class MainUsuarios : Form
    {
        
        public MainUsuarios()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            openChildForm(new Productos());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new Clientes());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new Categorias());
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            openChildForm(new Pedidos());
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicacion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void customerButton1_Click(object sender, EventArgs e)
        {
          if (MessageBox.Show("¿Quieres cerrar sesion?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Sesion cerrada con exito ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginUsuario user = new LoginUsuario();
                this.Hide();
                user.ShowDialog();
            }
          
        }
    }
}
