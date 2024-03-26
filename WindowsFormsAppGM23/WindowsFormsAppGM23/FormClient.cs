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

namespace WindowsFormsAppGM23
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            ListerClients();
        }

        private void ListerClients()
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select nom from Client";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            listBoxClient.Items.Clear();
            while (sqreader.Read() == true)
            {
                string str = sqreader["Nom"].ToString();
                listBoxClient.Items.Add(str);
            }

            sqreader.Close();
            cn.Close();
        }

        private void listBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomClient = listBoxClient.SelectedItem.ToString();

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string strsql = "select * from Client where nom = '" + nomClient + "'";
            SqlCommand ccom = new SqlCommand(strsql, cn);
            SqlDataReader sqreader = ccom.ExecuteReader();

            sqreader.Read();

            textBoxNom.Text = nomClient;
            textBoxAsresse.Text = sqreader["Adresse"].ToString();
            textBoxMail.Text = sqreader["Mail"].ToString(); 
            textBoxTel.Text = sqreader["Tel"].ToString();

            sqreader.Close();
            cn.Close();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            string sqlSearch = "select count(*) as nb from Client where" +
                " Nom = '" + textBoxNom.Text + "'";

            SqlCommand ccoms = new SqlCommand(sqlSearch, cn);
            SqlDataReader sqreader = ccoms.ExecuteReader();
            sqreader.Read();
            int res = Convert.ToInt32(sqreader["nb"].ToString());

            if (res > 0)
            {
                MessageBox.Show("Ce client existe déjà !", 
                    "ATTENTION", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                cn.Close();
                textBoxNom.Focus();
                return;
            }

            string nom = textBoxNom.Text;
            string adresse = textBoxAsresse.Text;
            string mail = textBoxMail.Text;
            string tel = textBoxTel.Text;

            sqreader.Close();

            string strsql = "INSERT INTO Client VALUES('" + nom +
                "','" + adresse + "','" + mail + "','" + tel + "')";

            SqlCommand ccom = new SqlCommand(strsql, cn);
            ccom.ExecuteNonQuery();

            sqreader.Close();
            cn.Close();

            ListerClients();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            string nom = textBoxNom.Text;
            string adresse = textBoxAsresse.Text;
            string mail = textBoxMail.Text;
            string tel = textBoxTel.Text;


            string strsql = "UPDATE Client SET Adresse = '"
                + adresse + "', mail = '" + mail + "', tel ='"
                + tel + "' where nom = '" + nom + "'";

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            SqlCommand ccom = new SqlCommand(strsql, cn);
            ccom.ExecuteNonQuery();

            cn.Close();

            ListerClients();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            string nom = textBoxNom.Text;

            string strsql = "delete from client where nom = '" + nom + "'";

            string strcon = "Server=.\\SQLEXPRESS;Database=GM23;Trusted_Connection=True;";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();

            SqlCommand ccom = new SqlCommand(strsql, cn);
            ccom.ExecuteNonQuery();

            cn.Close();

            ListerClients();

            textBoxNom.Text = textBoxAsresse.Text = textBoxMail.Text = textBoxTel.Text = "";
        }
    }
}
