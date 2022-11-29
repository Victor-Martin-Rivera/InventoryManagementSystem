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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT Id_Pedido, fecha_pedido, O.Id_Producto, P.nombre_producto, O.Id_Cliente, C.nombre_completo, cantidad, precio, total FROM tbOrder AS O JOIN tbCustomer AS C ON O.Id_Cliente=C.Id_Cliente JOIN tbProduct AS P ON O.Id_Producto=P.Id_Producto WHERE CONCAT (Id_Pedido, fecha_pedido, O.Id_Producto, P.nombre_producto, O.Id_Cliente, C.nombre_completo, cantidad, precio) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void btnAdd_Click_2(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            LoadOrder();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("¿Esta seguro de que desea eliminar este pedido?", "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE Id_Pedido LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("El registro se ha eliminado correctamente!");

                    cm = new SqlCommand("UPDATE tbProduct SET cantidad_producto=(cantidad_producto+@cantidad_producto) WHERE Id_Producto LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString()+ "' ", con);
                    cm.Parameters.AddWithValue("@cantidad_producto", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[7].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    
                } 
            }
            LoadOrder();
        }
    }
}