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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventoryManagementSystem
{
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cm = new SqlCommand("SELECT nombre_categoria FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboCat.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                int max = Convert.ToInt32(txtPQty.Text);
                if (max <= 0)
                {
                    MessageBox.Show("Introduce una cantidad mayor a 0");
a
                }
                else if (MessageBox.Show("¿Está seguro de que desea guardar este producto?", "Guardar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbProduct(nombre_producto,cantidad_producto,precio_producto,descripcion_producto,categoria_producto, ubicacion)VALUES(@nombre_producto, @cantidad_producto, @precio_producto, @descripcion_producto, @categoria_producto, @ubicacion)", con);
                    cm.Parameters.AddWithValue("@nombre_producto", txtPName.Text);
                    cm.Parameters.AddWithValue("@cantidad_producto", Convert.ToInt16(txtPQty.Text));
                    cm.Parameters.AddWithValue("@precio_producto", Convert.ToInt16(txtPPrice.Text));
                    cm.Parameters.AddWithValue("@descripcion_producto", txtPDes.Text);
                    cm.Parameters.AddWithValue("@categoria_producto", comboCat.Text);
                    cm.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El producto se ha guardado correctamente.");
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
            txtPName.Clear();
            txtPQty.Clear();
            txtPPrice.Clear();
            txtPDes.Clear();
            comboCat.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int max = Convert.ToInt32(txtPQty.Text);
                if (max <= 0)
                {
                    MessageBox.Show("Introduce una cantidad mayor a 0");

                }
                else if (MessageBox.Show("¿Esta seguro de que desea actualizar este producto?", "Actualizar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("UPDATE tbProduct SET nombre_producto = @nombre_producto, cantidad_producto=@cantidad_producto, precio_producto=@precio_producto, descripcion_producto=@descripcion_producto, categoria_producto=@categoria_producto, ubicacion=@ubicacion WHERE Id_Producto LIKE '" + lblPid.Text + "' ", con);
                    cm.Parameters.AddWithValue("@nombre_producto", txtPName.Text);
                    cm.Parameters.AddWithValue("@cantidad_producto", Convert.ToInt16(txtPQty.Text));
                    cm.Parameters.AddWithValue("@precio_producto", Convert.ToInt16(txtPPrice.Text));
                    cm.Parameters.AddWithValue("@descripcion_producto", txtPDes.Text);
                    cm.Parameters.AddWithValue("@categoria_producto", comboCat.Text);
                    cm.Parameters.AddWithValue("@ubicacion", txtUbicacion.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El producto ha sido actualizado con éxito!");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void comboCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

    }
}
