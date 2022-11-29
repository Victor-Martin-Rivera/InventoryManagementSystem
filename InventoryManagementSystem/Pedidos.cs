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
    public partial class Pedidos : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marti\OneDrive\Documentos\dbInventory.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Pedidos()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            panelOrder.Rows.Clear();
            cm = new SqlCommand("SELECT Id_Pedido, fecha_pedido, O.Id_Producto, P.nombre_producto, O.Id_Cliente, C.nombre_usuario, cantidad, precio, total  FROM tbOrder AS O JOIN tbCustomer AS C ON O.Id_Cliente=C.Id_Cliente JOIN tbProduct AS P ON O.Id_Producto=P.Id_Producto WHERE CONCAT (Id_Pedido, fecha_pedido, O.Id_Producto, P.nombre_producto, O.Id_Cliente, C.nombre_usuario, cantidad, precio) LIKE '%" + txtSearch.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                panelOrder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            con.Close();

            lblQty.Text = i.ToString();
            lblTotal.Text = total.ToString();
        }

      

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }

      
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelOrder_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = panelOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                MessageBox.Show("No tienes permisos suficientes para borrar Pedidos");
            }
            LoadOrder();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }
    }
}
