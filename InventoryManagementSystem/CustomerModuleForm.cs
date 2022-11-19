using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                if (MessageBox.Show("¿Esta seguro de que desea guardar este cliente?", "Guardar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbCustomer(customer_nombre,customer_telefono)VALUES(@customer_nombre, @customer_telefono)", con);
                    cm.Parameters.AddWithValue("@customer_nombre", txtCName.Text);
                    cm.Parameters.AddWithValue("@customer_telefono", txtCPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El cliente se ha guardado correctamente.");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           try
            { 
                if (MessageBox.Show("¿Esta seguro de que desea actualizar este cliente?", "Actualizar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbCustomer SET customer_nombre = @customer_nombre,customer_telefono=@customer_telefono WHERE Id_Cliente LIKE '" + lblCId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@customer_nombre", txtCName.Text);               
                    cm.Parameters.AddWithValue("@customer_telefono", txtCPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El cliente ha sido actualizado con éxito!");
                    this.Dispose();
                }

             }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
