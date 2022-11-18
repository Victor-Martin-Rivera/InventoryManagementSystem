﻿using System;
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
                    MessageBox.Show("Password did not Match!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this user?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cm = new SqlCommand("INSERT INTO tbLogin(username,nombre_completo,contra,telefono)VALUES(@username,@nombre_completo,@contra,@telefono)", con);
                    cm.Parameters.AddWithValue("@username", txtUsuario.Text);
                    cm.Parameters.AddWithValue("@nombre_completo", txtCompleto.Text);
                    cm.Parameters.AddWithValue("@contra", txtContra.Text);
                    cm.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfully saved.");
                    Clear();
                    LoginUsuario main = new LoginUsuario();
                    this.Hide();
                    main.ShowDialog();
                    
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
    }

}